using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed = 10f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.Translate(
            Vector3.up * moveSpeed * Time.deltaTime
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

        if (viewportPosition.y > 1.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy =
            other.GetComponent<EnemyController>();

        if (enemy != null)
        {
            enemy.Die();
            Destroy(gameObject);
        }
    }
}