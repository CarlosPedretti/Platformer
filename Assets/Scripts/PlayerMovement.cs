using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float horizontalMov;
    public float Velocity = 4f;
    public float jumpForce = 3f;
    private Rigidbody2D rigidBody;
    public bool jumping;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        jumping = false;
    }


    void Update()
    {
        horizontalMov = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && jumping == false)
        {
            jumping = true;
            Jump();

        }

        if (horizontalMov < 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontalMov > 0)
        {

            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = false;
        }
    }


    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalMov * Velocity, rigidBody.velocity.y);
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce);
    }
}




