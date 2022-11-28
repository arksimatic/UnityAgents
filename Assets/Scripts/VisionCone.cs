using System;
using UnityEngine;
using UnityEngine.Events;

public class VisionCone : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Vector3> onEnemySpotted;
    public MeshRenderer meshRenderer;
    public Color red;
    public Color yellow;
    public bool enemySpotted;
    private float _timeInRange;
    public float TimeInRange => _timeInRange;

    private void OnTriggerEnter(Collider other)
    {
        onEnemySpotted.Invoke(other.transform.position);
        enemySpotted = true;
        ChangeColorToRed();
    }

    private void OnTriggerStay(Collider other)
    {
        _timeInRange += Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit");
        enemySpotted = false;
        _timeInRange = 0;
        ChangeColorToYellow();
    }
    private void ChangeColorToRed()
    {
        meshRenderer.material.color = red;
    }
    
    private void ChangeColorToYellow()
    {
        meshRenderer.material.color = yellow;
    }
}