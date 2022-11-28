using System;
using UnityEngine;
using UnityEngine.Events;

public class PatrolPoint: MonoBehaviour
{
    [HideInInspector] public UnityEvent onRobotEnter;

    private void OnTriggerEnter(Collider other)
    {
        onRobotEnter.Invoke();
    }
}