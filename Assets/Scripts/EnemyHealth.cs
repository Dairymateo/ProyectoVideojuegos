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
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El enemigo ha muerto");
        // Aqu� puedes agregar l�gica para la muerte del enemigo, como reproducir una animaci�n de muerte o destruir el objeto
        Destroy(gameObject);
    }
}
