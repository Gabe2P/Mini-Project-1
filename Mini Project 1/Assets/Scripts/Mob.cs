using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Mob : MonoBehaviour, Copyable
{
    //public float maxHealth = 10f;
    //[SerializeField]
    //private float curHealth = 0f;

    //[SerializeField]
    //private float deathTime = 2f;

    //private Animator anim;

    //private EnemyController controller;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    controller = GetComponentInParent<EnemyController>();
    //    curHealth = maxHealth;
    //    anim = GetComponentInChildren<Animator>();
    //    anim.SetBool("isMoving", false);
    //    anim.SetBool("isAttacking", false);
    //    anim.SetBool("isDead", false);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (curHealth <= 0)
    //    {
    //        Debug.Log("I Should be Dead");
    //        anim.SetBool("isDead", true);
    //        if (deathTime <= 0)
    //        {
    //            controller.Destroy();
    //        }
    //        else
    //        {
    //            deathTime -= Time.deltaTime;
    //        }
    //    }
    //}

    //public void OnTriggerEnter(Collider other)
    //{

    //    Obtainable obj = other.transform.GetComponentInParent<Obtainable>();
    //    //&& other.transform.gameObject.layer == PlayerManager.instance.player.transform.gameObject.layer


    //    if (obj != null && !obj.isBeingHeld())
    //    {
    //        Debug.Log("Ive Been Hit");
    //        float _damage = obj.GetPower() * .1f;
    //        TakeDamage(_damage);
    //    }

    //    controller.SetCurLookRadius(controller.maxLookRadius);
    //}

    //public void TakeDamage(float damage)
    //{
    //    curHealth -= damage;
    //}

    public Copyable Copy()
    {
        return Instantiate(this);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
