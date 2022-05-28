using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // === PARAMETERS =====
    [Header("Health")]
    public float maxHitPoints;
    float currentHitPoints;
    [Space(10)]
    public UnityEvent onDeath;

    // === PARAMETERS =====
    [Header("Sounds")]
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;

    // === COMPONENTS =====
    [Header("Components")]
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Scrollbar healthBar;
    AudioSource audioSource;
    Shader originalShader;
    Color originalColor;


    void Start() 
    {
        currentHitPoints = maxHitPoints;
        audioSource = GetComponent<AudioSource>();
        originalShader = meshRenderer.material.shader;
        originalColor = meshRenderer.material.color;
    }

    public void TakeDamage(float damage) 
    {
        currentHitPoints = Mathf.Max(0f, currentHitPoints - damage);
        //Debug.Log(gameObject.name + ": I took " + damage + " damage (" + currentHitPoints + "/" + maxHitPoints + ")");

        if (healthBar != null) {
            healthBar.size = currentHitPoints / maxHitPoints;
        }

        if (currentHitPoints <= 0) {
            //Debug.Log(gameObject.name + ": I'm dead!");

            GameManager.instance.audioSource.PlayOneShot(deathSound);

            onDeath.Invoke();
        } else {
            StartCoroutine(Flash());
            audioSource.PlayOneShot(hitSound);
        }
    }

    public void Heal(float restore)
    {
        currentHitPoints = Mathf.Min(maxHitPoints, currentHitPoints + restore);
        //Debug.Log(gameObject.name + ": I healed " + restore + " hp (" + currentHitPoints + "/" + maxHitPoints + ")");

        if (healthBar != null) {
            healthBar.size = currentHitPoints / maxHitPoints;
        }
    }

    IEnumerator Flash()
    {
        meshRenderer.material.shader = Shader.Find("Unlit/Color");
        meshRenderer.material.color = Color.white;

        float flashDuration = 0.05f;
        yield return new WaitForSeconds(flashDuration);

        meshRenderer.material.shader = originalShader;
        meshRenderer.material.color = originalColor;
    }
}
