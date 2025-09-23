using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float health = 5f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player == null ) return;

        //플레이어까지 방향 구하기
        Vector3 direction = (player.position - transform.position ).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount; // 받은 데미지 양만큼 체력 감소
        Debug.Log("적 체력: " + health + ", 받은 데미지: " + amount);

        // 체력이 0 이하가 되면 적 오브젝트 파괴
        if (health <= 0f)
        {
            Destroy(gameObject);
            Debug.Log("적이 파괴되었습니다.");
        }
    }
}
