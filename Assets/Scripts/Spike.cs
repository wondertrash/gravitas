using UnityEngine;

public class Spike : MonoBehaviour
{
    public bool instantKill = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var health = collision.GetComponent<PlayerHealth>();
            var respawn = collision.GetComponent<PlayerRespawn>();

            if (health != null)
            {
                var audioSource = GetComponent<AudioSource>();
                audioSource.Play();
                health.TakeDamage(health.currentHealth);
            }
        }
    }
}
