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
    public int damage = 10;  // Da�o que el jefe inflige
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

        // Si no est� atacando, seguir al jugador
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

        // Actualizar animaci�n de movimiento
        Animator.SetBool("running", Mathf.Abs(Rigidbody2D.velocity.x) > 0.1f);

        // Ajustar la escala para voltear al jefe cuando cambia de direcci�n
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
        // Limitar el movimiento a solo cuando no est� atacando
        if (!isAttacking)
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, Rigidbody2D.velocity.y);
        }
    }

    private void FollowPlayer()
    {
        // El jefe se mueve hacia el jugador si no est� a la distancia de ataque
        if (!IsPlayerInRange())
        {
            float direction = Mathf.Sign(Player.position.x - transform.position.x);  // 1 si est� a la derecha, -1 si est� a la izquierda
            Rigidbody2D.velocity = new Vector2(direction * Speed, Rigidbody2D.velocity.y);
        }
    }

    private bool IsPlayerInRange()
    {
        // Comprobar si el jugador est� dentro del rango de ataque
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);
        return distanceToPlayer <= AttackRange;
    }

    private void AttackPlayer()
    {
        // Aqu� el jefe realiza un ataque al jugador
        Debug.Log("El jefe est� atacando al jugador");
        isAttacking = true;
        Animator.SetTrigger("attack");  // Lanzar la animaci�n de ataque
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(0.5f);  // Simular un retraso en el ataque para la animaci�n

        // Comprobar si el jugador sigue en el rango de ataque y aplicar da�o
        if (IsPlayerInRange())
        {
            // Aqu� puedes aplicar da�o al jugador. Esto asume que el jugador tiene un componente de salud
            Player.GetComponent<PlayerHealth>().TakeDamage(damage);  // Aplicar da�o al jugador

        }

        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false;  // El ataque ha terminado
    }

    private void Jump()
    {
        // Saltar si est� en el suelo
        if (Grounded)
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isJumping = true;
            Invoke("FinishJump", 1f);  // Finalizar el salto despu�s de 1 segundo
        }
    }

    private void FinishJump()
    {
        isJumping = false;
    }
}