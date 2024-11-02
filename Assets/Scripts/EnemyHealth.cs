using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Salud restante del enemigo: " + currentHealth);

        if (currentHealth <= 0)
        {
            // Llama al método Die en el componente EnemyYellowNinja
            GetComponent<EnemyYellowNinja>().Die();
        }
    }



    private void Die()
    {
        Debug.Log("El enemigo ha muerto"); // Esto debe imprimirse cuando el enemigo muere
                                           // ... el resto de tu lógica
    }

}
