using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRobot : MonoBehaviour
{
    private GameObject _alarmInstance;
    private StateMachine _stateMachine;
    private bool _goForEnemy;
    private int _energy;
    private bool _countDownStarted;
    private float _t;
    
    [SerializeField] private TextMeshPro energyStatus;
    [SerializeField] private float beamRange = 15f;
    [SerializeField] private float timeForSearch = 3f;
    [SerializeField] private int maxEnergy = 100;
    [Range(0.1f,0.99f)]
    [SerializeField] private float percentageToRecharge;
    
    public List<PatrolPoint> patrolPoints = new();
    public Transform firePoint;
    public NavMeshAgent navMeshAgent;
    public VisionCone visionCone;
    public TextMeshPro textMeshPro;
    public BlasterShot blasterShotPrefab;
    public GameObject alarm;
    public bool notified;
    public float time;

    public Vector3 LastSeenEnemyLocation => _lastSeenEnemyLocation;
    private Vector3 _lastSeenEnemyLocation;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        var patrol = new Patrol(this, patrolPoints);
        var notify = new Notify(this);
        var goToLastSeenLocation = new GoToLastSeenLocation(this);
        var attack = new Attack(this,blasterShotPrefab,0.25f,5);
        var randomlySearchLocation = new RandomlySearchLocation(this);
        var recharge = new Recharge(this);
        
        AddAnyTransition(recharge,LowEnergy());
        AddTransition(recharge,patrol,Recharged());
        
        AddTransition(patrol,notify,EnemyIsVisible());
        AddTransition(patrol,goToLastSeenLocation,NotificationReceived());
        
        AddTransition(notify,attack,EnemyIsVisible());
        AddTransition(notify,goToLastSeenLocation,EnemyIsNotVisible());
        
        AddTransition(goToLastSeenLocation,randomlySearchLocation,TargetIsReachedButNoEnemyVisible());
        AddTransition(goToLastSeenLocation,attack,TargetIsReachedButEnemyIsVisible());
        AddTransition(goToLastSeenLocation,attack,EnemyIsVisible());
        
        AddTransition(attack,randomlySearchLocation,EnemyIsNotVisible());
        
        AddTransition(randomlySearchLocation,attack,EnemyIsVisible());
        AddTransition(randomlySearchLocation,patrol,SearchingIsOver());
        
        Func<bool> EnemyIsVisible() => () => visionCone.enemySpotted;
        Func<bool> EnemyIsNotVisible() => () => !visionCone.enemySpotted;
        Func<bool> NotificationReceived() => () => notified;
        Func<bool> LowEnergy() => () => _energy < maxEnergy * percentageToRecharge;
        Func<bool> Recharged() => () => _energy == maxEnergy;
        Func<bool> SearchingIsOver() => CountDown;
        Func<bool> TargetIsReachedButEnemyIsVisible() => () => Vector3.Distance(transform.position,_lastSeenEnemyLocation)<=3f && visionCone.enemySpotted;
        Func<bool> TargetIsReachedButNoEnemyVisible() => () => Vector3.Distance(transform.position,_lastSeenEnemyLocation)<=3f && !visionCone.enemySpotted;
        
        _stateMachine.SetState(patrol);
    }
    
    private bool CountDown()
    { 
        time += Time.deltaTime; 
        return time >= timeForSearch;
    }
    
    private void Start()
    {
        _alarmInstance = Instantiate(alarm, transform);
        _alarmInstance.SetActive(false);
        _energy = maxEnergy;
        
    }

    private void AddTransition(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
    private void AddAnyTransition(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

    public void SetDestination(Vector3 transformPosition)
    {
        navMeshAgent.SetDestination(transformPosition);
    }

    private void Update()
    {
        _stateMachine.Tick();
        energyStatus.text = _energy + "%";
    }

    public void SendSignal()
    {
        _alarmInstance.SetActive(true);
        
        var robots = FindObjectsOfType<PatrolRobot>().ToList();
        var enemy = FindObjectOfType<PlayerController>();
        robots.Remove(this);
        
        LeanTween.value(_alarmInstance, 0, beamRange, 1f)
            .setOnUpdate(value => 
            { 
                _alarmInstance.transform.localScale = new Vector3(value, value, value); 
                robots
                    .Where(robot => Vector3.Distance(_alarmInstance.transform.position, robot.transform.position) <= value)
                    .ToList()
                    .ForEach(r => r.Notify(enemy.transform.position)); 
            })
            .setOnComplete(() => _alarmInstance.SetActive(false));
    }
    
    private void Notify(Vector3 enemyPosition)
    {
        if (notified)
        {
            return;
        }

        notified = true;
        _lastSeenEnemyLocation = enemyPosition;
    }
    public void Recharge()
    {
        _t += Time.deltaTime / 10f;
        _energy = Mathf.RoundToInt(Mathf.Lerp(maxEnergy*percentageToRecharge, maxEnergy, _t));
        energyStatus.text = _energy + "%";
    }
    public void TakeEnergy(int energy)
    { 
        _energy -= energy;
        energyStatus.text = _energy + "%";
    }
}