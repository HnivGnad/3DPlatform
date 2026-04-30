using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraCutController : MonoBehaviour
{
    [Header("Cinemachine Settings")]
    public CinemachineCamera doorCamera;
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
            doorCamera.Priority = 20;
            yield return new WaitForSeconds(cutDuration);
            doorCamera.Priority = 0;
        }
        else
        {
            Debug.LogWarning("CameraCutController: Chưa gán Door Camera!");
        }

    }
    public void ResetTrigger()
    {
        hasTriggered = false;
    }
}
