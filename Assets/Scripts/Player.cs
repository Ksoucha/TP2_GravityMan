using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 originalPosition;
    Camera playerCamera;
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    private int cherryCounter;

    void Start()
    {
        originalPosition = transform.position;
        playerCamera = Camera.main;
        spriteRenderer = rb.GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 nextPosition = transform.position;
        nextPosition += new Vector3(x, 0, 0) * Time.deltaTime * speed;
        transform.position = nextPosition;

        if (x != 0)
        {
            spriteRenderer.flipX = x < 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale *= -1;
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }

        var cameraPosition = playerCamera.transform.position;
        cameraPosition.x = transform.position.x;
        playerCamera.transform.position = cameraPosition;
    }

    internal void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Invoke(nameof(Reset), 0.5f);
    }

    private void Reset()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.position = originalPosition;
    }

    internal void CollectCherry()
    {
        if (++cherryCounter == 5)
        {
            FindAnyObjectByType<TMP_Text>().color = Color.white;
        }
    }
}
