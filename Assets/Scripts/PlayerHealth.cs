using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Salud m�xima
    private int currentHealth;

    public Slider healthSlider;  // Referencia al Slider de la barra de vida


    void Start()
    {
        currentHealth = maxHealth;  // Inicialmente la salud es m�xima
        healthSlider.maxValue = maxHealth;  // Establece el valor m�ximo del Slider
        healthSlider.value = currentHealth; // Establece el valor inicial del Slider
    }

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Resta el da�o a la salud actual
        Debug.Log("El jugador recibi� " + damage + " puntos de da�o. Salud restante: " + currentHealth);

        // Actualizar el Slider con la nueva salud
        healthSlider.value = currentHealth;


        // Verificar si la salud es 0 o menor
        if (currentHealth <= 0)
        {
            Die();  // Llamar al m�todo de muerte
        }
    }

    // M�todo para morir
    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
        Destroy(gameObject);  // Esto destruir� al jugador, pero puedes reemplazarlo por otra l�gica
        Debug.Log("Juego terminado. No te queda vida.");
        SceneManager.LoadScene("GameOver");
        
    }

}
