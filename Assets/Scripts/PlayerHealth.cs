using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 1f;
    public float currentHealth;

    private PlayerRespawn respawn;

    private void Start()
    {
        currentHealth = maxHealth;
        respawn = GetComponent<PlayerRespawn>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        currentHealth = maxHealth;
        respawn.Respawn();
    }
}
