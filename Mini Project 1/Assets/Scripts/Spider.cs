using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyHealth))]

public class Spider : Mob
{
    //Stats
    public float damage = 1f;
    public float attackSpeed = 1f;
    private float curTime= 0f;
    public float attackRange = .5f;
    public float fieldOfViewAngle = 110f;

    [SerializeField]
    private Animator anim;

    public float maxLookRadius = 10f;
    private float curLookRadius = 0f;
    private SphereCollider AwarenessCol;

    public LayerMask Character;

    Transform target;
    Transform player = PlayerManager.instance.player.transform;
    PlayerController playerController = PlayerManager.instance.player.GetComponent<PlayerController>();

    NavMeshAgent agent;

    private EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.freezeRotation = true;
        target = player;
        agent = GetComponent<NavMeshAgent>();
        AwarenessCol = GetComponent<SphereCollider>();
        health = GetComponentInChildren<EnemyHealth>();
        curLookRadius = maxLookRadius;
        AwarenessCol.radius = curLookRadius;
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isMoving", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDead", false);
    }

    private void Update()
    {
        if (target == null)
        {
            agent.isStopped = true;
            anim.SetBool("isMoving", false);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            agent.isStopped = false;
        }
    }

    void FixedUpdate()
    {
        if (health.isDead == false)
        {
            //Movement Calculations
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= curLookRadius)
            {
                anim.SetBool("isAttacking", false);
                anim.SetBool("isMoving", true);
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance + attackRange)
                {
                    anim.SetBool("isMoving", false);
                    FaceTarget();

                    if (curTime >= attackSpeed)
                    {
                        Attack();
                        curTime = 0;
                    }
                    else
                    {
                        curTime += Time.deltaTime;
                    }
                }
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    private void FaceTarget()
    {
        //Rotating and facing the target
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void Attack()
    {
        if (target == PlayerManager.instance.player.transform)
        {
            playerController = PlayerManager.instance.player.transform.GetComponent<PlayerController>();

            if (playerController != null)
            {
                Debug.Log("Attacking");
                anim.SetBool("isAttacking", true);
                playerController.TakeDamage(damage);
            }
        }
    }


    //Target Finding
    public void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.transform == PlayerManager.instance.player.transform)
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hitInfo, curLookRadius))
                {
                    if (hitInfo.collider.gameObject.transform.parent.transform == PlayerManager.instance.player.gameObject.transform)
                    {
                        target = PlayerManager.instance.player.transform;
                        FaceTarget();
                    }
                }
            }
        }
        else
        {
            Obtainable obj = other.transform.GetComponentInParent<Obtainable>();

            if (obj != null)
            {
                if (other.transform.parent.gameObject.layer == PlayerManager.instance.player.gameObject.layer)
                {

                    Vector3 direction = other.transform.position - transform.position;
                    float angle = Vector3.Angle(direction, transform.forward);

                    if (angle < fieldOfViewAngle * 0.5f)
                    {
                        RaycastHit hitInfo;

                        if (Physics.Raycast(transform.position, direction.normalized, out hitInfo, curLookRadius))
                        {
                            Debug.Log(hitInfo.collider.transform.parent.name);

                            if (hitInfo.collider.transform.parent == obj.transform)
                            {
                                if (!obj.isBeingHeld())
                                {
                                    target = other.transform;
                                    FaceTarget();
                                }
                                else
                                {
                                    target = null;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.transform == PlayerManager.instance.player.transform)
        {
            target = null;
        }
    }

    public void SetCurLookRadius(float radius)
    {
        curLookRadius = radius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, curLookRadius);

    }
}

