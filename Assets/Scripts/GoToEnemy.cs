using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

internal class GoToEnemy : IState
{
    private readonly PatrolRobot _patrolRobot;
    private VisionCone _visionCone;
    public GoToEnemy(PatrolRobot patrolRobot)
    {
        _patrolRobot = patrolRobot;

    }
    public void Tick()
    {
        
    }
    public void OnEnter()
    {
        Debug.Log("Go to enemy enter");
        var enemy = Object.FindObjectOfType<PlayerController>();
        _patrolRobot.SetDestination(enemy.transform.position);
        var howLongSpotted = _visionCone.TimeInRange;
        var robots = Object.FindObjectsOfType<PatrolRobot>().ToList();
        robots.Remove(_patrolRobot);
        foreach (var robot in robots)
        {
            robot.SendInformationAboutEnemy(enemy.transform.position,howLongSpotted);
        }
    }
    public void OnExit()
    {
        Debug.Log("Go to enemy exit");
    }
}