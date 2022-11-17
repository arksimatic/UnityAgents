using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sheep:Agent
{
    public TMP_Text info;
    
    private List<string> _names = new List<string>
    {
        "Sophia",
        "Sebastian",
        "Samuel",
        "Sofia",
        "Scarlett",
        "Santiago",
        "Stella",
        "Sawyer",
    };
    
    protected override void Awake()
    {
        info.text = _names[Mathf.FloorToInt(Random.Range(0, _names.Count))];
    }

    public void Eat(ChickenMeal meal)
    {
        base.Eat();
        Destroy(meal);
    }
}