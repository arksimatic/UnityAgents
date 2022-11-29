using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomlySearchLocation:IState
{
    private readonly PatrolRobot _patrolRobot;
    public RandomlySearchLocation(PatrolRobot patrolRobot)
    {
        _patrolRobot = patrolRobot;
    }

    public void Tick()
    {
        
    }
    public void OnEnter()
    {
        _patrolRobot.textMeshPro.text = "Searching !";
        _patrolRobot.navMeshAgent.ResetPath();
        _patrolRobot.time = 0;
        _patrolRobot.SetDestination(_patrolRobot.transform.position + _patrolRobot.transform.forward * 10f);
    }
    public void OnExit()
    {
       
    }
}