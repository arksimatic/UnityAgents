public class Notify : IState
{
    private VisionCone _visionCone;
    private readonly PatrolRobot _patrolRobot;
    public Notify(PatrolRobot patrolRobot)
    {
        _patrolRobot = patrolRobot;
    }


    public void Tick()
    {

    }
    public void OnEnter()
    {
        _patrolRobot.textMeshPro.text = "Notifying";
        _patrolRobot.SendSignal();
    }

    public void OnExit()
    {

    }
}