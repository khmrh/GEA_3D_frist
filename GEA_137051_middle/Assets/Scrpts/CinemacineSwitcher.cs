    using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemacineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCam;
    public CinemachineFreeLook freeLookCame;
    public bool usingFreeLook = false;

    // Start is called before the first frame update
    void Start()
    {
        VirtualCam.Priority = 10;
        freeLookCame.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook)
            {
                VirtualCam.Priority = 0;
                freeLookCame.Priority = 20;
            }
            else
            {
                VirtualCam.Priority = 20;
                freeLookCame.Priority = 0;
            }
        }
    }
}
