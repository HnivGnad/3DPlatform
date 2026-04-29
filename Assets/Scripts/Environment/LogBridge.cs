using UnityEngine;
using System.Collections;

public class LogBridge : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 fallRotation = new Vector3(0, 0, -90); // Hướng xoay để nằm ngang
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
        Debug.Log("<color=cyan>Khúc gỗ đang đổ xuống làm cầu...</color>");

        Quaternion startRotation = transform.rotation;
        // Tính toán góc quay đích dựa trên góc hiện tại cộng thêm độ lệch
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + fallRotation);
        
        float elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fallDuration;
            
            // Sử dụng hiệu ứng mượt (SmoothStep)
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, smoothT);
            yield return null;
        }

        transform.rotation = targetRotation;
        Debug.Log("<color=green>Cầu gỗ đã sẵn sàng!</color>");
    }
}
