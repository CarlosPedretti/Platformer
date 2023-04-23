using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    //public AudioClip Sound;

    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;
    //private PlayerMovement playerMovement;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
         //playerMovement = GetComponent<PlayerMovement>();
        //Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
        
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(/*playerMovement.*/gameObject, 8f);
    }


    
    private void OnTriggerEnter2D(Collider2D other)
    {

        DestroyBullet();
    }
    
}
