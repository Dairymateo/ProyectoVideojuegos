using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    public float Speed;
    public float JumpForce;
    private bool Grounded;

    // Variables de ataque
    public Transform AttackPoint;
    public float AttackRange;
    public int AttackDamage;
    public LayerMask EnemyLayers;

    private bool isAttacking = false;
    private float attackCooldown;
    private float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        // Girar el personaje en la dirección de movimiento
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-6.0f, 6.0f, 6.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);

        // Animación de correr
        Animator.SetBool("running", Horizontal != 0.0f);

        // Comprobar si está en el suelo
        Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.yellow);
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 1.2f);


        // Saltar si está en el suelo y se presiona espacio
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Debug.Log("El personaje está en el suelo.");
            Jump();
        }

        // Ataque
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        // Movimiento horizontal
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Attack()
    {
        // Activar la animación de ataque
        Animator.SetTrigger("Attack");

        // Desactivar movimiento durante el ataque
        isAttacking = true;

        // Registrar el momento del ataque para el cooldown
        lastAttackTime = Time.time;

        // Detectar enemigos en el rango de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

        // Verificar si hay enemigos detectados
        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Golpeaste a " + enemy.name);
                enemy.GetComponent<EnemyHealth>().TakeDamage(AttackDamage);
            }
        }
        else
        {
            Debug.Log("No hay enemigos en rango de ataque.");
        }

        // Permitir movimiento nuevamente tras un breve retraso
        Invoke("ResetAttack", 0.1f);
    }



    private void ResetAttack()
    {
        isAttacking = false;
    }

    // Dibujar el rango de ataque en el Editor para visualización
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}