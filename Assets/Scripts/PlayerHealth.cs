using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public float invicibilityTimeAfterHit = 3f;
    public float invicibilityFlashDelay = 0.2f;
    public bool isInvicible = false;

    public SpriteRenderer graphics;

    public HealthBar healthBar;

    public AudioClip hitSound;

    public static PlayerHealth instance;

    // La fonction Awake est lue même avant la fonction start
    private void Awake()
    {

        // Permet de s'assurer qu'il n'y a qu'un seul Inventory
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDammage(60);
        }
    }

    public void HealPlayer(int amount)
    {

        if ((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDammage(int dammage)
    {
        if (!isInvicible) // Il est invicible
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);

            currentHealth -= dammage;
            healthBar.SetHealth(currentHealth);

            //Vérifier si le joueur est toujours vivant
            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvicible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }
    }

    public void Die()
    {
        Debug.Log("Le joueur est éliminé");
        // Bloquer les mvts. Pour cela, on va désactiver le script DéplacementJoueur
        DeplacementJoueur.instance.enabled = false;
        // Joueur l'animation d'élimination
        DeplacementJoueur.instance.animator.SetTrigger("Die");
        // Empecher les interactions physiques avec les autres éléments de la scène
        DeplacementJoueur.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        DeplacementJoueur.instance.rb.velocity = Vector3.zero;
        DeplacementJoueur.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();

    }

    public void Respawn()
    {
        // Réactiver les mvts. Pour cela, on va activer le script DéplacementJoueur
        DeplacementJoueur.instance.enabled = true;
        // Réactiver le système d'animation par défaut
        DeplacementJoueur.instance.animator.SetTrigger("Respawn");
        // Permettre les interactions physiques avec les autres éléments de la scène
        DeplacementJoueur.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        DeplacementJoueur.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvicibilityFlash()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvicible = false;
    }

}
