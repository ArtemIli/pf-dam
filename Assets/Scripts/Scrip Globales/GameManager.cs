using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{

    public int totalGems;
    public bool isPaused;
    public enum Player {Fox, Frog}
    public Player playerSelect;
    private GameObject mainCharacter;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] public RuntimeAnimatorController[] collectAnimator;
    [SerializeField] public Sprite[] collectSprites;
    [SerializeField] public float playerVidas;
    private AudioSource audioSource;
    private Boolean isDialog;
    private float musicVolume = 1f;

    //Instance
    public static GameManager Instance;

    private void Awake()
    {
        //Comprobar si eya existe instanse
        if(Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.loop = true;
                audioSource.playOnAwake = true;
                audioSource.clip = collectSound;
                audioSource.Play();
                audioSource.volume = musicVolume;
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isDialog = false;
        Application.targetFrameRate = 60;
    }

    public void addGams()
    {
        totalGems++;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(false);
        }
        audioSource.volume = musicVolume;
    }

    public void TogglePause(Boolean isDialog)
    {
        if(isDialog)
        {
            if (this.isDialog)
            {
                this.isDialog = false;
                Time.timeScale = 1;
            }
            else
            {
                this.isDialog = true;
                Time.timeScale = 0;
            }
        }else
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (!SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            canvas.transform.GetChild(3).gameObject.SetActive(true);
        }
        Time.timeScale = 0;
        isPaused = true;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        audioSource.volume = musicVolume;
    }

    public float GetVolume()
    {
        return musicVolume;
    }

    public void ChangeVolume(float volume)
    {
        SetVolume(volume);
    }

    private void Resume()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (!SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            canvas.transform.GetChild(3).gameObject.SetActive(false);
        }
        if (!this.isDialog)
        {
           Time.timeScale = 1;
        }
        isPaused = false;
    }

    public void setIsDialog(Boolean isDialog) {
        this.isDialog = isDialog;
    }

}
