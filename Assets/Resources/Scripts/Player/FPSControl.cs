using UnityEngine;
using System.Collections;

public class FPSControl : MonoBehaviour
{
    CharacterController cc;
    PlayerScript player;
    public Camera camera;
    public float speed, jumpForce;
    public float sensitivity;
    private float degree = 90f;
    private float rotateX;
    private float rotateY = 0f;
    private float gravity;

    bool usingJetPack, isJumping, isCrouching, hasControl;

    private float vertical, horizontal, mouseHoldTimer;

    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        cc = GetComponent<CharacterController>();
        player = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            rotateX = Input.GetAxis("Mouse X") * sensitivity;
            rotateY += Input.GetAxis("Mouse Y") * sensitivity;

            transform.Rotate(0, rotateX, 0);
            rotateY = Mathf.Clamp(rotateY, -degree, degree);
            camera.transform.localRotation = Quaternion.Euler(rotateY, 0, 0);


            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            vertical = 0;
            horizontal = 0;
        }

        Vector3 movement = new Vector3(horizontal * speed, gravity, vertical * speed);

        movement = transform.rotation * movement;

        cc.Move(movement * Time.deltaTime);


        if (!cc.isGrounded && !usingJetPack)
        {
            gravity += Physics.gravity.y * Time.deltaTime;
        }

        if (cc.isGrounded)
        {
            isJumping = false;
            if (!isJumping)
            {
                gravity = 0;
            }
        }

        if(player.playerControl) { 
            if (Input.GetButton("Jump") && cc.isGrounded)
            {
                gravity = jumpForce;
                isJumping = true;
            }

            if (Input.GetMouseButton(1))
            {
                usingJetPack = true;
                if (gravity <= 3f)
                {
                    gravity += player.stats["jetpack_force"].finalValue * Time.deltaTime * 100;
                }
            }
            usingJetPack = false;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 6;
            }
            else
            {
                speed = 3;
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                isCrouching = true;
                transform.localScale = new Vector3(1, 0.4f, 1);
                speed = 1.5f;
            }
            else
            {
                if (!Physics.Raycast(transform.position, Vector3.up, 1f))
                {
                    transform.localScale = new Vector3(1, 0.9f, 1);
                }
            }
        }
    }
}

