using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    public float JumpForce;  // Establece un valor por defecto
    public float Speed;      // Establece un valor por defecto
    private bool Grounded;
    public Transform AreaAtaque;
    public float AttackRange;
    public int damage;
    public LayerMask Enemylayer;
    private float leftLimit, rightLimit;

<<<<<<< Updated upstream
    // Variables de ataque
    public Transform AttackPoint;
    public float AttackRange;
    public int AttackDamage;
    public LayerMask EnemyLayers;

    private bool isAttacking = false;
    private float attackCooldown;
    private float lastAttackTime;
=======
>>>>>>> Stashed changes

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

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-6.0f, 6.0f, 6.0f);
        else if (Horizontal >0.0f) transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);

        Animator.SetBool("running", Horizontal !=0.0f);


<<<<<<< Updated upstream

        // Saltar si est� en el suelo y se presiona espacio
=======
        Debug.DrawRay(transform.position, Vector3.down * 1.2f,Color.yellow);
        if(Physics2D.Raycast(transform.position, Vector3.down, 1.2f))
        {
            Grounded = true;
            Debug.Log("grounded true");
        }
        else Grounded = false;


        // Si se presiona la tecla de salto (espacio) y est� en el suelo, salta
>>>>>>> Stashed changes
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Debug.Log("El personaje est� en el suelo.");
            Jump();
            Debug.Log("Intentando saltar");
        }

        
        
    }

    void FixedUpdate()
    {
        // Movimiento horizontal del jugador
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);

        // Obtener los l�mites de la c�mara
        SetMovementLimits();

        // Limitar la posici�n del jugador dentro de los l�mites de la c�mara
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, leftLimit, rightLimit);
        transform.position = position;
    }


    private void Jump()
    {

        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void SetMovementLimits()
    {
        // Obtener la posici�n de los bordes izquierdo y derecho de la c�mara en el mundo
        Vector3 cameraLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 cameraRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        // Definir los l�mites basados en la posici�n de la c�mara
        leftLimit = cameraLeft.x + 1f; // Agregar un margen
        rightLimit = cameraRight.x - 1f; // Agregar un margen
    }


}
