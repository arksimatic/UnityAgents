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
        robots = FindObjectsOfType<PatrolRobot>().ToList();
    }
    
    private void Update()
    {
        foreach (var patrolRobot in robots.Where(robot=>Vector3.Distance(transform.position,robot.transform.position)<=chargeDistance))
        {
            patrolRobot.Recharge();
        }
    }
}