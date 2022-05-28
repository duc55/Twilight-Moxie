using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // === PARAMETERS =====
    [Header("Sounds")]
    [SerializeField] AudioClip pickUpSound;

    // === COMPONENTS =====
    [Header("Components")]
    public Rigidbody rigi;
    Animator animator;


    void Start() 
    {
        rigi = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update() 
    {
        animator.SetFloat("Speed", rigi.velocity.magnitude);
    }

    public void Initialize(float throwStrength)
    {
        Vector3 throwForce = Random.insideUnitSphere * throwStrength;
        throwForce.y = Mathf.Abs(throwForce.y);

        rigi.AddForce(throwForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player")) {
            GameManager.instance.audioSource.PlayOneShot(pickUpSound);

            GameManager.instance.Gold++;

            Destroy(gameObject);
        }
    }
}
