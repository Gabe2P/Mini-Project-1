using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifeSpan = 2f;
    private float curLifeSpan;

    private Rigidbody rb;
    private SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        rb.velocity = transform.forward * speed;
    }

    private void Update()
    {
        if (curLifeSpan == lifeSpan)
        {
            Destroy(this.gameObject);
        }
        else
        {
            curLifeSpan += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Player Takes Damage");
            player.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
