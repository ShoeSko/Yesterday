using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [Header("Base Info")]
    public AudioMixer mixer;
    public Slider slider;
    private SaveSystem saving;
    public GameObject soundSettings;

    [Header("Which Mixer is this?")]
    public bool masterMixer;
    public bool musicMixer;
    public bool sfxMixer;

    private void Awake()
    {
        if (soundSettings && soundSettings.activeInHierarchy == false) //Refreshes the sound on start.
        {
            soundSettings.SetActive(true);
            soundSettings.GetComponent<CanvasGroup>().alpha = 0;
            soundSettings.GetComponent<CanvasGroup>().interactable = false;
            StartCoroutine(LoadTheVolumeLevel());
        }
    }
    IEnumerator LoadTheVolumeLevel()
    {
        yield return new WaitForSeconds(0.5f);
        soundSettings.SetActive(false);
        soundSettings.GetComponent<CanvasGroup>().alpha = 1;
        soundSettings.GetComponent<CanvasGroup>().interactable = true;
    }

    private void Start()
    {
        if (FindObjectOfType<SaveSystem>() && mixer != null)
        {
            saving = FindObjectOfType<SaveSystem>().GetComponent<SaveSystem>(); //Grabs the save system from the scene.  This line can be reused for all scripts needing to save info.

            if (masterMixer)
            {
                mixer.SetFloat("MasterVolume", Mathf.Log10(saving.data.masterVolLevel) * 20); //Sets the volume to the stored data from save.
                slider.value = saving.data.masterVolLevel;
            }
            else if (musicMixer)
            {
                mixer.SetFloat("MusicVolume", Mathf.Log10(saving.data.musicVolLevel) * 20);
                slider.value = saving.data.musicVolLevel;
            }
            else if (sfxMixer)
            {
                mixer.SetFloat("SFXVolume", Mathf.Log10(saving.data.sfxVolLevel) * 20);
                slider.value = saving.data.sfxVolLevel;
            }
        }
    }

    public void SetLevelMaster(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        saving.data.masterVolLevel = sliderValue; //Sets the volume level in the save file. Still needs to activate the save function.
    }
    public void SetLevelMusic(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        saving.data.musicVolLevel = sliderValue;
    }
    public void SetLevelSFX(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        saving.data.sfxVolLevel = sliderValue;
    }
}




//public void SetLevel(float sliderValue)
//{
        //mixer.SetFloat("INSERT NAME OF EXPOSED PARAMETER", Mathf.Log10(sliderValue) * 20); // Gives the slider value a log value more aligned to the actual sound.
//}