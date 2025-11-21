using UnityEngine;
public class WaterArea : MonoBehaviour
{
    public float speedMultiplier = 0.5f;
    public float jumpMultiplier = 0.6f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            player.speed *= speedMultiplier;
            player.jumpStrength *= jumpMultiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.ResetMovementStats();
        }
    }
}
