using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Gabriel Tupy 9/3/2019 @ 6:56pm
public class Unlockable : MonoBehaviour
{
    public Transform key = null; 

    [SerializeField]
    private bool _isActive = true;

    public bool isActive()
    {
        return _isActive;
    }


    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate(Transform _key = null)
    {
        if (key == null)
        {
            _isActive = false;
        }
        else
        {
            if (_key.Equals(key))
            {
                _isActive = false;
            }
        }
    }
}
