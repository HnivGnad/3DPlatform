using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Settings")]
    public string playerTag = "Player";
    public float delayBeforeLoad = 1.5f; // Tăng thêm chút thời gian để xem Particle

    [Header("Visual Effects")]
    public ParticleSystem winParticles;

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem có phải Player chạm vào vùng Trigger không
        if (other.CompareTag(playerTag) && !isTransitioning)
        {
            isTransitioning = true;

            // Khóa di chuyển của Player
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.MarkAsDead();
                player.SetVelocity(0, 0, 0);
            }

            // Chạy hiệu ứng hạt nếu có
            if (winParticles != null)
            {
                winParticles.Play();
            }

            // Phát âm thanh chiến thắng
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayWinSound();

            Debug.Log("<color=cyan>Chiến thắng! Đã khóa di chuyển và đang chuyển màn...</color>");
            Invoke("LoadNextLevel", delayBeforeLoad);
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Nếu còn màn tiếp theo thì tải, nếu không thì quay về Lobby UI
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (SceneFader.Instance != null)
                SceneFader.Instance.FadeTo(nextSceneIndex);
            else
                SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("<color=green>Đã hết màn chơi! Quay về Lobby UI.</color>");
            
            if (SceneFader.Instance != null)
                SceneFader.Instance.FadeTo("Lobby UI");
            else
                SceneManager.LoadScene("Lobby UI");
        }
    }
}
