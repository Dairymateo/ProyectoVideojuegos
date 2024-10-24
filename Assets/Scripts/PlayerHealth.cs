using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Salud máxima
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;  // Inicialmente la salud es máxima
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Resta el daño a la salud actual
        Debug.Log("El jugador recibió " + damage + " puntos de daño. Salud restante: " + currentHealth);

        // Verificar si la salud es 0 o menor
        if (currentHealth <= 0)
        {
            Die();  // Llamar al método de muerte
        }
    }

    // Método para morir
    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
        // Aquí puedes implementar la lógica de muerte, como reiniciar el nivel, mostrar una pantalla de "Game Over", etc.
        Destroy(gameObject);  // Esto destruirá al jugador, pero puedes reemplazarlo por otra lógica
    }

    // (Opcional) Método para recuperar salud
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;  // Asegurarse de que no exceda la salud máxima
        }

        Debug.Log("El jugador ha sido curado. Salud actual: " + currentHealth);
    }
}
