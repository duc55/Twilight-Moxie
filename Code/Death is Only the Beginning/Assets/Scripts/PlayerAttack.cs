using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // === PARAMETERS =====
    [Header("Attack")]
    public float timeBetweenAttacks;
    float timeSinceLastAttack;

    // === SOUNDS =====
    [Header("Sounds")]
    [SerializeField] AudioClip attackSound;

    // === COMPONENTS =====
    [Header("Components")]
    [SerializeField] Animator animator;


    void Update() 
    {
        timeSinceLastAttack += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && timeSinceLastAttack >= timeBetweenAttacks) {
            animator.SetTrigger("Attack");
            GameManager.instance.audioSource.PlayOneShot(attackSound);
            timeSinceLastAttack = 0;
        }
    }
}
