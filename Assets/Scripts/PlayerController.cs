using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Di chuyển")]
    public float moveSpeed = 6f;

    [Header("Bắn đạn")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 0.1f;

    [Header("Âm thanh")]
    public AudioSource audioSource;
    public AudioClip shootSound;

    private float nextShootTime;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.IsGameOver)
        {
            return;
        }

        Move();
        Shoot();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        transform.Translate(
            Vector3.right * horizontal * moveSpeed * Time.deltaTime
        );

        LimitInsideCamera();
    }

    private void LimitInsideCamera()
    {
        if (mainCamera == null || spriteRenderer == null)
        {
            return;
        }

        Vector3 leftBorder =
            mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        Vector3 rightBorder =
            mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float halfWidth = spriteRenderer.bounds.extents.x;

        float limitedX = Mathf.Clamp(
            transform.position.x,
            leftBorder.x + halfWidth,
            rightBorder.x - halfWidth
        );

        transform.position = new Vector3(
            limitedX,
            transform.position.y,
            transform.position.z
        );
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            Time.time >= nextShootTime)
        {
            nextShootTime = Time.time + shootCooldown;

            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
            );

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}