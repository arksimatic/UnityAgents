    using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [SerializeField] private List<Behavior> behaviors; //TODO make it one again 
    void Start()
    {
        InvokeRepeating(nameof(SelectNewDestination),2f,2f);
    }

    private void SelectNewDestination()
    {
        GetComponent<NavMeshAgent>().destination = behaviors.First().target;
    }

}