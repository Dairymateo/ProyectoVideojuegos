using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYellowNinja : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    public Transform Player;
    public float Speed;
    public float AttackRange;
    public float AttackCooldown;
    public int damage;
    private float attackTimer;

    private float minX;
    private float maxX;
    public float minDistanceFromPlayer; // Distancia m�nima al jugador

    private bool isDead = false; // Estado de muerte del enemigo

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        attackTimer = AttackCooldown;

        Camera camera = Camera.main;
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        minX = camera.transform.position.x - cameraWidth / 2f;
        maxX = camera.transform.position.x + cameraWidth / 2f;
    }

    void Update()
    {
        if (isDead) return; // Si est� muerto, no hace nada

        attackTimer += Time.deltaTime;
        FollowPlayer();
        RestrictMovement();
    }

    private void FollowPlayer()
    {
        if (Player == null)
        {
            Debug.LogError("Player no est� asignado.");
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        if (distanceToPlayer > minDistanceFromPlayer)
        {
            if (!IsPlayerInRange())
            {
                float direction = Mathf.Sign(Player.position.x - transform.position.x);
                Rigidbody2D.velocity = new Vector2(direction * Speed, Rigidbody2D.velocity.y);

                if (direction < 0.0f) transform.localScale = new Vector3(-6.0f, 6.0f, 6.0f);
                else transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);

                Animator.SetBool("running2", true);
            }
            else
            {
                Animator.SetBool("running2", false);
                Attack();
            }
        }
        else
        {
            Rigidbody2D.velocity = Vector2.zero; // Detiene al enemigo si est� demasiado cerca
            Animator.SetBool("running2", false);
        }
    }

    private bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);
        return distanceToPlayer <= AttackRange;
    }

    private void Attack()
    {
        if (attackTimer < AttackCooldown)
            return;

        attackTimer = 0f;
        Animator.SetTrigger("Attack2");

        if (Player != null)
        {
            Player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    public void Die() // M�todo para manejar la muerte
    {
        if (isDead) return; // Evita m�ltiples llamadas a la muerte

        isDead = true; // Cambia el estado a muerto
        Animator.SetTrigger("Morir"); // Activa la animaci�n de muerte
        Rigidbody2D.velocity = Vector2.zero; // Detiene cualquier movimiento
        // Otras acciones que quieras realizar al morir, como destruir el objeto despu�s de un tiempo
        Destroy(gameObject, 2f); // Destruir despu�s de 3 segundos
    }

    private void RestrictMovement()
    {
        if (isDead) return; // Evita restricciones de movimiento si est� muerto

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        transform.position = position;
    }

    private void FixedUpdate()
    {
        RestrictMovement();
    }
}