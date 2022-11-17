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

    protected override void Awake()
    {
        base.Awake();
        info.text = _names[Mathf.FloorToInt(Random.Range(0, _names.Count))];
    }

    protected override void Update()
    {
        base.Update();
        if (IsHungry())
        {
            SelectTargetToEat();
        }
    }
    private void SelectTargetToEat()
    {
        
    }

    public void Eat(Sheep poorSheepVictim)
    {
        base.Eat();
        Destroy(poorSheepVictim);
    }
}