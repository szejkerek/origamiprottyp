using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from WASD keys
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1f;
        }

        // Calculate movement vector
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Normalize the movement vector to avoid diagonal movement faster than horizontal/vertical
        movement.Normalize();

        // Move the character
        rb.velocity = movement * moveSpeed;
    }

}
