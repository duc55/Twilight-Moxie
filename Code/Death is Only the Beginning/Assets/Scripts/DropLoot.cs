using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    // === LOOT =====
    [Header("Loot")]
    public GameObject dropPrefab;
    public int dropCount;
    public float throwStrength;


    public void Drop()
    {
        for (int i = 0; i < dropCount; i++) {
            GameObject drop = Instantiate(dropPrefab, transform.position + Random.onUnitSphere + Vector3.up, Quaternion.identity);
            PickUp pickUp = drop.GetComponent<PickUp>();
            if (pickUp != null) {
                pickUp.Initialize(throwStrength);
            }
        }
    }
}
