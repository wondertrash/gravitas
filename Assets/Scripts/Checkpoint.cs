using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerRespawn player = collision.GetComponent<PlayerRespawn>();

        if (player != null)
        {
            player.SetCheckpoint(transform.position);
        }
    }
}
