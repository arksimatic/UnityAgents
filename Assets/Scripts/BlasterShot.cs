using UnityEngine;

public class BlasterShot : MonoBehaviour
{
    [SerializeField] float _speed = 15f;

    public void Launch(Vector3 direction)
    {
        direction.Normalize();
        transform.up = direction;
        GetComponent<Rigidbody>().linearVelocity = direction * _speed;
    }
    
    private void Start()
    {
        Destroy(gameObject,5f);
    }
}