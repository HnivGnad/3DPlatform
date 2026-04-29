using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Settings")]
    public string playerTag = "Player";
    public float delayBeforeLoad = 0.5f;

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem có phải Player chạm vào vùng Trigger không
        if (other.CompareTag(playerTag) && !isTransitioning)
        {
            isTransitioning = true;
            Debug.Log("<color=cyan>Đang chuyển sang màn tiếp theo...</color>");
            Invoke("LoadNextLevel", delayBeforeLoad);
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Nếu còn màn tiếp theo thì tải, nếu không thì quay về Menu
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("<color=green>Đã hết màn chơi! Quay về Main Menu.</color>");
            SceneManager.LoadScene(0); // Màn hình chính thường là Index 0
        }
    }
}
