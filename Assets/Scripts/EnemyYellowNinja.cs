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
