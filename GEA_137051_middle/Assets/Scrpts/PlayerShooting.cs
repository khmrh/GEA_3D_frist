using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] ProjectilePrefab;     //projectile������
    public Transform FirePont;          //�߻� ��ġ (�ѱ�)
    Camera cam;

    public float Switch = 0f;

    void Start()
    {
        cam = Camera.main;//���� ī�޶� ��������
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }
        else
        {

        }

        if (Input.GetMouseButtonDown(0))//��Ŭ�� �߻�
        {
            Shoot();
        }
    }

    void Shoot(GameObject bullet)
    {
        //ȭ�鿡�� ���콺 -> ���� (ray) ���
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint= ray.GetPoint(50f);
        Vector3 direction = (targetPoint - FirePont.position).normalized; //���� ����

        //Projectile ����
        Instantiate(bullet, FirePont.position, Quaternion.LookRotation(direction));
    }
}
