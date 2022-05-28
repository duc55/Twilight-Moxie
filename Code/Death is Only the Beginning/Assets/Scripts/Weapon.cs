using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // === PARAMETERS =====
    [Header("Weapon")]
    [SerializeField] float minDamage;
    [SerializeField] float maxDamage;
    List<GameObject> hitObjects = new List<GameObject>();

    // === COMPONENTS =====


    public void ResetHitObjects()
    {
        hitObjects.Clear();
    }

    public void SetDamage(float newMin, float newMax)
    {
        minDamage = newMin;
        maxDamage = newMax;
    }

    private float GetDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (hitObjects.Contains(other.gameObject)) {
            return;
        }

        Health otherHealth = other.gameObject.GetComponent<Health>();
        
        Debug.Log(gameObject.transform.parent.transform.parent.gameObject.name + "'s " + gameObject.name + " hit " + other.gameObject.name + " at frame " + Time.frameCount);

        if (otherHealth != null) {
            otherHealth.TakeDamage(GetDamage());
        }

        hitObjects.Add(other.gameObject);
    }
}
