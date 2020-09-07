using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Array qui stock toutes les musiques que l'on va jouer
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex=0;

    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instance;

    // La fonction Awake est lue même avant la fonction start
    private void Awake()
    {

        // Permet de s'assurer qu'il n'y a qu'un seul Inventory
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            //Si aucune musique n'est jouée, on passe à la suivante
            playNextSong();
        }
    }

    void playNextSong()
    {
        //Permet de pouvoir rejouer la premiere musique si on arrive à la fin de la liste
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt (AudioClip clip, Vector3 pos)
    {
        //Génération d'un gameOject vide temporaire
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        //Ce qu'il n'y a pas dans la méthode de base de unity PlayClipAtPoint (truc du genre)
        //En effet, avec la méthode d'unity, on a pas accès au Mixer
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        //Le deuxième paramètre clip.lenght renseigne sur la durée du son, afin de procéder à la destruction une fois qu'on a fini de jouer le son
        Destroy(tempGO, clip.length);
        return audioSource;
    }

}
