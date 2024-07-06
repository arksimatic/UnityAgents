using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RobotSpawner : MonoBehaviour
{
    public Vector3 SpawnFrom;
    public Vector3 SpawnTo;
    public GameObject RobotPrefab;
    public Int32 RobotCount;
    public GameObject patrolPointsObj;

    public void Start()
    {
        List<PatrolPoint> patrolPoints = new List<PatrolPoint>();

        foreach(Transform ppChild in patrolPointsObj.transform)
        {
            var test = ppChild.gameObject.GetComponent<PatrolPoint>();
            patrolPoints.Add(ppChild.GetComponent<PatrolPoint>());
        }

        for(int i = 0; i < RobotCount; i++)
        {
            var newRobot = Instantiate(RobotPrefab, this.transform.position, Quaternion.identity);
            newRobot.transform.position = new Vector3(Random.Range(SpawnFrom.x, SpawnTo.x), Random.Range(SpawnFrom.y, SpawnTo.y), Random.Range(SpawnFrom.z, SpawnTo.z));
            newRobot.transform.parent = this.transform;
            newRobot.GetComponent<PatrolRobot>().patrolPoints = patrolPoints.ToList();
            newRobot.GetComponent<PatrolRobot>().SetStateMachine();
            newRobot.GetComponent<PatrolRobot>().enabled = true;
            //newRobot.GetComponent<PatrolRobot>();
        }
    }
}
