using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Di chuyển")]
    public float fallSpeed = 3f;

    [Header("Điểm")]
    public int scoreValue = 10;

    [Header("Hiệu ứng")]
    public GameObject explosionPrefab;
    public AudioClip explosionSound;

    private Camera mainCamera;
    private bool isDead;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.IsGameOver)
        {
            return;
        }

        transform.Translate(
            Vector3.down * fallSpeed * Time.deltaTime
        );

        DestroyWhenOutsideCamera();
    }

    private void DestroyWhenOutsideCamera()
    {
        if (mainCamera == null)
        {
            return;
        }

        Vector3 viewportPosition =
            mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.y < -0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            isDead = true;

            PlayerController player =
                other.GetComponent<PlayerController>();

            CreateExplosion(other.transform.position);

            if (player != null)
            {
                player.Die();
            }

            Destroy(gameObject);
        }
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        CreateExplosion(transform.position);
        Destroy(gameObject);
    }

    private void CreateExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                position,
                Quaternion.identity
            );
        }

        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(
                explosionSound,
                position
            );
        }
    }
}