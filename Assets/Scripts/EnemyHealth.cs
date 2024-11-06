using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public Slider healthSlider;  // Referencia al Slider de la barra de vida


    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;  // Establece el valor máximo del Slider
        healthSlider.value = currentHealth; // Establece el valor inicial del Slider
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Salud restante del enemigo: " + currentHealth);
        // Actualizar el Slider con la nueva salud
        healthSlider.value = currentHealth;


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
