using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemacineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    public CinemachineFreeLook freeLookCame;
    public CinemachineVirtualCamera fpsCam;

    public int activeCamIndex = 0;

    void Start()
    {
        SwitchCamera(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            activeCamIndex = (activeCamIndex + 1) % 3;
            SwitchCamera(activeCamIndex);
        }
    }

    void SwitchCamera(int index)
    {
        virtualCam.Priority = 0;
        freeLookCame.Priority = 0;
        fpsCam.Priority = 0;

        if (index == 0)
        {
            virtualCam.Priority = 20;
            Debug.Log("�⺻ ī�޶� Ȱ��ȭ");
        }
        else if (index == 1)
        {
            freeLookCame.Priority = 20;
            Debug.Log("���� ���� ī�޶� Ȱ��ȭ");
        }
        else if (index == 2)
        {
            fpsCam.Priority = 20;
            Debug.Log("FPS ī�޶� Ȱ��ȭ");
        }
    }
}