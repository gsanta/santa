
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoamState : GameEntityState
{
    public static string Name = "Roam";

    private PathFinding _pathFinding = new PathFinding();

    private Character _character;

    private Movement _movement;

    private PathGrid _pathGrid;

    private List<Vector2> _path = new();


    private void Start()
    {
        _stateName = Name;
        _character = GetComponent<Character>();
        _movement = GetComponent<Movement>();
        _pathGrid = Services.GetInstance().GetPathGrid();

        FindTarget();
        SetIsActive(true);
    }

    private void Update()
    {
        if (!IsActive())
        {
            return;
        }

        if (IsDestinationReached())
        {
            if (_path.Count <= 1)
            {
                Finish();
                //FindTarget();
            } else
            {
                _path.RemoveAt(0);
            }
        }

        if (_path.Count > 0)
        {
            _movement.SetDirection(_path[0] - _character.GetPosition());
        }
    }

    private void FindTarget()
    {
        var position = _character.GetPosition();
        var startNode = _pathGrid.GetNodeAtWorldPos(position);
        var targetNode = _pathGrid.GetRandomNode(true);

        var nodes = _pathFinding.FindPath(_pathGrid, startNode, targetNode);

        _path = nodes.Select((node) => _pathGrid.GetNodeWorldPosition(node).Value).ToList();
    }

    private bool IsDestinationReached()
    {
        if (_path.Count == 0)
        {
            return true;
        }

        if (Vector2.Distance(_character.GetPosition(), _path[0]) < 0.2f) {
            return true;
        }

        return false;
    }

    protected override void OnActivated()
    {
        FindTarget();
    }

    protected override void OnDeactivated()
    {
    }

    private void Finish()
    {
        var nextState = Array.Find(_character.States, (state) => state.GetName() == IdleState.Name);

        SetIsActive(false);
        nextState.SetIsActive(true);
    }
}
