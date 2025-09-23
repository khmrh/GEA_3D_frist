using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] ProjectilePrefab;     //projectile������
    public Transform FirePont;          //�߻� ��ġ (�ѱ�)
    Camera cam;

    public int SwitchWeapon = 0; //���� ��ü

    // ��Ÿ�ӿ� ���� 
    public float cooldowntimewapon = 2.0f; //���� ��Ÿ��
    public float nextfiretime = 0f; //���� �߻� �ð�

    void Start()
    {
        cam = Camera.main;//���� ī�޶� ��������
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchWeapon = (SwitchWeapon + 1) % ProjectilePrefab.Length; //���� ��ü
            Debug.Log("���� ����Ī");
        }

        if (Input.GetMouseButtonDown(0))//��Ŭ�� �߻�
        {
            if (SwitchWeapon == 1 && Time.time < nextfiretime)
            {
                Debug.Log("��Ÿ��");
                return;
            }

            Shoot(ProjectilePrefab[SwitchWeapon]);

            if (SwitchWeapon == 1)
            {
                nextfiretime = Time.time + cooldowntimewapon; //��Ÿ�� ����
            }
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
