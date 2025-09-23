using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;       //이동속도
    public float lifeTime = 2f;     //생존 시간 (초)

    public float damage = 1f;

    void Start()
    {
        //일정 시간 후 자동 삭제 (메모리 관리)
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //로컬의 forwoard방향(앞)으로 이동
        transform.Translate(Vector3.forward * speed *  Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {

            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            // Projectile 제거
            Destroy(gameObject);
        }
    }
}
