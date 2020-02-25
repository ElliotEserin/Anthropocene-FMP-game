using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : DynamicMovement
{
    public float walkSpeed;
    public float sprintSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    bool isSprinting = false;
    float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        moveSpeed = 0;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (isSprinting) { moveSpeed += sprintSpeed; }

        if (movement.sqrMagnitude > 0.01) { base.updateSortOrder(); }

        if (movement.x != 0 && movement.y != 0) { moveSpeed += walkSpeed / 2; }
        else { moveSpeed += walkSpeed; }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
