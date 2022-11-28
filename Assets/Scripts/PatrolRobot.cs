using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRobot : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public VisionCone visionCone;
    public List<PatrolPoint> patrolPoints = new();
    public Transform Target = null;
    
    private StateMachine _stateMachine;
    private bool _goForEnemy;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        var patrol = new Patrol(this,patrolPoints);
        var goToEnemy = new GoToEnemy(this);
        
        AddAnyTransition(goToEnemy,EnemyIsVisible());
        AddTransition(patrol,goToEnemy,EnemySpottedSomewhere());
        
        Func<bool> EnemyIsVisible() => () => visionCone.enemySpotted;
        Func<bool> EnemyIsNotVisible() => () => !visionCone.enemySpotted;
        Func<bool> EnemySpottedSomewhere() => () => _goForEnemy;
        
        _stateMachine.SetState(patrol);
    }
    
    private void AddTransition(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
    private void AddAnyTransition(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
    
    public void SetDestination(Vector3 transformPosition)
    {
        navMeshAgent.SetDestination(transformPosition);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
    
    public void SendInformationAboutEnemy(Vector3 enemy, float howLongSpotted)
    {
        var distanceToEnemy = Vector3.Distance(enemy, transform.position);
        // if (howLongSpotted > distanceToEnemy)
        // {
        //     _goForEnemy = true;
        //     print("Go for enemy");
        // }
        //
        // else
        // {
        //     _goForEnemy = false;
        //     print($"Dont go for enemy {howLongSpotted} vs {distanceToEnemy}");
        // }

        _goForEnemy = true;
    }
}