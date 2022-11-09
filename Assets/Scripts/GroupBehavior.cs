using UnityEngine;

public class GroupBehavior : Behavior
{
    public float maxRadius = 5f;
    public override Vector3 target => new(WolfFlock.Instance.flockPosition.x + Random.Range(-maxRadius, maxRadius), 0, WolfFlock.Instance.flockPosition.z + Random.Range(-maxRadius, maxRadius));
}