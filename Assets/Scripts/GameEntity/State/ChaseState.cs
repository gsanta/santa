
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaseState : CharacterState
{
    public static string Name = "Chase";

    private PathFinding _pathFinding = new PathFinding();

    private Character _character;

    private Movement _movement;

    private PathGrid _pathGrid;

    private List<Vector2> _path = new();


    protected override void OnStarted()
    {
        _stateName = Name;
        _character = GetComponent<Character>();
        _movement = GetComponent<Movement>();
        _pathGrid = Services.GetInstance().GetPathGrid();

        AddStateTransition(new EndChaseTransition(_character));

        FindTarget();
    }

    protected override void OnActivated()
    {
        InvokeRepeating("FindTarget", 0, 3f);
    }

    protected override void OnUpdated()
    {
        if (!IsActive())
        {
            return;
        }

        if (IsPathFinished())
        {
            Finish();
        }
        else
        {
            if (IsNextPathPointReached())
            {
                _path.RemoveAt(0);
            }

            _movement.SetDirection(_path[0] - _character.GetPosition());
        }
    }

    private bool IsPathFinished()
    {
        return _path.Count == 0 || (IsNextPathPointReached() && _path.Count == 1);
    }

    private bool IsNextPathPointReached()
    {
        if (_path.Count == 0)
        {
            return true;
        }

        if (Vector2.Distance(_character.GetPosition(), _path[0]) < 0.2f)
        {
            return true;
        }

        return false;
    }

    private void FindTarget()
    {
        var position = _character.GetPosition();
        var startNode = _pathGrid.GetNodeAtWorldPos(position);

        var playerPosition = CharacterStore.GetInstance().GetPlayer().GetPosition();
        var targetNode = _pathGrid.GetNodeAtWorldPos(playerPosition);

        var nodes = _pathFinding.FindPath(_pathGrid, startNode, targetNode);

        _path = nodes.Select((node) => _pathGrid.GetNodeWorldPosition(node).Value).ToList();
    }

    private void Finish()
    {
        //_path = new();
        //_movement.SetDirection(new Vector2(0, 0));
        //SetIsActive(false);

        //var nextState = Array.Find(_character.States, (state) => state.GetName() == IdleState.Name);
        //nextState.SetIsActive(true);
    }
}
