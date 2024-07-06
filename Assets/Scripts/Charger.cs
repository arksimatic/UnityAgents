using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Charger : MonoBehaviour
{
    private List<PatrolRobot> robots;
    [SerializeField] private float chargeDistance = 2f;

    private void Awake()
    {
        
    }
    
    private void Update()
    {
        robots = FindObjectsOfType<PatrolRobot>().ToList();
        foreach (var patrolRobot in robots.Where(robot=>Vector3.Distance(transform.position,robot.transform.position)<=chargeDistance))
        {
            patrolRobot.Recharge();
        }
    }
}