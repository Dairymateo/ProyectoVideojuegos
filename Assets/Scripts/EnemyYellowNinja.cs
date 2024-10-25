using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYellowNinja : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    public Transform Player;  // Referencia al jugador
    public float Speed ;
    public float AttackRange;
    public float AttackCooldown;
    public int damage = 10;
    private float attackTimer;

    // Límites de la cámara
    private float minX;
    private float maxX;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        attackTimer = AttackCooldown;

        // Calcular los límites basados en la cámara principal
        Camera camera = Camera.main;
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        minX = camera.transform.position.x - cameraWidth / 2f;
        maxX = camera.transform.position.x + cameraWidth / 2f;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        // Si no está atacando, seguir al jugador
        FollowPlayer();

        // Limitar el movimiento del enemigo dentro de los límites
        RestrictMovement();
    }

    private void FollowPlayer()
    {
        if (Player == null)
        {
            Debug.LogError("Player no está asignado.");
            return;
        }

        if (!IsPlayerInRange())
        {
            // Seguir al jugador
            float direction = Mathf.Sign(Player.position.x - transform.position.x);
            Rigidbody2D.velocity = new Vector2(direction * Speed, Rigidbody2D.velocity.y);

            // Cambiar la dirección del enemigo
            if (direction < 0.0f) transform.localScale = new Vector3(-6.0f, 6.0f, 6.0f);
            else transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);

            // Activar animación de correr
            Animator.SetBool("running2", true);
        }
        else
        {
            // Detener la animación de correr
            Animator.SetBool("running2", false);
            Attack();
        }
    }

    private bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);
        return distanceToPlayer <= AttackRange;
    }

    private void Attack()
    {
        // Si está en tiempo de ataque, no hace nada
        if (attackTimer < AttackCooldown)
            return;

        // Reiniciar el temporizador de ataque
        attackTimer = 0f;

        // Activar la animación de ataque
        Animator.SetTrigger("attack");

        // Aplicar daño al jugador
        if (Player != null)
        {
            Player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void RestrictMovement()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        transform.position = position;
    }

    private void FixedUpdate()
    {
        RestrictMovement();
    }
}