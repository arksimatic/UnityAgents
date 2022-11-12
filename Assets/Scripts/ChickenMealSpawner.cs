using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChickenMealSpawner : MonoBehaviour
{
   [SerializeField] private GameObject chickenMealPrefab;
   [SerializeField] private GameObject chickenMealParent;
   
   private void Start()
   {
      InvokeRepeating(nameof(SpawnMeal), 2f, 3f);
   }

   private void SpawnMeal()
   {
        var newChickenMeal = Instantiate(chickenMealPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
        newChickenMeal.transform.parent = chickenMealParent.transform;
   }
}
