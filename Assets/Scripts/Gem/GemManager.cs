using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{

    private Text textMision;
    private int childTotal;
    private int childRec;

    void Start()
    {
        GameObject textObject = GameObject.Find("textMision");
        textMision = textObject.GetComponent<Text>();
        childRec = 0;
        childTotal = transform.childCount;
        textMision.text = childRec + "/"+ childTotal;
    }

    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int child = transform.childCount;
        if (child == 0)
        {
            int activeScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = activeScene + 1;

            if(nextScene < SceneManager.sceneCountInBuildSettings)
            {
                PlayerPrefs.SetInt("savedScene", currentSceneIndex+1);
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                PlayerPrefs.SetInt("savedScene", 1);
                SceneManager.LoadScene(0);
            }

        }
    }

    public void changeText()
    {
        childRec++;
        textMision.text = childRec + "/" + childTotal;
    }
}
