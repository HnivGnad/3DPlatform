using UnityEngine;
using System.Collections;
using Unity.Cinemachine; // Namespace cho Cinemachine 3.0

public class CameraCutController : MonoBehaviour
{
    [Header("Cinemachine Settings")]
    public CinemachineCamera doorCamera; // Kéo Camera phụ nhìn vào cửa vào đây
    public float cutDuration = 1.5f;

    [Header("Optional")]
    public float delayBeforeCut = 0f;

    private bool hasTriggered = false;

    public void TriggerCameraCut()
    {
        if (hasTriggered) return;
        StartCoroutine(CutRoutine());
    }

    private IEnumerator CutRoutine()
    {
        hasTriggered = true;

        if (delayBeforeCut > 0)
            yield return new WaitForSeconds(delayBeforeCut);

        if (doorCamera != null)
        {
            Debug.Log("<color=orange>Camera: Đang lia tới cửa...</color>");
            
            // Tăng Priority để máy quay chuyển sang nhìn cửa
            doorCamera.Priority = 20;

            yield return new WaitForSeconds(cutDuration);

            // Giảm Priority để máy quay trả về cho Player
            doorCamera.Priority = 0;
            
            Debug.Log("<color=orange>Camera: Đang quay về Player.</color>");
        }
        else
        {
            Debug.LogWarning("CameraCutController: Chưa gán Door Camera!");
        }
        
        // Cho phép kích hoạt lại sau khi kết thúc (tùy chọn)
        // hasTriggered = false; 
    }

    // Hàm reset nếu cần kích hoạt lại nhiều lần
    public void ResetTrigger()
    {
        hasTriggered = false;
    }
}
