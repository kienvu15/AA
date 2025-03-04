﻿using System.Collections;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    public float fallDelay = 1f;
    private float destroyDelay = 2f;
    [SerializeField] private Rigidbody2D rb;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private RigidbodyType2D initialBodyType;
    private bool hasFallen = false;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialBodyType = rb.bodyType;

        FallPlatformManager.Instance.RegisterPlatform(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasFallen && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        hasFallen = true;
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);
    }

    public void ResetFallPlatform()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        hasFallen = false;
    }
}
