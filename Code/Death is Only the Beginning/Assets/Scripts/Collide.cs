using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Debug.Log(gameObject.name + ": I collided with " + other.gameObject.name);
    }
}
