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
    [SerializeField] protected int maxHungeryLevel = 100;
    [SerializeField] protected int minHungarLevel = 30;
    [SerializeField] protected int hungaryLossPerSecond = 1;
    [SerializeField] private const int defaultFoodValue = 100;
    [SerializeField] protected List<Behavior> behaviors; //TODO make it one again 
    [SerializeField] protected Slider hungarySlider;

    protected GameObject WOLVES;
    protected GameObject SHEEPS;
    protected GameObject MEALS;

    protected virtual void Start()
    {
        hungary = maxHungeryLevel;
        InvokeRepeating(nameof(SelectNewDestination), 2f, 2f);
        StartCoroutine(HungaryMore());
        hungarySlider.GetComponent<Slider>().maxValue = maxHungeryLevel;
        WOLVES = GameObject.Find("WOLVES");
        SHEEPS = GameObject.Find("SHEEPS");
        MEALS = GameObject.Find("MEALS");
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
        return hungary < minHungarLevel ? true : false;
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

    protected void Eat()
    {
        hungary = maxHungeryLevel;
    }
}