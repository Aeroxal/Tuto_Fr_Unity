using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public Text coinsCountText;

    public static Inventory instance;

    // La fonction Awake est lue même avant la fonction start
    private void Awake()
    {

        // Permet de s'assurer qu'il n'y a qu'un seul Inventory
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }
        
        instance = this;
    }

    // Permet d'ajouter les pièces à notre inventaire
    public void AddCoins(int count)
    {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }

    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        coinsCountText.text = coinsCount.ToString();
    }

}
