using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]                                                                        //private variables are serialized to be editable in the editor, but uneditable by other classes
    private float speed = 5f;                                                               //player speed variable
    [SerializeField]                                                                        //private variables are serialized to be editable in the editor, but uneditable by other classes
    private float Sensitivity = 3f;                                                         //look sensitivity
    [SerializeField]
    private float rotSpeed = 3f;
                                                                                            //Rotation Speed
    private PlayerMotor motor;                                                              //player motor class instantitiated as variable motor
    private XboxController controller;                                                      //XBox Controller instant

    /*on first run of Player Controller*/
    private void Start()
    {
        motor = GetComponent<PlayerMotor>();                                                //gets components of Player Motor

       // Cursor.lockState = CursorLockMode.Confined;                                         //locks cursor on center of game screen
       // Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        int numberOfControllers = XCI.GetNumPluggedCtrlrs();

        float _xMov;
        float _zMov;

        if (numberOfControllers >= 1)
        {
            _xMov = XCI.GetAxis(XboxAxis.LeftStickX, controller);                           //returns the raw float value of movement in the x axis
            _zMov = XCI.GetAxis(XboxAxis.LeftStickY, controller);                           //returns the raw float value of movement in the z axis
        }
        else
        {
            _xMov = Input.GetAxis("Horizontal");                                            //returns the raw float value of movement in the x axis
            _zMov = Input.GetAxis("Vertical");                                              //returns the raw float value of movement in the z axis
        }

        Vector3 _movHorizontal = Vector3.zero * _xMov;                                      //translates _xMov into the going to be position of the player in relation to the x axis
        Vector3 _movVertical = transform.forward * _zMov;                                   //translates _zMov into the going to be position of the player in relation to the z axis
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;             //then translates the values of movHorizontal and mov Vertical into the velocity of the player with relation to player speed

        motor.Move(_velocity);                                                              //and sends the value of velocity into player motor to move the player

        float _xRot;

        /*checks if xbox is being used*/
         if (numberOfControllers >= 1)
        {
            //moves player according to axis of left stick
            _xRot = XCI.GetAxis(XboxAxis.LeftStickX, controller);
        }
        else
        {
            //if Xbox Controller is not detected use default axis controls
            _xRot = Input.GetAxisRaw("Horizontal"); 
        }

        Vector3 _rotation = new Vector3(0f, _xRot, 0f) * rotSpeed;                       //sets the value of y_rot as the rotation in relation to the y-axis with a look speed from sensitivity


        motor.Rotate(_rotation);                                                         //sends the value of rotation to playermotor to rotate the player in the y-axis

        float _camRot;
        float _cameraRotationY;

        float _camRotX;
        float _cameraRotationX;
        
        if (numberOfControllers >= 1)
        {
            //moves player according to axis of left stick
            _camRot = XCI.GetAxis(XboxAxis.RightStickY, controller);                  //gets value of mouse Y-axis and stores it in xRot
            _cameraRotationY = _camRot * Sensitivity;                                    //multiplies xRot with the mouse sensitivity to get the value of camera rotation in the x_axis

            _camRotX = XCI.GetAxis(XboxAxis.RightStickX, controller);
            _cameraRotationX = _camRotX * Sensitivity;
        }
        else
        {
            //moves player through default if Xbox Controller is not detected
            _camRot = Input.GetAxisRaw("Mouse Y");                                      //gets value of mouse Y-axis and stores it in xRot
            _cameraRotationY = _camRot * Sensitivity;                                   //multiplies xRot with the mouse sensitivity to get the value of camera rotation in the x_axis

            _camRotX = Input.GetAxisRaw("Mouse X");
            _cameraRotationX = _camRotX * Sensitivity;
        }

        motor.RotateCameraX(-_cameraRotationX);                                         //sends the value of camera rotation x to player motor to rotate player in the x axis
        motor.RotateCamera(-_cameraRotationY);

        //if (Input.GetKeyDown(KeyCode.Escape))                                         //checks if escape button is pressed
        //{
        //    Quit();                                                                   //if yes, call on Quit function
        //}
    }

    /*Quit Function*/
    public void Quit()
    {
    #if UNITY_EDITOR                                                                //checks if in Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;                            //if yes, then close the Editor Simulation Application, else, then quit
    #else
        Application.Quit();                                                      
    #endif
    }
}
