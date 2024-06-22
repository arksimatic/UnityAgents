using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


public class Recharge:IState
{
    private readonly PatrolRobot _patrolRobot;
    private bool chargerIsNotSelected;
    private Charger _selectedCharger;
    public Recharge(PatrolRobot patrolRobot)
    {
        _patrolRobot = patrolRobot;

    }
    public void Tick()
    {
        if (!_selectedCharger)
        {
            SelectCharger();
        }
    }
    private void SelectCharger()
    {
        _selectedCharger = Object.FindObjectsOfType<Charger>().OrderBy(charger=>Vector3.Distance(charger.transform.position,_patrolRobot.transform.position)).First();
        _patrolRobot.SetDestination(_selectedCharger.transform.position);
    }
    public void OnEnter()
    {
        _patrolRobot.textMeshPro.text = "Recharge!";
    }
    public void OnExit()
    {
        
    }
}