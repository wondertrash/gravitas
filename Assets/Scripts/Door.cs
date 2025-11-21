using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D doorCollider;
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        startPosition = transform.position;
        targetPosition = startPosition;
    }
    private void OnEnable()
    {
        PlayerRespawn.OnPlayerRespawn += ResetDoor;
    }

    private void OnDisable()
    {
        PlayerRespawn.OnPlayerRespawn -= ResetDoor;
    }
    public void OpenDoor()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        doorCollider.enabled = false;
        targetPosition = new Vector2(transform.position.x, transform.position.y - 1f);
    }
    public void ResetDoor()
    {
        doorCollider.enabled = true;
        targetPosition = startPosition;
        transform.position = startPosition;
    }

    private void Update()
    {
        float smoothing = 5f;
        transform.position = Vector2.Lerp(transform.position, targetPosition, 1 - Mathf.Exp(-smoothing * Time.deltaTime));
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
    }
}
