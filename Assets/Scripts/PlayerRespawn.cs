using UnityEngine;
using System;

public class PlayerRespawn : MonoBehaviour
{
    public static event Action OnPlayerRespawn;
    private Vector3 respawnPoint;
    public GameObject ufoPowerupPrefab;
    private Vector3 ufoPowerupSpawnPosition;

    private void Start()
    {
        respawnPoint = transform.position;
        ufoPowerupSpawnPosition = ufoPowerupPrefab.transform.position;
    }

    public void SetCheckpoint(Vector3 newPos)
    {
        respawnPoint = newPos;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        PlayerMovement pm = GetComponent<PlayerMovement>();
        if (pm != null) pm.DeactivateUFOForm();
        if (ufoPowerupPrefab != null)
        {
            ufoPowerupPrefab.SetActive(true);
            ufoPowerupPrefab.transform.position = ufoPowerupSpawnPosition;
        }
        OnPlayerRespawn?.Invoke();
    }
}