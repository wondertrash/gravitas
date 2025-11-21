using System;
using UnityEngine;

public class UFOPowerUp : MonoBehaviour
{
    public float ufoDuration = 5f;
    private Vector3 startPosition;
    private bool collected = false;

    private void Start()
    {
        startPosition = transform.position;
        PlayerRespawn.OnPlayerRespawn += ResetPowerup;
    }

    private void OnDestroy()
    {
        PlayerRespawn.OnPlayerRespawn -= ResetPowerup;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.ActivateUFOForm(ufoDuration);
            collected = true;
            gameObject.SetActive(false);
        }
    }

    private void ResetPowerup()
    {
        if (collected)
        {
            collected = false;
            gameObject.SetActive(true);
            transform.position = startPosition;
        }
    }
}
