using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Agent : MonoBehaviour
{
    public int hungary;
    [SerializeField] private int maxHungeryLevel = 100;
    [SerializeField] private int minHungarLevel = 30;
    [SerializeField] private int hungaryLossPerSecond = 1;
    private const int defaultFoodValue = 100;
    [SerializeField] private List<Behavior> behaviors; //TODO make it one again 
    [SerializeField] private Slider hungarySlider;

    protected virtual void Start()
    {
        hungary = maxHungeryLevel;
        InvokeRepeating(nameof(SelectNewDestination), 2f, 2f);
        StartCoroutine(HungaryMore());
        hungarySlider.GetComponent<Slider>().maxValue = maxHungeryLevel;
    }

    protected virtual void Awake()
    {
        
    }

    protected virtual void Update()
    {
        hungarySlider.value = hungary;
        if (hungary < 0)
            Die();
    }

    protected void SelectNewDestination()
    {
        GetComponent<NavMeshAgent>().destination = behaviors.First().target;
    }

    protected bool IsHungry()
    {
        if (hungary < minHungarLevel)
        {
            return true;
        }

        return false;
    }

    protected IEnumerator HungaryMore()
    {
        while (true)
        {
            hungary -= hungaryLossPerSecond;
            yield return new WaitForSeconds(1);
        }
    }

    protected void Eat(int foodValue = defaultFoodValue)
    {
        int newHungaryLevel = Math.Min(hungary + foodValue, maxHungeryLevel);
        hungary = newHungaryLevel;
    }

    protected void Die()
    {
        Destroy(this.gameObject);
    }
}