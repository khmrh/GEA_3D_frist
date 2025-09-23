using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] ProjectilePrefab;     //projectile프리팹
    public Transform FirePont;          //발사 위치 (총구)
    Camera cam;

    public float Switch = 0f;

    void Start()
    {
        cam = Camera.main;//메인 카메라 가져오기
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }
        else
        {

        }

        if (Input.GetMouseButtonDown(0))//좌클릭 발사
        {
            Shoot();
        }
    }

    void Shoot(GameObject bullet)
    {
        //화면에서 마우스 -> 광선 (ray) 쏘기
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint= ray.GetPoint(50f);
        Vector3 direction = (targetPoint - FirePont.position).normalized; //방향 백터

        //Projectile 생성
        Instantiate(bullet, FirePont.position, Quaternion.LookRotation(direction));
    }
}
