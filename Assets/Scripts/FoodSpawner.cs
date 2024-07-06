using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    public Vector3 SpawnFrom;
    public Vector3 SpawnTo;
    public GameObject FoodPrefab;
    public Int32 FoodCount;

    public void Start()
    {
        for(int i = 0; i < FoodCount; i++)
        {
            var newFood = Instantiate(FoodPrefab, 
                new Vector3(Random.Range(SpawnFrom.x, SpawnTo.x), Random.Range(SpawnFrom.y, SpawnTo.y), Random.Range(SpawnFrom.z, SpawnTo.z)), Quaternion.identity);
            newFood.transform.parent = this.transform;
        }
    }
}
