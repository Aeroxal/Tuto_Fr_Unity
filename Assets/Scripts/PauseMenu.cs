using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject settingsWindow;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    void Paused()
    {
        DeplacementJoueur.instance.enabled = false;
        //Activer notre menu pause, donc l'afficher
        pauseMenuUI.SetActive(true);
        //Arrêter le temps
        Time.timeScale = 0;
        //Changer le statut du jeu
        gameIsPaused = true;
    }

    //Fais exactement l'inverse de la fonction Paused()
    public void Resume()
    {
        DeplacementJoueur.instance.enabled = true;
        //Désactiver notre menu pause, donc l'afficher
        pauseMenuUI.SetActive(false);
        //Reprendre le temps
        Time.timeScale = 1;
        //Changer le statut du jeu
        gameIsPaused = false;
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }
    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }


    public void LoadMainMenu()
    {
        //Toujours appeller cette méthode lorsque l'on ne veut pas garder le joueur, le canvas par exemple.
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
