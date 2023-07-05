using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = transform.forward * bulletSpeed;  //As soon as Bullet is activated its starts moving
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle collision with other objects if needed
        // For example, you can deactivate the bullet here
        gameObject.SetActive(false);
    }


}
