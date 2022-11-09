using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WolfFlock : MonoBehaviour
{
    public static WolfFlock Instance;
    public List<(Agent intrester, Agent target )> WolfSheepList;
    public Vector3 flockPosition;

    public List<Wolf> wolves;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this); 
        }
        
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        wolves = FindObjectsOfType<Wolf>().ToList();
        foreach (var wolf in wolves)
        {
            flockPosition += wolf.transform.position;
        }
        flockPosition /= wolves.Count;
        
    }
}