using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0.7f; // Ajusta la velocidad de la animación (0.5 es la mitad de la velocidad normal)
    }

    // Si deseas cambiar la velocidad en otros momentos, puedes agregar métodos adicionales aquí.
}
