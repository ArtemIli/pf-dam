using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;

    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = GameManager.Instance.GetVolume();
    }

    public void OnValueChanged()
    {
        GameManager.Instance.ChangeVolume(volumeSlider.value);
    }
}
