using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Gabriel Tupy 9/3/2019 @ 6:56pm

[RequireComponent(typeof(Rigidbody))]
public class Obtainable : MonoBehaviour
{
    Transform parent = null;
    Rigidbody rb = null;

    private bool isheld = false;

    LayerMask ogLayer;
    private float curTime = 0f;
    public float timer = 1f;

    private float lastPower = 0f;

    private void Start()
    {
        parent = gameObject.transform.parent;
        rb = gameObject.GetComponent<Rigidbody>();
        ogLayer = gameObject.layer;
    }

    public void Update()
    {
        if (gameObject.transform.parent == parent)
        {
            if (gameObject.layer != ogLayer)
            {
                if (curTime < timer)
                {
                    curTime += Time.fixedDeltaTime;
                }
                else
                {
                    curTime = 0;
                    gameObject.layer = ogLayer;
                }
            }
        }
        else
        {
            curTime = 0;
        }

    }

    public void Activate(Camera cam)
    {
        rb.isKinematic = true;
        //rb.useGravity = false;
        gameObject.transform.SetParent(cam.transform);
        gameObject.layer = cam.gameObject.layer;
        isheld = true;
    }
    public void Deactivate()
    {
        rb.isKinematic = false;
        //rb.useGravity = true;
        gameObject.transform.SetParent(parent);
        isheld = false;
    }

    public void Throw(Camera cam, float power)
    {
        Deactivate();
        lastPower = power;
        rb.AddForce(cam.transform.forward * power, ForceMode.Impulse);
    }

    public float GetPower()
    {
        return lastPower;
    }

    public bool isBeingHeld()
    {
        return isheld;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
