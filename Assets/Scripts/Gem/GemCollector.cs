using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GemCollector : MonoBehaviour
{

    [SerializeField] private AudioClip collectSound;
    private GameObject mainCharacter;
    private AudioSource audioSource; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }
        mainCharacter = GameObject.Find("MainCharacter");


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            GameObject gemManager = GameObject.Find("GemsManager");

            GemManager gemManagerScript = gemManager.GetComponent<GemManager>();

            gemManagerScript.changeText();

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int child = gemManager.transform.childCount;

            GameManager.Instance.addGams();
            collision.GetComponent<EstadoPlayer>().RecuperarVida();

            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            if (currentSceneIndex == 5)
            {
                if (child == 7)
                {
                    mainCharacter.transform.position = new Vector2(-6.94f, -4f);
                }
                else if (child == 4)
                {
                    mainCharacter.transform.position = new Vector2(12.03f, -4.93f);
                }
            }

            Destroy(gameObject, collectSound.length-0.5f);
        }
    }

}
