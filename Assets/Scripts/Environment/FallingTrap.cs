using UnityEngine;
using System.Collections;

public class FallingTrap : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallDistance = 5f;
    public float fallDuration = 1.5f;
    public string playerTag = "Player";

    [Header("Detection Area (Omnidirectional)")]
    public Vector3 detectionArea = new Vector3(3f, 3f, 3f);
    public Vector3 detectionOffset = Vector3.zero;
    public LayerMask playerLayer;

    private bool hasTriggered = false;

    private void Update()
    {
        if (!hasTriggered)
        {
            CheckOverlapArea();
        }
    }

    private void CheckOverlapArea()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position + detectionOffset, detectionArea / 2, transform.rotation, playerLayer);
        
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag(playerTag))
            {
                StartCoroutine(FallRoutine());
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Die();
            }
            if (!hasTriggered)
            {
                StartCoroutine(FallRoutine());
            }
        }
    }

    private IEnumerator FallRoutine()
    {
        hasTriggered = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.down * fallDistance;
        float elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fallDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);
            
            yield return null;
        }

        transform.position = targetPosition;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.matrix = Matrix4x4.TRS(transform.position + detectionOffset, transform.rotation, Vector3.one);
        Gizmos.DrawCube(Vector3.zero, detectionArea);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, detectionArea);
    }
}
