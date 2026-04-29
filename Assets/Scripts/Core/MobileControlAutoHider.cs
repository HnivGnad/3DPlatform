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

        // Kiểm tra nền tảng để ẩn/hiện
        bool isMobile = CheckIfMobile();

        mobileCanvas.SetActive(isMobile);

        // Nếu đang ở trong Editor (để test)
        if (Application.isEditor && forceShowInEditor)
        {
            mobileCanvas.SetActive(true);
        }
    }

    private bool CheckIfMobile()
    {
        // 1. Kiểm tra nền tảng thực tế (Android/iOS)
        if (Application.isMobilePlatform) return true;

        // 2. Kiểm tra nếu đang chạy WebGL trên trình duyệt Mobile
        #if UNITY_WEBGL
        return IsMobileUserAgent();
        #endif

        return false;
    }

    private bool IsMobileUserAgent()
    {
        // Kiểm tra thêm một lần nữa cho chắc chắn trên WebGL
        return Application.isMobilePlatform;
    }
}
