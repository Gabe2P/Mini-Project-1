using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 10f;
    [SerializeField]
    private float curHealth = 0f;

    [SerializeField]
    private float deathTime = 0.5f;

    [SerializeField]
    private float animTime;

    public bool isDead = false;
    
    public Animator anim;

    private Mob controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<Mob>();
        curHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isMoving", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("takeDamage", false);
        anim.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth <= 0)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            Invoke("Destroy", deathTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        Obtainable obj = other.transform.GetComponentInParent<Obtainable>();
        //&& other.transform.gameObject.layer == PlayerManager.instance.player.transform.gameObject.layer


        if (obj != null && !obj.isBeingHeld())
        {
            Debug.Log("Ive Been Hit");
            float _damage = obj.GetPower() * .1f;
            TakeDamage(_damage);
            Invoke("TurnOffAnim", animTime);
        }
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        anim.SetBool("takeDamage", true);
    }

    private void TurnOffAnim()
    {
        anim.SetBool("takeDamage", false);
    }

    public void Destroy()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
