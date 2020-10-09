using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class CheckPoint : MonoBehaviour
{
    private Transform playerTransform = PlayerManager.instance.transform;
    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.transform.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.SetCheckPoint(this.transform);
        }
    }
}
