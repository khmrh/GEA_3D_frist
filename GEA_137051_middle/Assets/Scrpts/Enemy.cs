using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum Enemystate { Idle, Trace, attack, RunAway}
    public Enemystate state = Enemystate.Idle;

    public float moveSpeed = 2f;
    public float taceRange = 15f;
    public float attackRange = 6f;
    public float attackCooldown = 1.5f;

    public float health = 5f;
    public float nodheal = 3f;

    public Slider HPbar;

    public GameObject ProjectilePrefab;
    public Transform FirePont;

    private Transform player;
    public float lastAttacktime;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        HPbar.value = 1f;
    }
    void Update()
    {
        if (health <= 1 && state != Enemystate.Idle)
        {
            state = Enemystate.RunAway;
        }

        if (player == null ) return;

        float dist = Vector3.Distance(player.position, transform.position);

        //Fsm ������ȯ
        switch (state)
        {
            case Enemystate.Idle:
                if (health <= 1f)
                {
                    Debug.Log("ü�� ����");
                    health += nodheal * Time.deltaTime * 2f;
                }
                else if (health == 3f) { Debug.Log("ȸ���Ϸ�"); }
                if (dist < taceRange)
                    state = Enemystate.Trace;
                break;

            case Enemystate.Trace:
                if (dist < attackRange)
                    state = Enemystate.attack;
                else if (dist > taceRange)
                    state = Enemystate.Idle;
                else
                    tracePlayer();
                break;

            case Enemystate.attack:
                if (dist > attackRange)
                    state = Enemystate.Trace;
                else
                    AttackPlayer();
                break;

            case Enemystate.RunAway:
                if (dist < taceRange && health <= 1)
                    RRunAway();
                else
                    state = Enemystate.Idle;
                break;
        }

    }

    void tracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position);
    }

    void AttackPlayer()
    {
        //���� ��ٿ�� �߻�
        if (Time.time >= lastAttacktime + attackCooldown)
        {
            lastAttacktime = Time.time;
            ShootProjectile();
        }
    }

    void RRunAway()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * -moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position);
    }

    void ShootProjectile()
    {
        if (ProjectilePrefab != null && FirePont != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(ProjectilePrefab, FirePont.position, FirePont.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if (ep != null)
            {
                Vector3 dir = (player.position - FirePont.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount; // ���� ������ �縸ŭ ü�� ����
        Debug.Log("�� ü��: " + health + ", ���� ������: " + amount);

        HPbar.value = (float)health;

        // ü���� 0 ���ϰ� �Ǹ� �� ������Ʈ �ı�
        if (health <= 0f)
        {
            Destroy(gameObject);
            Debug.Log("���� �ı��Ǿ����ϴ�.");
        }
    }
}
