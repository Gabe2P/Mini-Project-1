using UnityEngine;

public class CursorLock : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("Alpha1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown("Alpha2"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } 
        
    }
}
