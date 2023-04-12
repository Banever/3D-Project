using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float Speed = 20f;

    void Start()
    {
        Rigidbody.velocity = transform.forward * Speed;
    }

    void OnTriggerEnter2D(Collider2D Collision)
    { 
            Destroy(gameObject);
    }
}
