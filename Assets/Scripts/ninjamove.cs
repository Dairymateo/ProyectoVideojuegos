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


        Debug.DrawRay(transform.position, Vector3.down * 1.2f,Color.yellow);
        if(Physics2D.Raycast(transform.position, Vector3.down, 1.2f))
        {
            Grounded = true;
            Debug.Log("grounded true");
        }
        else Grounded = false;


        // Si se presiona la tecla de salto (espacio) y está en el suelo, salta
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
            Debug.Log("Intentando saltar");
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
}
