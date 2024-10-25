using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EscenaJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Config()
    {
        SceneManager.LoadScene("Config");
    }

    public void InicioMenu()
    {
        SceneManager.LoadScene("InicioMenu");
    }

    public void SalirDelJuego()
    {
        Debug.Log("El juego se cerrará."); // Mensaje para verificar en el editor
        Application.Quit();
    }

}
