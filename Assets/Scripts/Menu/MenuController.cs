using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject btnContinuar;
    [SerializeField]
    private GameObject panelOpcion;
    [SerializeField]
    private GameObject panelSkins;
    [SerializeField]
    private GameObject panelDificultad;
    [SerializeField]
    private Text foxText;
    [SerializeField]
    private Text frogText;
    [SerializeField]
    private Toggle fullscreenToggle;
    private void Start()
    {

        int savedSceneIndex = PlayerPrefs.GetInt("savedScene");

        if (savedSceneIndex > 1 && btnContinuar != null)
        {
            btnContinuar.SetActive(true);
        }
        else if (btnContinuar != null)
        {
            btnContinuar.SetActive(false);
        }
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.buildIndex == 0)
        {
            if (GameManager.Instance.playerSelect == GameManager.Player.Fox)
            {
                this.btnFox();
            }
            else
            {
                this.btnFrog();
            }
        }


    }
    public void StartGame()
     {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("savedScene", 1);
        SceneManager.LoadScene("Nivel1");
        Time.timeScale = 1;
        GameManager.Instance.setIsDialog(false);
     }

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Resume()
    {
        GameManager.Instance.TogglePause(false);
    }

    public void MenuPrincipal()
    {
        GameManager.Instance.TogglePause(false);
        SceneManager.LoadScene("Menu");
    }

    public void Opcion()
    {
        if (panelOpcion.active)
        {
            panelOpcion.SetActive(false);
        }
        else
        {
            panelOpcion.SetActive(true);
        }
    }

    public void Skins()
    {
        if (panelSkins.active)
        {
            panelSkins.SetActive(false);
        }
        else
        {
            panelSkins.SetActive(true);
        }
    }

    public void Dificultad()
    {
        if (panelDificultad.active)
        {
            panelDificultad.SetActive(false);
        }
        else
        {
            panelDificultad.SetActive(true);
        }
    }

    public void btnEasy()
    {
        GameManager.Instance.playerVidas = 3;
        StartGame();
    }

    public void btnMedium()
    {
        GameManager.Instance.playerVidas = 2;
        StartGame();
    }
    public void btnHard()
    {
        GameManager.Instance.playerVidas = 1;
        StartGame();
    }

    public void Continuar()
    {
        int savedSceneIndex = PlayerPrefs.GetInt("savedScene");
        SceneManager.LoadScene(savedSceneIndex);
    }

    public void RestartLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.DeleteKey("checkPointPositionX");
        PlayerPrefs.DeleteKey("checkPointPositionY");
        PlayerPrefs.DeleteKey("GemsRecogidos");
        PlayerPrefs.Save();
        this.Resume();
        SceneManager.LoadScene(activeSceneIndex);
    }

    public void btnFox()
    {
        GameManager.Instance.playerSelect = GameManager.Player.Fox;
        Color newColor;
        foxText.text = "Elegido";
        if (ColorUtility.TryParseHtmlString("#ADD833", out newColor))
        {
            foxText.color = newColor;
        }
        frogText.text = "Elegir";
        if (ColorUtility.TryParseHtmlString("#ADD8E6", out newColor))
        {
            frogText.color = newColor;
        }
    }

    public void btnFrog()
    {
        GameManager.Instance.playerSelect = GameManager.Player.Frog;
        Color newColor;

        frogText.text = "Elegido";
        if (ColorUtility.TryParseHtmlString("#ADD833", out newColor))
        {
            frogText.color = newColor;
        }
        foxText.text = "Elegir";
        if (ColorUtility.TryParseHtmlString("#ADD8E6", out newColor))
        {
            foxText.color = newColor;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
