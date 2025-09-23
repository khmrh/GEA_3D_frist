using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.8f;
    public CinemachineVirtualCamera virtualCam;
    public float rotationSpeed = 10f;

    private CharacterController controller;
    private Vector3 velocity;
    public bool isGrounded;

    public CinemacineSwitcher switcher;

    public CinemachineVirtualCamera fpsCam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10;
            if (switcher.activeCamIndex == 0)
            {
                virtualCam.m_Lens.FieldOfView = 80f;
            }
            else if (switcher.activeCamIndex == 2)
            {
                fpsCam.m_Lens.FieldOfView = 80f;
            }
        }
        else
        {
            speed = 5f;
            if (switcher.activeCamIndex == 0)
            {
                virtualCam.m_Lens.FieldOfView = 60f;
            }
            else if (switcher.activeCamIndex == 2)
            {
                fpsCam.m_Lens.FieldOfView = 60f;
            }
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 camForward = Vector3.zero;
        if (switcher.activeCamIndex == 0)
        {
            camForward = virtualCam.transform.forward;
        }
        else if (switcher.activeCamIndex == 2)
        {
            camForward = fpsCam.transform.forward;
        }
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Vector3.zero;
        if (switcher.activeCamIndex == 0)
        {
            camRight = virtualCam.transform.right;
        }
        else if (switcher.activeCamIndex == 2)
        {
            camRight = fpsCam.transform.right;
        }
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized;
        controller.Move(move * speed * Time.deltaTime);

        if (switcher.activeCamIndex == 0 || switcher.activeCamIndex == 2)
        {
            CinemachinePOV pov = null;

            if (switcher.activeCamIndex == 0)
            {
                pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();
            }
            else if (switcher.activeCamIndex == 2)
            {
                pov = fpsCam.GetCinemachineComponent<CinemachinePOV>();
            }

            if (pov != null)
            {
                float cameraYaw = pov.m_HorizontalAxis.Value;
                Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}