using UnityEngine;

public class MobileControlAutoHider : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Kéo Canvas chứa Joystick và Nút nhảy vào đây")]
    public GameObject mobileCanvas;

    [Header("Settings")]
    public bool forceShowInEditor = true;

    void Start()
    {
        if (mobileCanvas == null) return;

        bool isMobile = CheckIfMobile();

        // Ưu tiên: Nếu ở trong Editor và bật ForceShow thì luôn hiện
        if (Application.isEditor && forceShowInEditor)
        {
            mobileCanvas.SetActive(true);
        }
        else
        {
            mobileCanvas.SetActive(isMobile);
        }
    }

    private bool CheckIfMobile()
    {
        if (Application.isMobilePlatform) return true;

        #if UNITY_WEBGL
        return IsMobileUserAgent();
        #else
        return false;
        #endif
    }

    private bool IsMobileUserAgent()
    {
        // Trên WebGL, SystemInfo.operatingSystem thường chứa tên hệ điều hành của trình duyệt
        string os = SystemInfo.operatingSystem.ToLower();
        return os.Contains("iphone") || os.Contains("android") || os.Contains("ipad") || os.Contains("ipod");
    }
}
