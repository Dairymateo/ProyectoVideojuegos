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

    // L�mites de la c�mara
    private float minX;
    private float maxX;

    private bool isDead = false; // Estado de muerte del enemigo

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        attackTimer = AttackCooldown;

        // Calcular los l�mites basados en la c�mara principal
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

        // Si no est� atacando, seguir al jugador
        FollowPlayer();

        // Limitar el movimiento del enemigo dentro de los l�mites
        RestrictMovement();
    }

    private void FollowPlayer()
    {
        if (Player == null)
        {
            Debug.LogError("Player no est� asignado.");
            return;
        }

        if (!IsPlayerInRange())
        {
            // Seguir al jugador
            float direction = Mathf.Sign(Player.position.x - transform.position.x);
            Rigidbody2D.velocity = new Vector2(direction * Speed, Rigidbody2D.velocity.y);

            // Cambiar la direcci�n del enemigo
            if (direction < 0.0f) transform.localScale = new Vector3(-6.0f, 6.0f, 6.0f);
            else transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);

            // Activar animaci�n de correr
            Animator.SetBool("running2", true);
        }
        else
        {
            // Detener la animaci�n de correr
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
        // Si est� en tiempo de ataque, no hace nada
        if (attackTimer < AttackCooldown)
            return;

        // Reiniciar el temporizador de ataque
        attackTimer = 0f;
<<<<<<< Updated upstream
        Animator.SetTrigger("Attack2");
        
=======

        // Activar la animaci�n de ataque
        Animator.SetTrigger("attack");

        // Aplicar da�o al jugador
>>>>>>> Stashed changes
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
        Destroy(gameObject, 2f); // Destruir despu�s de 2 segundos
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