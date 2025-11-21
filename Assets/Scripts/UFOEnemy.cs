using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.left;
    public float speed = 2f;
    public bool oscillate = false;
    public float oscillateDistance = 3f;

    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (oscillate)
        {
            float offset = Mathf.Sin(Time.time * speed) * oscillateDistance;
            transform.position = startPos + moveDirection.normalized * offset;
        }
        else
        {
            transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
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