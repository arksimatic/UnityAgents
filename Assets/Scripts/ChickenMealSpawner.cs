using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChickenMealSpawner : MonoBehaviour
{
   [SerializeField] private GameObject chickenMealPrefab;

   private void Start()
   {
      InvokeRepeating(nameof(SpawnMeal), 2f ,3f);
   }

   private void SpawnMeal()
   {
      Instantiate(chickenMealPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
   }
}
