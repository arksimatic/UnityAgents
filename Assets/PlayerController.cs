using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask aimLayerMask;
    public float speed;
    private Animator _animator;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    private void Awake() => _animator = GetComponent<Animator>();

    private void Update()
    {
        AimTowardMouse();
        Move();
    }
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement *= Time.deltaTime * speed;

        transform.Translate(movement,Space.Self);
        
        _animator.SetFloat(Horizontal,horizontal,0.1f,Time.deltaTime);
        _animator.SetFloat(Vertical,vertical,0.1f,Time.deltaTime);
    }
    private void AimTowardMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo,  Mathf.Infinity, aimLayerMask))
        {
            var destination = hitInfo.point;
            var position = transform.position;
            destination.y = position.y;
            var direction = destination - position;
            direction.Normalize();
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
