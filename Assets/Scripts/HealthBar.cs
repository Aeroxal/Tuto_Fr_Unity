using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    // Appellée à l'initialisation de la vie du joueur afin de lui mettre tous ses pdv
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        //1f = 100% du gradient, la couleur attribuée est donc tout à droite du gradient
        fill.color = gradient.Evaluate(1f);
    }

    // Indiquer les pdv
    public void SetHealth(int health)
    {
        slider.value = health;

        //Même principe, sauf qu'on récupere la valeur du slider normalisée (entre 0 et1, et non pas 0 à 100)
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
