using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    float moveSpeed;
    [SerializeField] float rightBoundary;
    [SerializeField] float rightOffset;
    [SerializeField] float bottomBoundary;
    [SerializeField] float bottomOffset;


    private void Start()
    {
        SetSpeed(Random.Range(minSpeed, maxSpeed));
    }
    
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x <= 0f) {
            Reset();
        }
    }

    private void Reset()
    {
        SetSpeed(Random.Range(minSpeed, maxSpeed));
        transform.position = new Vector3(rightBoundary + Random.Range(0f, rightOffset), transform.position.y, transform.position.z);
    }

    private void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;

        transform.localScale = Vector3.one * (0.5f + 2.5f * moveSpeed / maxSpeed);
        transform.position = new Vector3(transform.position.x, bottomBoundary - bottomOffset * moveSpeed / maxSpeed, -moveSpeed);
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(1.0f, 0.75f - 0.65f * moveSpeed / maxSpeed, 0.73f);
    }
}
