using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float speed = 3f; // Velocidad del enemigo
    public float patrollingDistance = 4f; // Distancia a patrullar
    public Transform[] waypoints; // Puntos de patrulla
    public Transform player; // Jugador
    public float detectionRange = 5f; // Rango de detección del jugador
    public float chaseSpeed = 5f; // Velocidad de persecución
    public Animator animator; // Animator del enemigo

    private int currentWaypointIndex = 0; // Índice actual del punto de patrulla
    private Vector3 targetPosition; // Posición objetivo del enemigo
    private bool isChasing = false; // Indica si está persiguiendo al jugador

    void Start()
    {
        // Al inicio, establecemos el primer punto de patrulla como objetivo
        targetPosition = waypoints[currentWaypointIndex].position;
    }

    void Update()
    {
        if (!isChasing)
        {
            // Si no está persiguiendo al jugador, se mueve hacia el punto de patrulla actual
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Si alcanza el punto de patrulla actual, cambia al siguiente punto de patrulla
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                targetPosition = waypoints[currentWaypointIndex].position;
            }
        }
        else
        {
            // Si está persiguiendo al jugador, se mueve hacia su posición
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }

        // Si el jugador está dentro del rango de detección, comienza a perseguirlo
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

        // Si se está moviendo hacia la derecha, voltea al enemigo hacia esa dirección
        if (transform.position.x < targetPosition.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Si se está moviendo hacia la izquierda, voltea al enemigo hacia esa dirección
        else if (transform.position.x > targetPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
