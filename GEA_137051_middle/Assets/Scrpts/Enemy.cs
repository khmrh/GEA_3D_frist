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

        //�÷��̾���� ���� ���ϱ�
        Vector3 direction = (player.position - transform.position ).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount; // ���� ������ �縸ŭ ü�� ����
        Debug.Log("�� ü��: " + health + ", ���� ������: " + amount);

        // ü���� 0 ���ϰ� �Ǹ� �� ������Ʈ �ı�
        if (health <= 0f)
        {
            Destroy(gameObject);
            Debug.Log("���� �ı��Ǿ����ϴ�.");
        }
    }
}
