using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject tutorialPanel;
    // Start is called before the first frame update
    void Start()
    {
        creditsPanel.SetActive(false);
        tutorialPanel.SetActive(false);
    }

  

    public void Comenzar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void AbrirCreditos()
    {
        creditsPanel.SetActive(true);
    }

    public void CerrarCreditos()
    {
        creditsPanel.SetActive(false);
    }

    public void AbrirTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void CerrarTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
