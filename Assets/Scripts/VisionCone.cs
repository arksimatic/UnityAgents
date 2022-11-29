using System;
using UnityEngine;
using UnityEngine.Events;

public class VisionCone : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Vector3> onEnemySpotted;
    public MeshRenderer meshRenderer;
    public Color red;
    public Color yellow;
    public PlayerController enemySpotted;
    

    private void OnTriggerEnter(Collider other)
    {
        onEnemySpotted.Invoke(other.transform.position);
        enemySpotted = other.GetComponent<PlayerController>();
        ChangeColorToRed();
    }
    

    private void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit");
        enemySpotted = null;
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