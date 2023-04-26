using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float speed = 3f; 
    public float patrollingDistance = 4f; 
    public Transform[] waypoints; 
    public Transform player; 
    public float detectionRange = 5f; 
    public float chaseSpeed = 5f; 
    public Animator animator;

    public Enemy enemy;

    
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;




    private int currentWaypointIndex = 0; 
    private Vector3 targetPosition; 
    private bool isChasing = false; 

    void Start()
    {

        targetPosition = waypoints[currentWaypointIndex].position;

        currentHealth = maxHealth;

    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }



    void Update()
    {
        if (!isChasing)
        {
         
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

       
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                targetPosition = waypoints[currentWaypointIndex].position;
            }
        }
        else
        {
           
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }

     
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            isChasing = true;
            animator.SetBool("isWalking", true);
        }
        else
        {
            isChasing = false;
            animator.SetBool("isWalking", false);
        }

       
        if (transform.position.x < targetPosition.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    
        else if (transform.position.x > targetPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
     {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement playerHealth = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemy.damage);
            }
        }
     }
}

