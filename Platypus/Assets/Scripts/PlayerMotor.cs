using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

/*
 * Player Motor for controlling Player
 */

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    //Variables
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float camRotLimit = 85f; //camera Rotation Limit

    private Vector3 velocity = Vector3.zero; //velocity of player initially
    private Vector3 rotation = Vector3.zero; //rotation
    private float cameraRotationX = 0f; //camera rotation X
    private float cameraRotationY = 0f; //camera roatation Y
    private float currentCameraRotationX = 0f;
    private float currentCameraRotationY = 0f;
    public float sizeInY = 1.0f; //size of object in Y for RayCasting
    private float jumpReadyTime = 0.0f; //jump ready time
    public float jumpCoolDownTime = 0.0f; //jump cooldown time

    public float jumpForce = 7.0f; //jump force
    public float fallMultiplier = 2f; //fall speed if in mid-air
    public float lowJumpMultiplier = 2f; //low jump multiplier
    public LayerMask groundLayers; //layers for ground
    private bool jumpButtonPressed = false; //bool for checking if player has jumped
    private bool jumping = false;
    public float CameraReturnSpeed = 500.0f;

    public float xCamRotation = 45f; //xCamera Rotation

    private XboxController controller; //getting instance of XBox Controller

    private Rigidbody rb;

    //Functions
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //gets component for Rigidbody
    }

    /// <summary>
    ///  Fixed Update
    /// </summary>
    private void FixedUpdate()
    {
        PerformMovement(); //performs movement function
        PerformRotation(); //performs rotation function
        PerformCamRotation(); //performs Camera Rotaion
    }

    /// <summary>
    /// changes velocity depending on what's passed in from Player Controller
    /// </summary>
    /// <param name="_velocity"></param>
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    /// <summary>
    /// Rotates Camera depending on mouse/controller movement from PlayerController
    /// </summary>
    /// <param name="_cameraRotationX"></param>
    public void RotateCameraX(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    /// <summary>
    /// Rotates Camera depending on mouse/controller movement from PlayerController
    /// </summary>
    /// <param name="_cameraRotationY"></param>
    public void RotateCamera(float _cameraRotationY)
    {
        cameraRotationY = _cameraRotationY;
    }

    /// <summary>
    /// Rotates Player depending on left stick/default
    /// </summary>
    /// <param name="_rotation"></param>
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    /// <summary>
    /// bool to check if player is grounded
    /// </summary>
    /// <returns>raycast if player is on the ground</returns>
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, sizeInY);
    }

    /// <summary>
    /// Movement Function
    /// </summary>
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); //if velocity changes add velocity change to Rigidbody
        }

        //script to check if player can jump, convoluted set of code for a jump delay setting
        if ((Input.GetButtonDown("Jump") || XCI.GetButtonDown(XboxButton.A, controller)) && isGrounded() )
        {
            jumpReadyTime = jumpCoolDownTime; //jump ready time is set for jump delay
            jumpButtonPressed = true; //detects that jump has been pressed
        }

        //cools down jump time as long as jump button has been pressed
        if (jumpReadyTime > 0 && jumpButtonPressed)
        {
            jumpReadyTime -= Time.deltaTime;
        }
        else if (jumpButtonPressed) //checks if jump button has been pressed
        {
            jumping = true; //sets player to jump after delays
            jumpButtonPressed = false;
        }

        //check if player is jumping
        if (jumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //jumps player
            jumping = false; //debugs all else to false
            jumpButtonPressed = false;
        }

        //pulls player back to ground at faster velocity than gravity
        if (rb.velocity.y < 0)
        {
            //fallMultiplier is to be changed to increase/decrease fall speed
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump") || !XCI.GetButtonDown(XboxButton.A, controller))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// rotates player function
    /// </summary>
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }

    /// <summary>
    /// rotates camera function
    /// </summary>
    void PerformCamRotation()
    {
        if (cam != null)
        {
            if (XCI.GetAxis(XboxAxis.RightStickX, controller) == 0)
            {
                cam.transform.localEulerAngles = new Vector3(xCamRotation,0f,0f);
                currentCameraRotationX = 0f;
                //Debug.Log("zero");

                //if (currentCameraRotationX > transform.forward.x)
                //currentCameraRotationX += transform.forward.x * CameraReturnSpeed;

                //if (currentCameraRotationX < transform.forward.x)
                //    currentCameraRotationX -= transform.forward.x * CameraReturnSpeed;

                //if (currentCameraRotationX == transform.forward.x)
                //    currentCameraRotationX = transform.forward.x;
            }
            else
            {
                currentCameraRotationX -= cameraRotationX;

                currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -camRotLimit, camRotLimit);
                cam.transform.localEulerAngles = new Vector3(xCamRotation, currentCameraRotationX, 0f);
            }
            //
            
        }
    }
}
