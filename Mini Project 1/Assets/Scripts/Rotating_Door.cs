using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Gabriel Tupy 9/3/2019 @ 6:56pm
public class Rotating_Door : MonoBehaviour
{
    public float offset = 0f;
    public float maxRotation = 90f;
    public float rotationSpeed = 50f;

    private float curRotation = 0;

    [SerializeField]
    private bool _isActive = false;

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            curRotation += Time.deltaTime * rotationSpeed;
            curRotation = Mathf.Clamp(curRotation, offset, offset + maxRotation);
            transform.localEulerAngles = new Vector3(0f, curRotation, 0f);
        }
        else
        {
            curRotation -= Time.deltaTime * rotationSpeed;
            curRotation = Mathf.Clamp(curRotation, offset, offset + maxRotation);
            transform.localEulerAngles = new Vector3(0f, curRotation, 0f);
        }
    }

    public void Activate()
    {
        Unlockable _lock = gameObject.transform.GetComponent<Unlockable>();


        if (_lock != null)
        {
            if (_lock.isActive() == false)
            {
                if (_isActive == true)
                {
                    _isActive = false;
                }
                else
                {
                    _isActive = true;
                }
            }
        }
        else
        {
            if (_isActive == true)
            {
                _isActive = false;
            }
            else
            {
                _isActive = true;
            }
        }
    }
}
