using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float horizontalMov;
    public float Velocity = 4f;
    public float jumpForce = 3f;
    private Rigidbody2D rigidBody;
    private Animator Animator;
    public bool jumping;
    private float LastShoot;
    public GameObject BulletPrefab;
    public Transform BulletSpawn;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();


        jumping = false;
    }


    void Update()
    {
        horizontalMov = Input.GetAxisRaw("Horizontal");

        Animator.SetBool("Running", horizontalMov != 0.0f);


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


        if (Input.GetKey(KeyCode.Mouse0) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
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


    private void Shoot()
    {
        GameObject newBulletPrefab = BulletPrefab;

        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(newBulletPrefab, BulletSpawn.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce);
    }
}




