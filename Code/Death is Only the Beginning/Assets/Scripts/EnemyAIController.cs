using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyAIController : MonoBehaviour
{
    [Header("Guard")]
    Vector3 guardingPosition;
    Quaternion guardingRotation;

    [Header("Chase")]
    [SerializeField] float chaseDistance;
    [SerializeField] float timeToWaitAfterChasing;

    [Header("Attack")]
    [SerializeField] float attackDistance;
    [SerializeField] float timeBetweenAttacks = 1f;
    float timeSinceLastAttack;

    // === SOUNDS =====
    [Header("Sounds")]
    [SerializeField] AudioClip attackSound;

    // === COMPONENTS =====
    [Header("Components")]
    [SerializeField] TMP_Text stateText;
    [SerializeField] Animator weaponAnimator;
    EnemyMove enemyMove;
    DropLoot dropLoot;

    // === STATE =====
    delegate void State(); 
    State currentState;
    float timeSinceLastSawPlayer;

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        dropLoot = GetComponent<DropLoot>();
        timeSinceLastAttack = timeBetweenAttacks;
        ChangeState(IdleState, "Idle");
        guardingPosition = transform.position;
        guardingRotation = transform.rotation;
    }

    void Update() 
    {
        currentState?.Invoke();
    }

    public void Die()
    {
        dropLoot.Drop();

        GameManager.instance.Kills++;

        Destroy(gameObject);
    }

    float DistanceToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            return Vector3.Distance(transform.position, player.transform.position);
        }
        return Mathf.Infinity;
    }
    
    private void ChangeState(State nextState, string stateName)
    {
        currentState = nextState;
        stateText.text = stateName;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    void IdleState()
    {
        if (DistanceToPlayer() <= attackDistance) {
            enemyMove.Cancel();
            ChangeState(AttackState, "Attack");
            return;
        } else if (DistanceToPlayer() <= chaseDistance) {
            ChangeState(ChaseState, "Chase");
        }
    }

    void ChaseState()
    {
        if (DistanceToPlayer() <= attackDistance) {
            enemyMove.Cancel();
            ChangeState(AttackState, "Attack");
            return;
        } else if (DistanceToPlayer() > chaseDistance) {
            enemyMove.Cancel();

            timeSinceLastSawPlayer += Time.deltaTime;

            if (timeSinceLastSawPlayer >= timeToWaitAfterChasing) {
                ChangeState(GuardState, "Guard");
            }

            return;
        }

        enemyMove.MoveTo(GameObject.FindGameObjectWithTag("Player").transform.position);
        timeSinceLastSawPlayer = 0f;
    }

    void GuardState()
    {
        if (DistanceToPlayer() <= attackDistance) {
            enemyMove.Cancel();
            ChangeState(AttackState, "Attack");
            return;
        } else if (DistanceToPlayer() <= chaseDistance) {
            ChangeState(ChaseState, "Chase");
            return;
        }

        if (Vector3.Distance(transform.position, guardingPosition) > 1f) {
            enemyMove.MoveTo(guardingPosition);
        } else if (transform.rotation != guardingRotation) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, guardingRotation, 500f);
        } else {
            ChangeState(IdleState, "Idle");
        }
    }

    void AttackState()
    {
        if (DistanceToPlayer() > attackDistance && DistanceToPlayer() <= chaseDistance) {
            ChangeState(ChaseState, "Chase");
            return;
        } else if (DistanceToPlayer() > chaseDistance) {
            timeSinceLastSawPlayer += Time.deltaTime;

            if (timeSinceLastSawPlayer >= timeToWaitAfterChasing) {
                ChangeState(GuardState, "Guard");
            }

            return;
        }

        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

        timeSinceLastSawPlayer = 0;
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= timeBetweenAttacks) {
            weaponAnimator.SetTrigger("Attack");
            GameManager.instance.audioSource.PlayOneShot(attackSound);
            timeSinceLastAttack = 0;
        }
    }
}
