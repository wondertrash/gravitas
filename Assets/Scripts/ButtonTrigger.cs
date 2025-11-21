using TMPro;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Door doorToOpen;
    private bool isActivated = false;
    private Vector2 startPosition;
    Vector2 targetPosition;

    public void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition;
    }
    private void OnEnable()
    {
        PlayerRespawn.OnPlayerRespawn += ResetButton;
    }

    private void OnDisable()
    {
        PlayerRespawn.OnPlayerRespawn -= ResetButton;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated && collision.CompareTag("Player"))
        {
            isActivated = true;
            targetPosition = new Vector2(transform.position.x, transform.position.y - 0.4f);
            doorToOpen.OpenDoor();
        }
    }
    private void Update()
    {
        float smoothing = 5f;
        transform.position = Vector2.Lerp(transform.position, targetPosition, 1 - Mathf.Exp(-smoothing * Time.deltaTime));
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
    }
    public void ResetButton()
    {
        isActivated = false;
        targetPosition = startPosition;
        transform.position = startPosition;
    }
}