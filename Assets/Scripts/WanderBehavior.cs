using UnityEngine;

public class WanderBehavior : Behavior
{
    [SerializeField] private Vector2 range;
    public override Vector3 target => new(Random.Range(-range.x, range.x), 0, Random.Range(-range.y, range.y));
}