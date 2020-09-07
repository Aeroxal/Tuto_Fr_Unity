using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;
    public int coinsPickedUpInThisSceneCount;

    public static CurrentSceneManager instance;

    // La fonction Awake est lue même avant la fonction start
    private void Awake()
    {

        // Permet de s'assurer qu'il n'y a qu'un seul Inventory
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la scène");
            return;
        }

        instance = this;
    }
}
