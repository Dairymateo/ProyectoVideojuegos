using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYellowNinja : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    public Transform Player;  // Referencia al jugador
    public float Speed = 3f;  // Velocidad del jefe
    public float JumpForce = 10f;
    public float AttackRange = 2f;  // Rango de ataque
    public float AttackCooldown = 2f;  // Tiempo entre ataques
    public int damage = 10;  // Daño que el jefe inflige
    private bool Grounded;
    private bool isAttacking;
    private bool isJumping;
    private float attackTimer;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        attackTimer = AttackCooldown;  // Inicialmente puede atacar
    }

    void Update()
    {
        // Usar Raycast para detectar el suelo
        Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.yellow);
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 1.2f);

        // Si no está atacando, seguir al jugador
        if (!isAttacking)
        {
            FollowPlayer();
        }

        // Gestionar el ataque
        attackTimer += Time.deltaTime;
        if (attackTimer >= AttackCooldown && IsPlayerInRange())
        {
            AttackPlayer();
            attackTimer = 0f;
        }

        // Actualizar animación de movimiento
        Animator.SetBool("running", Mathf.Abs(Rigidbody2D.velocity.x) > 0.1f);

        // Ajustar la escala para voltear al jefe cuando cambia de dirección
        if (Player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-6.0f, 6.0f, 6.0f);  // Mirar a la izquierda
        }
        else
        {
            transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);  // Mirar a la derecha
        }
    }

    void FixedUpdate()
    {
        // Limitar el movimiento a solo cuando no está atacando
        if (!isAttacking)
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, Rigidbody2D.velocity.y);
        }
    }

    private void FollowPlayer()
    {
        // El jefe se mueve hacia el jugador si no está a la distancia de ataque
        if (!IsPlayerInRange())
        {
            float direction = Mathf.Sign(Player.position.x - transform.position.x);  // 1 si está a la derecha, -1 si está a la izquierda
            Rigidbody2D.velocity = new Vector2(direction * Speed, Rigidbody2D.velocity.y);
        }
    }

    private bool IsPlayerInRange()
    {
        // Comprobar si el jugador está dentro del rango de ataque
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);
        return distanceToPlayer <= AttackRange;
    }

    private void AttackPlayer()
    {
        // Aquí el jefe realiza un ataque al jugador
        Debug.Log("El jefe está atacando al jugador");
        isAttacking = true;
        Animator.SetTrigger("attack");  // Lanzar la animación de ataque
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(0.5f);  // Simular un retraso en el ataque para la animación

        // Comprobar si el jugador sigue en el rango de ataque y aplicar daño
        if (IsPlayerInRange())
        {
            // Aquí puedes aplicar daño al jugador. Esto asume que el jugador tiene un componente de salud
            Player.GetComponent<PlayerHealth>().TakeDamage(damage);  // Aplicar daño al jugador

        }

        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false;  // El ataque ha terminado
    }

    private void Jump()
    {
        // Saltar si está en el suelo
        if (Grounded)
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isJumping = true;
            Invoke("FinishJump", 1f);  // Finalizar el salto después de 1 segundo
        }
    }

    private void FinishJump()
    {
        isJumping = false;
    }
}