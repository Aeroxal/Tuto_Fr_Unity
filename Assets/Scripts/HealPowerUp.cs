using UnityEngine;

public class HealPowerUp : MonoBehaviour
{

    public int healthPoint;

    public AudioClip pickUpSoud;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerHealth.instance.currentHealth < PlayerHealth.instance.maxHealth)
            {
                AudioManager.instance.PlayClipAt(pickUpSoud, transform.position);
                PlayerHealth.instance.HealPlayer(healthPoint);
                Destroy(gameObject);
            }
        }
    }
}
