using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wolf: Agent
{
    public TMP_Text info;
    private List<string> _names = new List<string>
    {
        "William",
        "Wyatt",
        "Willow",
        "Waylon",
        "Wesley",
        "Weston",
        "Walker",
        "Wren",
        "WolfStreet",
    };

    public int hungary = 100;
    [SerializeField] private int minHungarLevel = 30;

    private void Awake()
    {
        info.text = _names[Mathf.FloorToInt(Random.Range(0, _names.Count))];
    }

  

    private void Update()
    {
        if (IsHungry())
        {
            SelectTargetToEat();
        }
    }
    private void SelectTargetToEat()
    {
        
    }
    private bool IsHungry()
    {
        if (hungary < minHungarLevel)
        {
            return true;
        }

        return false;
    }
}