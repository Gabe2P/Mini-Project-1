using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Gabriel Tupy 9/3/2019 @ 6:56pm

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 jumpVelocity = Vector3.zero;

    private Vector3 velocity = Vector3.zero;

    private Vector3 rotation = Vector3.zero;

    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    [SerializeField]
    private float InteractionRange = 1f;
    private bool isActive = false;

    private bool isCrouched = false;

    public float CrouchOffset = 1f;

    private Vector3 crouchedHeight;
    private Vector3 PermHeight;


    public List<Transform> inventory;


    [SerializeField]
    private float cameraRotationLimit = 80f;

    

    [SerializeField]
    private LayerMask GroundLayer;

    [SerializeField]
    private LayerMask PropLayer;

    public SphereCollider GroundCheck;

    public CapsuleCollider Hitbox;

    private Rigidbody rb;
      

    // Start is called before the first frame update
    void Start()
    {
        PermHeight = cam.transform.position;
        crouchedHeight = new Vector3(PermHeight.x, PermHeight.y - CrouchOffset, PermHeight.z);
        rb = GetComponent<Rigidbody>();
    }

    public void Throw(float power)
    {
        //Item checks
        Obtainable heldItem = cam.transform.GetComponentInChildren<Obtainable>();

        if (heldItem != null)
        {
            heldItem.Throw(cam, power);
        }
    }

    public void Activate()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, InteractionRange, PropLayer))
        {
            //Name of the items


            //Item checks
            Obtainable heldItem = cam.transform.GetComponentInChildren<Obtainable>();
            Obtainable Obj = hitInfo.transform.GetComponentInParent<Obtainable>();
            Unlockable _lock = hitInfo.transform.GetComponentInParent<Unlockable>();
            Rotating_Door Door = hitInfo.transform.GetComponentInParent<Rotating_Door>();

            //Instance Checks and Logic
            if (Door != null)
            {
                if (_lock != null)
                {
                    foreach (Transform item in inventory)
                    {
                        _lock.Deactivate(item);
                    }
                    Door.Activate();

                }
                else
                {
                    Door.Activate();
                }
            }

            if (Obj != null)
            {
                if(Obj.CompareTag("Collectable"))
                {
                    inventory.Add(Obj.transform);
                    Obj.Destroy();
                    Debug.Log(inventory);
                }
            }


            if (heldItem == null)
            {
                if (Obj != null)
                {
                    Obj.Activate(cam);
                }
            }
            else
            {
               heldItem.Deactivate();
            }
        }
    }

    
    //Gets a Crouch input
    public void Crouch()
    {
        Hitbox.enabled = false;
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - CrouchOffset, cam.transform.position.z);
    }

    public void unCrouch()
    {
        Hitbox.enabled = true;
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + CrouchOffset, cam.transform.position.z);
    }


    //Gets a jump movement vector
    public void Jump(Vector3 _jumpVelocity)
    {
        jumpVelocity = _jumpVelocity;
    }

    //Gets a mouse movement Vector
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    //Gets a mouse movement Vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // Gets a movement Vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PerformMovement();
        PerformJump();
        PerformRotation();
    }


    //Ground Collision Check
    private bool isGrounded()
    {
        return Physics.CheckCapsule(GroundCheck.bounds.center, new Vector3(GroundCheck.bounds.center.x, GroundCheck.bounds.min.y, GroundCheck.bounds.center.z), GroundCheck.radius * .9f, GroundLayer);
    }

    //Perform Jump
    void PerformJump()
    {
        if (jumpVelocity != Vector3.zero && isGrounded())
        {
            rb.AddForce(jumpVelocity, ForceMode.Impulse);
        }
    }

    //perform movement
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    //perform rotation/mouse movement
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (cam != null)
        {
            //Set Rotation and Clamp Rotation
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);


            //Applying the Camera Rotation
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

}
