using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

internal class GoToLastSeenLocation : IState
{
    private readonly PatrolRobot _patrolRobot;
    private VisionCone _visionCone;
    public GoToLastSeenLocation(PatrolRobot patrolRobot)
    {
        _patrolRobot = patrolRobot;

    }
    public void Tick()
    {
        
    }
    public void OnEnter()
    {
        _patrolRobot.textMeshPro.text = "Go to Last Seen Location";
        _patrolRobot.SetDestination(_patrolRobot.LastSeenEnemyLocation);
        
    }
    public void OnExit()
    {
        _patrolRobot.navMeshAgent.ResetPath();
    }
}