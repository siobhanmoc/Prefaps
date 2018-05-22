using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerAttack : MonoBehaviour
{

    private XboxController controller;

    public Transform armSpawnLeft = null;
    public Transform armSpawnRight = null;
    public Transform legSpawnLeft = null;
    public Transform legSpawnRight = null;
    public GameObject attackSphereLeftPunch = null;
    public GameObject attackSphereRightPunch = null;
    public GameObject attackSphereLeftKick = null;
    public GameObject attackSphereRightKick = null;

    private GameObject LeftPunch = null;
    private GameObject RightPunch = null;
    private GameObject LeftKick = null;
    private GameObject RightKick = null;

    [SerializeField]
    private float forceOnFire = 1.0f;
    [SerializeField]
    private float cooldownTimeLA = 0.0f;
    [SerializeField]
    private float cooldownTimeRA = 0.0f;
    [SerializeField]
    private float cooldownTimeLL = 0.0f;
    [SerializeField]
    private float cooldownTimeRL = 0.0f;
    [SerializeField]
    private float range = 0.0f;

    float rangeTimer = 1f;

    bool punchedLA = false;
    bool punchedRA = false;
    bool punchedLL = false;
    bool punchedRL = false;

    float cooldownTimerLA = 0f;
    float cooldownTimerRA = 0f;
    float cooldownTimerLL = 0f;
    float cooldownTimerRL = 0f;

    float spawnLengthLA = 0f;
    float spawnLengthRA = 0f;
    float spawnLengthLL = 0f;
    float spawnLengthRL = 0f;

    float kickLL = 0f;
    float kickRL = 0f;

    public float timer = 0f;

    private void Update()
    {

        if ((Input.GetMouseButtonDown(0) || XCI.GetButtonDown(XboxButton.LeftBumper)) && cooldownTimerLA <= 0)
        {
            LeftPunch = Instantiate(attackSphereLeftPunch, armSpawnLeft.position, armSpawnLeft.rotation);

            if (LeftPunch == null)
                return;

            Rigidbody rbLeftPunch = LeftPunch.GetComponent<Rigidbody>();

            if (rbLeftPunch == null)
                return;

            rbLeftPunch.AddForce(LeftPunch.transform.forward * forceOnFire);

            cooldownTimerLA = cooldownTimeLA;

            spawnLengthLA = cooldownTimeLA;
        }
        
        if (spawnLengthLA > 0)
        {
            spawnLengthLA -= Time.deltaTime;
        }
        else
        {
            Destroy(LeftPunch);
        }

        timer = spawnLengthLA;


        if ((Input.GetMouseButtonDown(1) || XCI.GetButtonDown(XboxButton.RightBumper)) && cooldownTimerRA <= 0)
        {
            RightPunch = Instantiate(attackSphereRightPunch, armSpawnRight.position, armSpawnRight.rotation);

            if (RightPunch == null)
                return;

            Rigidbody rbRightPunch = RightPunch.GetComponent<Rigidbody>();

            if (rbRightPunch == null)
                return;

            rbRightPunch.AddForce(RightPunch.transform.forward * forceOnFire);

            cooldownTimerRA = cooldownTimeRA;

            spawnLengthRA = cooldownTimeRA;
        }
        
        if (spawnLengthRA > 0)
        {
            spawnLengthRA -= Time.deltaTime;
        }
        else
        {
            Destroy(RightPunch);
        }

        if (XCI.GetAxis(XboxAxis.LeftTrigger) > 0)
        {
            punchedLL = true;
            kickLL++;
        }

        if (XCI.GetAxis(XboxAxis.LeftTrigger) <= 0)
        {
            punchedLL = false;
        }

        if ((punchedLL) && kickLL == 1 && cooldownTimerLL <= 0)
        {
            LeftKick = Instantiate(attackSphereLeftKick, legSpawnLeft.position, legSpawnLeft.rotation);

            if (LeftKick == null)
                return;

            Rigidbody rbLeftKick = LeftKick.GetComponent<Rigidbody>();

            if (rbLeftKick == null)
                return;

            rbLeftKick.AddForce(LeftKick.transform.forward * forceOnFire);

            cooldownTimerLL = cooldownTimeLL;

            spawnLengthLL = cooldownTimeLL;
        }

        if (spawnLengthLL > 0)
        {
            spawnLengthLL -= Time.deltaTime;
        }
        else
        {
            Destroy(LeftKick);
            kickLL = 0;
        }

        if (XCI.GetAxis(XboxAxis.RightTrigger) > 0)
        {
            punchedRL = true;
            kickRL++;
        }

        if (XCI.GetAxis(XboxAxis.RightTrigger) <= 0)
        {
            punchedRL = false;
        }

        if ((punchedRL) && kickRL == 1 && cooldownTimerRL <= 0)
        {
            RightKick = Instantiate(attackSphereRightKick, legSpawnRight.position, legSpawnRight.rotation);

            if (RightKick == null)
                return;

            Rigidbody rbRightKick = RightKick.GetComponent<Rigidbody>();

            if (rbRightKick == null)
                return;

            rbRightKick.AddForce(RightKick.transform.forward * forceOnFire);

            cooldownTimerRL = cooldownTimeRL;

            spawnLengthRL = cooldownTimeRL;
        }

        if (spawnLengthRL > 0)
        {
            spawnLengthRL -= Time.deltaTime;
        }
        else
        {
            Destroy(RightKick);
            kickRL = 0;
        }

        if (cooldownTimerLA > 0)
            cooldownTimerLA -= Time.deltaTime;

        if (cooldownTimerRA > 0)
            cooldownTimerRA -= Time.deltaTime;

        if (cooldownTimerLL > 0)
            cooldownTimerLL -= Time.deltaTime;

        if (cooldownTimerRL > 0)
            cooldownTimerRL -= Time.deltaTime;

    }
}