using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnImage;
    [SerializeField] Image soundOffImage;

    [SerializeField] Image soundOnImageOnPause;
    [SerializeField] Image soundOffImageOnPause;

    private bool muted = false;
    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            LoadSound();
        }
        else
        {
            LoadSound();
        }
        UpdateButtonIcon();

        AudioListener.pause = muted;
    }


    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }

        SaveSound();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            soundOnImage.enabled = true;
            soundOffImage.enabled = false;

            soundOnImageOnPause.enabled = true;
            soundOffImageOnPause.enabled = false;
        }
        else 
        {
            soundOnImage.enabled = false;
            soundOffImage.enabled = true;

            soundOnImageOnPause.enabled = false;
            soundOffImageOnPause.enabled = true;

        }
    }


    private void LoadSound()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void SaveSound()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
