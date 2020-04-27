using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Canvas gameView;
    public Canvas menuView;
    public bool movementEnabled = true;
    public bool jumpEnabled = true;

    private const int WallMenuLayer = 1 << 9;
    private const int PlayerShootsLayer = 1 << 8;
    private GameObject wallMenu = null;
    private int wallMenuID = 0;
    private float speed = 5.0f;
    private float xSensitivity;
    private float gravity = 0.4f;
    private CharacterController controller;
    private Camera cam;
    private CameraMovement camScript;
    private CapsuleCollider capsuleCollider;
    private float upwardVelocity = 0.0f;
    Vector3 airMovement;
    private bool isGrounded = false;
    private bool showMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        camScript = cam.GetComponent<CameraMovement>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        xSensitivity = GameManager.Instance.xSensitivity;
        LockIn();
    }

    public void LockIn()
    {
        Time.timeScale = 1.0f;
        menuView.enabled = false;
        gameView.enabled = true;
        camScript.LockIn();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        showMenu = false;
    }

    public void LockOut()
    {
        Time.timeScale = 0.0f;
        menuView.enabled = true;
        gameView.enabled = false;
        camScript.LockOut();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        showMenu = true;
    }

    public void setXSensitivity(float value)
    {
        xSensitivity = value * Conversions.SliderModifier;
    }

    // Update is called once per frame
    void Update()
    {

        //toggle menu
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (showMenu)
                LockIn();
            else
                LockOut();
        }

        //movement, turning, & interaction
        if (!showMenu)
        {
            RaycastHit hit;

            //movement
            Vector3 centerPlayer = transform.position;
            float playerScale = transform.localScale.y;
            Vector3 offset = (Vector3.down * playerScale) + new Vector3(0.0f, playerScale / 2, 0.0f);
            isGrounded = Physics.SphereCast(centerPlayer + offset, transform.localScale.y / 2, Vector3.down, out hit, 0.08f);

            Vector3 movement = new Vector3(0, 0, 0);
            if (!controller.isGrounded)
            {
                movement = airMovement;
            }
            else
            {
                if (movementEnabled)
                {
                    movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;
                    movement = transform.TransformDirection(movement);
                    //store our x and z coords for air movement
                    airMovement = new Vector3(movement.x, 0.0f, movement.z);
                }
            }
            movement += new Vector3(0.0f, upwardVelocity, 0.0f);
            controller.Move(movement);
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f) * xSensitivity * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                //interaction
                Vector3 rayDirection = cam.transform.TransformDirection(Vector3.forward);
                if (wallMenu != null)
                {
                    wallMenu.SendMessage("DoAction");
                }
                else if (Physics.Raycast(cam.transform.position, rayDirection, out hit, Mathf.Infinity, PlayerShootsLayer))
                {
                    hit.collider.gameObject.SendMessage("TakeDamage");
                }
                
            }

            //calc isGrounded
            if (!controller.isGrounded)
            {
                controller.Move(new Vector3(0.0f, -0.01f, 0.0f));
            }

            if (!controller.isGrounded)
            {
                upwardVelocity -= gravity * Time.deltaTime;
            }
            else
            {
                upwardVelocity = 0.0f;
            }

            if (jumpEnabled)
            {
                //jump
                if (Input.GetButtonDown("Jump") && controller.isGrounded)
                {
                    upwardVelocity = 0.2f;
                }
            }
            
        }
    }

    void FixedUpdate()
    {
        //raycast for wall menus
        Vector3 rayDirection = cam.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, rayDirection, out hit, Mathf.Infinity, WallMenuLayer))
        {
            int temp;
            GameObject hitObject = hit.collider.gameObject;
            if ((temp = hitObject.GetInstanceID()) != wallMenuID)
            {
                //deselect old
                if (wallMenu != null)
                    wallMenu.SendMessage("DisableSelected");
                //select new
                wallMenuID = temp;
                wallMenu = hitObject;
                wallMenu.SendMessage("EnableSelected");
            }
        }
        else
        {
            //deselect old
            if (wallMenu != null)
                wallMenu.SendMessage("DisableSelected");
            wallMenu = null;
            wallMenuID = 0;
        }
    }

    public void SimulateImpulse(Vector3 force)
    {
        upwardVelocity = force.y;
        airMovement.x = force.x;
        airMovement.z = force.z;
    }
}
