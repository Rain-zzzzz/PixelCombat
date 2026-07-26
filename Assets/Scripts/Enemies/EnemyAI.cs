using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeTime = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private bool stopMoving = false;

    private enum State
    {
        Roaming,
        Attacking
    };

    private State state;
    private Vector2 roamingPosition;
    private EnemyPathFinding enemyPathFinding;
    private float timeRoaming = 0f;
    private bool canAttack = true;

    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamingPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateContrl();
    }

    private void MovementStateContrl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        enemyPathFinding.MoveTo(roamingPosition);
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
        if (timeRoaming > roamChangeTime)
        {
            roamingPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(PlayerController.Instance.transform.position, transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();
            if (stopMoving)
            {
                enemyPathFinding.StopMoving();
            }
            else
            {
                enemyPathFinding.MoveTo(roamingPosition);
            }
            StartCoroutine(AttackCoolDownRoutine());
        }
    }

    private IEnumerator AttackCoolDownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f)).normalized;
    }
}