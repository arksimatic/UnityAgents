using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Patrol: IState
{
    private readonly PatrolRobot _patrolRobot;
    private readonly List<PatrolPoint> _patrolPoints;
    private int _indexOfCurrentPoint;
    private bool _targetIsSet;
    private PatrolPoint _currentPoint;

    public Patrol(PatrolRobot patrolRobot, List<PatrolPoint> patrolPoints)
    {
        _patrolRobot = patrolRobot;
        _patrolPoints = patrolPoints;
    }

    public void Tick()
    {
        if (_targetIsSet)
        {
        }
        else
        {
            _targetIsSet = true;
            _currentPoint = _patrolPoints[_indexOfCurrentPoint];
            _patrolRobot.SetDestination(_patrolPoints[_indexOfCurrentPoint].transform.position);
        }
    }
    public void OnEnter()
    {
        _patrolRobot.notified = false;
        _patrolRobot.textMeshPro.text = "Patrol";
        _targetIsSet = false;
        _patrolPoints.ForEach(point=>point.onRobotEnter.AddListener(()=>SelectNextPoint(point)));
        var nearestPatrolPoint = _patrolPoints.OrderBy(point => Vector3.Distance(point.transform.position, _patrolRobot.transform.position)).First();
        _indexOfCurrentPoint = _patrolPoints.IndexOf(nearestPatrolPoint);
    }
    private void SelectNextPoint(PatrolPoint patrolPoint)
    {
        if(patrolPoint!=_currentPoint) return;
        if (_indexOfCurrentPoint == _patrolPoints.Count -1)
        {
            _indexOfCurrentPoint = 0;
        }
        else
        {
            _indexOfCurrentPoint++;
        }

        _targetIsSet = false;
    }
    public void OnExit()
    {
    }
}