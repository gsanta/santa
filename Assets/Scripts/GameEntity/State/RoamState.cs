using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoamState : CharacterState
{
    public static string Name = "Roam";

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

        AddStateTransition(new IdleStateTransition(_character));
        AddStateTransition(new StartChaseTransition(_character));

        FindTarget();
    }

    protected override void OnUpdated()
    {
        if (!IsActive() || _isEnded)
        {
            return;
        }

        if (IsPathFinished())
        {
            _isEnded = true;
        } else
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

        if (Vector2.Distance(_character.GetPosition(), _path[0]) < 0.2f) {
            return true;
        }

        return false;
    }

    protected override void OnActivated()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        var position = _character.GetPosition();
        var startNode = _pathGrid.GetNodeAtWorldPos(position);
        var targetNode = _pathGrid.GetRandomNode(true);

        var nodes = _pathFinding.FindPath(_pathGrid, startNode, targetNode);

        _path = nodes.Select((node) => _pathGrid.GetNodeWorldPosition(node).Value).ToList();
    }
}
