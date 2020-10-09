using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class MyEnemyMotor : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    private Rigidbody rb;
    private Collider hitbox;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hitbox = GetComponentInChildren<Collider>();
    }

    public void Move()
    {
        Debug.Log("Move");
    }

    public void Idle()
    {
        velocity = Vector3.zero;
    }

    public void MoveTowards(Vector3 destination, float moveSpeed)
    {
        Vector3 _velocity = (destination - rb.position).normalized * moveSpeed;

        if (rb.position + _velocity == destination)
        {
            velocity = Vector3.zero;
        }
        else
        {
            velocity = _velocity;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PerformMove();
    }

    private void PerformMove()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        else
        {
            rb.position = rb.position;
        }
    }
}
