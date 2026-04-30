using UnityEngine;

public class MobileControlAutoHider : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Kéo Canvas chứa Joystick và Nút nhảy vào đây")]
    public GameObject mobileCanvas;

    [Header("Settings")]
    public bool forceShowInEditor = true;

    void Awake()
    {
        if (mobileCanvas == null)
        {
            Debug.LogWarning("MobileControlAutoHider: Chưa gán Mobile Canvas!");
            return;
        }
        bool isMobile = CheckIfMobile();
        mobileCanvas.SetActive(isMobile);
        if (Application.isEditor && forceShowInEditor)
        {
            mobileCanvas.SetActive(true);
        }
    }

    private bool CheckIfMobile()
    {
        if (Application.isMobilePlatform) return true;
        #if UNITY_WEBGL
        return IsMobileUserAgent();
        #endif

        return false;
    }

    private bool IsMobileUserAgent()
    {
        return Application.isMobilePlatform;
    }
}
