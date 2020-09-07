using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public Dropdown resolutionDropDown;

    Resolution[] resolutions;

    //Création variable pour récupere nos sliders Music & Sound
    public Slider musicSlider;
    public Slider soundSlider;

    public void Start()
    {
        //Récupérer les volumes de nos pistes audio Music & Sound
        audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;
        audioMixer.GetFloat("Sound", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;

        //Permet de récupérer toutes les résolutions dispo pour notre écran sans dupplication (grace à Distinct)
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        // Effacer les options présentes par défaut
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            // Permet d'afficher par défaut la résolution de l'ordinateur du joueur
            if(resolutions[i].width==Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        // Permet de rafraichir l'élément actuellement séléctionné (pour ne pas avoir de décallage entre ce qui est séléctionné et affiché
        resolutionDropDown.RefreshShownValue();

        Screen.fullScreen = true;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("Sound", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //Avec cette méthode, on modifie réellement la résolution de l'ordinateur du joueur.
    //Dans les méthodes précedentes, on annoncait les changements sans les réaliser
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
