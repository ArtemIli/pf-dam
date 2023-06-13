using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EstadoPlayer : MonoBehaviour
{
    private float vidas;
    public float invulnerabilityDuration = 2f;
    public float blinkInterval = 0.1f;
    private float checkPointPositionX, checkPointPositionY;
    private int GemsRecogidos;
    public GameObject panelCorazones;
    public Sprite corazonFull;
    public Sprite corazonVacio;

    private SpriteRenderer spriteRenderer;
    private bool isInvulnerable = false;
    private Image [] corazones;
    [SerializeField]
    private AudioClip hurtSound;

    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        corazones = panelCorazones.GetComponentsInChildren<Image>();
        vidas = GameManager.Instance.playerVidas;
        if (panelCorazones.GetComponent<Image>() != null)
        {
            corazones = corazones.Skip(1).ToArray();
        }

        if (GameManager.Instance.playerVidas == 1)
        {
            corazones[1].enabled = false;
            corazones[2].enabled = false;
        }
        else if (GameManager.Instance.playerVidas == 2)
        {
            corazones[2].enabled = false;
        }

        if (PlayerPrefs.GetFloat("checkPointPositionX") != 0 &
            PlayerPrefs.GetInt("checkPointScene") == SceneManager.GetActiveScene().buildIndex)
        {
            transform.position = (new Vector2(PlayerPrefs.GetFloat("checkPointPositionX"),
                PlayerPrefs.GetFloat("checkPointPositionY")));
            int gemCounter = PlayerPrefs.GetInt("GemsRecogidos");
            GameObject gemManager = GameObject.Find("GemsManager");
            GemManager gemManagerScript = gemManager.GetComponent<GemManager>();
            for(int i = 0; i < gemCounter; i++)
            {
                gemManagerScript.changeText();
                Destroy(gemManagerScript.transform.GetChild(i).gameObject);
            }
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }

    }

    public void PerderVida(float damage)
    {
        if (isInvulnerable) return;
        if (hurtSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }
        vidas -= damage;
        ActulizarCorazones();
        if (vidas <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
    }

    public void RecuperarVida()
    {
        if (vidas < GameManager.Instance.playerVidas)
        {
            vidas++;
            ActulizarCorazones();
        }
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;

        float endTime = Time.time + invulnerabilityDuration;
        while (Time.time < endTime)
        {
            if(vidas < 3)
            {
                corazones[(int)vidas].enabled = !corazones[(int)vidas].enabled;
            }
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.enabled = true;
        if (vidas < 3)
        {
            corazones[(int)vidas].enabled = true;
        }
        isInvulnerable = false;
    }

    private void ActulizarCorazones()
    {
        for(int i =0; i < corazones.Length; i++)
        {
            if (i < vidas)
            {
                corazones[i].sprite = corazonFull;
            }
            else
            {
                corazones[i].sprite = corazonVacio;
            }
            corazones[i].enabled = true;
        }
    }

    private void Die()
    {
        GetComponent<Animator>().Play("Hit");
        StartCoroutine(RestartAfterAnimation());
    }

    private IEnumerator RestartAfterAnimation()
    {
        // Espera a que la animación actual termine
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    public void ReachedCheckPoint(float x, float y)
    {
        if (PlayerPrefs.GetFloat("checkPointPositionX") != x)
        {
            PlayerPrefs.SetFloat("checkPointPositionX", x);

            PlayerPrefs.SetFloat("checkPointPositionY", y);
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("checkPointScene", activeSceneIndex);
            GameObject gemManager = GameObject.Find("GemsManager");
            GemManager gemManagerScript = gemManager.GetComponent<GemManager>();
            PlayerPrefs.SetInt("GemsRecogidos", 8 - gemManagerScript.transform.childCount);
        }
    }

}