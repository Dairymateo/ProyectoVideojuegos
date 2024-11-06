using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Salud máxima
    private int currentHealth;

    public Slider healthSlider;  // Referencia al Slider de la barra de vida


    void Start()
    {
        currentHealth = maxHealth;  // Inicialmente la salud es máxima
        healthSlider.maxValue = maxHealth;  // Establece el valor máximo del Slider
        healthSlider.value = currentHealth; // Establece el valor inicial del Slider
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Resta el daño a la salud actual
        Debug.Log("El jugador recibió " + damage + " puntos de daño. Salud restante: " + currentHealth);

        // Actualizar el Slider con la nueva salud
        healthSlider.value = currentHealth;


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
        Destroy(gameObject);  // Esto destruirá al jugador, pero puedes reemplazarlo por otra lógica
        Debug.Log("Juego terminado. No te queda vida.");
        SceneManager.LoadScene("GameOver");
        
    }

}
