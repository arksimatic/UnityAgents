using System.Linq;
using UnityEngine;

internal class SendMessageToAnotherRobots : IState
{
    private readonly PatrolRobot _robot;
    private readonly VisionCone _visionCone;
    public SendMessageToAnotherRobots(PatrolRobot robot, VisionCone visionCone)
    {
        _robot = robot;
        _visionCone = visionCone;
    }
    public void Tick()
    {
        
    }
    public void OnEnter()
    {
        Debug.Log("Message enter");
        

    }
    public void OnExit()
    {
        Debug.Log("Message exit");
    }
}