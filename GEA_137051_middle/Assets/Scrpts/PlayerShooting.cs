using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] ProjectilePrefab;     //projectile프리팹
    public Transform FirePont;          //발사 위치 (총구)
    Camera cam;

    public int SwitchWeapon = 0; //무기 교체

    // 쿨타임용 변수 
    public float cooldowntimewapon = 2.0f; //무기 쿨타임
    public float nextfiretime = 0f; //다음 발사 시간

    void Start()
    {
        cam = Camera.main;//메인 카메라 가져오기
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchWeapon = (SwitchWeapon + 1) % ProjectilePrefab.Length; //무기 교체
            Debug.Log("무기 스위칭");
        }

        if (Input.GetMouseButtonDown(0))//좌클릭 발사
        {
            if (SwitchWeapon == 1 && Time.time < nextfiretime)
            {
                Debug.Log("쿨타임");
                return;
            }

            Shoot(ProjectilePrefab[SwitchWeapon]);

            if (SwitchWeapon == 1)
            {
                nextfiretime = Time.time + cooldowntimewapon; //쿨타임 설정
            }
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
