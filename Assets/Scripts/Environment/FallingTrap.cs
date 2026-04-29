using UnityEngine;
using System.Collections;

public class FallingTrap : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallDistance = 5f;
    public float fallDuration = 1.5f;
    public string playerTag = "Player";

    [Header("Detection (BoxCast Upwards)")]
    public bool useBoxCast = true;
    public Vector3 boxSize = new Vector3(1f, 0.5f, 1f); // Kích thước của hộp quét
    public float detectionHeight = 2f; // Độ cao mà hộp quét bắn lên
    public LayerMask playerLayer;

    private bool hasTriggered = false;

    private void Update()
    {
        if (useBoxCast && !hasTriggered)
        {
            CheckForPlayerAbove();
        }
    }

    private void CheckForPlayerAbove()
    {
        RaycastHit hit;
        // Sử dụng BoxCast hướng lên trên để quét một vùng rộng hơn
        if (Physics.BoxCast(transform.position, boxSize / 2, Vector3.up, out hit, transform.rotation, detectionHeight, playerLayer))
        {
            if (hit.collider.CompareTag(playerTag))
            {
                StartCoroutine(FallRoutine());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag) && !hasTriggered)
        {
            StartCoroutine(FallRoutine());
        }
    }

    private IEnumerator FallRoutine()
    {
        hasTriggered = true;
        
        Debug.Log("<color=red>GAME OVER! Bẫy đã kích hoạt.</color>");

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.down * fallDistance;
        float elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fallDuration;

            // Hiệu ứng làm mượt (Easing)
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);
            
            yield return null;
        }

        transform.position = targetPosition;
        Debug.Log("Khối đã rơi xong.");
    }

    // Vẽ BoxCast trong Editor
    private void OnDrawGizmosSelected()
    {
        if (useBoxCast)
        {
            Gizmos.color = Color.yellow;
            Vector3 center = transform.position + Vector3.up * (detectionHeight / 2);
            Vector3 size = new Vector3(boxSize.x, detectionHeight, boxSize.z);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
