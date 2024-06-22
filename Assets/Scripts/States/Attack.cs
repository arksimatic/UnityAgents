using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Attack : IState
{
    private readonly PatrolRobot _patrolRobot;
    private readonly BlasterShot _blasterShot;
    private readonly float _fireDelay;
    private readonly int _energyCost;
    private PlayerController _enemy;

    private float _nextFireTime;
    
    public Attack(PatrolRobot patrolRobot, BlasterShot blasterShot, float fireDelay,int energyCost)
    {
        _patrolRobot = patrolRobot;
        _blasterShot = blasterShot;
        _fireDelay = fireDelay;
        _energyCost = energyCost;
    }
    
    public void Tick()
    {
        _patrolRobot.transform.LookAt(_enemy.transform.position);
        if (ReadyToFire())
        {
            _patrolRobot.TakeEnergy(_energyCost);
            Fire();
        }
        
    }
    private bool ReadyToFire()
    {
        return Time.time >= _nextFireTime;
    }
    private void Fire()
    {
        _nextFireTime = Time.time + _fireDelay;

        var shot = Object.Instantiate(_blasterShot, _patrolRobot.firePoint.position, _patrolRobot.transform.rotation);
        shot.Launch(_patrolRobot.transform.forward);
    }

    public void OnEnter()
    {
        _enemy = _patrolRobot.visionCone.enemySpotted;
        _patrolRobot.textMeshPro.text = "Attack !";
        _patrolRobot.navMeshAgent.ResetPath();
    }
    
    public void OnExit()
    {
       
    }
}