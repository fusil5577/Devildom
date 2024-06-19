﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputController controller;
    private Rigidbody2D movementRigidbody;

    private Vector2 movementDirection = Vector2.zero;
    private bool isGrounded = true;
    private bool canDoubleJump = false;
    private float jumpForce = 7f;

    [SerializeField] private float moveSpeed = 8.0f;

    private void Awake()
    {
        controller = GetComponent<InputController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
        controller.OnJumpEvent += Jump;
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            movementRigidbody.velocity = new Vector2(movementRigidbody.velocity.x, jumpForce);
            isGrounded = false;
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            movementRigidbody.velocity = new Vector2(movementRigidbody.velocity.x, jumpForce);
            canDoubleJump = false;
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * moveSpeed;
        movementRigidbody.velocity = new Vector2(direction.x, movementRigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}