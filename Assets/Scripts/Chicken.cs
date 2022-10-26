using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    [SerializeField] private List<Behavior> behaviors;
    void Start()
    {
        InvokeRepeating(nameof(SelectNewDestination),2f,2);
    }

    private void SelectNewDestination()
    {
        GetComponent<NavMeshAgent>().destination = behaviors.First().target;
    }

    
}