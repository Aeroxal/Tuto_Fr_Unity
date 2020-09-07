using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{

   public GameObject[] objects;

    public static DontDestroyOnLoadScene instance;

    // La fonction Awake est lue même avant la fonction start
    private void Awake()
    {

        // Permet de s'assurer qu'il n'y a qu'un seul Inventory
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroyOnLoadScene dans la scène");
            return;
        }

        instance = this;
        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }
    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objects)
        {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());           
        }

    }
}
