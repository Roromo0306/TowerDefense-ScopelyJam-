using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject panelVictoria;

    [Header("Tiempo Inicial")]
    public float startTime = 120f;

    private float currentTime;
    private bool isRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        UpdateTimerDisplay();
        panelVictoria.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            return;
        }

        currentTime -= Time.deltaTime;

        if (currentTime <=0f)
        {
            currentTime = 0f;
            isRunning = false;
            Time.timeScale = 0f;
            panelVictoria.SetActive(true);
        }

        UpdateTimerDisplay();
    }


    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = minutes.ToString("00")+ ":" + seconds.ToString("00");
    }

    public void VolverJugar()
    {
        SceneManager.LoadScene("Juego");
        Time.timeScale = 1f;

    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;

    }


}
