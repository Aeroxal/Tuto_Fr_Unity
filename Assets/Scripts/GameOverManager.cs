using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public GameObject gameOverUI;

    public static GameOverManager instance;

    // La fonction Awake est lue même avant la fonction start
    private void Awake()
    {

        // Permet de s'assurer qu'il n'y a qu'un seul Inventory
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {
        if (CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }
        gameOverUI.SetActive(true);

    }

    //Recommencer le niveau
    public void RetryButton()
    {
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
        // Recharger la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Replacer le joueur au spawn
        PlayerHealth.instance.Respawn();
        // Réactiver les mouvements du joueur et lui remettre de la vie
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        //Permet de garder les personnages d'une scène à une autre
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        //Retour au menu principal
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitButton()
    {
        //Quitter le jeu
        Application.Quit();
    }

}
