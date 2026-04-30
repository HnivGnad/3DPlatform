using UnityEngine;
using System.Collections;

public class LogBridge : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 fallRotation = new Vector3(0, 0, -90);
    public float fallDuration = 1.5f;
    public string playerTag = "Player";
    
    [Header("Detection")]
    public bool useCollision = true;
    public bool useTrigger = false;

    private bool hasTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (useCollision && collision.gameObject.CompareTag(playerTag) && !hasTriggered)
        {
            TriggerFall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (useTrigger && other.CompareTag(playerTag) && !hasTriggered)
        {
            TriggerFall();
        }
    }

    public void TriggerFall()
    {
        if (hasTriggered) return;
        StartCoroutine(FallRoutine());
    }

    private IEnumerator FallRoutine()
    {
        hasTriggered = true;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + fallRotation);
        
        float elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fallDuration;
            
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, smoothT);
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
