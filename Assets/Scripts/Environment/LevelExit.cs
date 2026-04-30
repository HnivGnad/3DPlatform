using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Settings")]
    public string playerTag = "Player";
    public float delayBeforeLoad = 1.5f;

    [Header("Visual Effects")]
    public ParticleSystem winParticles;

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
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

            Debug.Log("<color=cyan>Chiến thắng! Đang chuẩn bị chuyển màn...</color>");
            Invoke("LoadNextLevel", delayBeforeLoad);
        }
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Kiểm tra xem còn màn tiếp theo trong Build Settings không
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Lấy đường dẫn của scene tiếp theo
            string scenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
            
            // Nếu scene tiếp theo là màn "Retry", coi như đã hết các Level chơi
            if (scenePath.Contains("Retry"))
            {
                ReturnToLobby();
            }
            else
            {
                // Nạp màn tiếp theo bình thường
                if (SceneFader.Instance != null)
                    SceneFader.Instance.FadeTo(nextSceneIndex);
                else
                    SceneManager.LoadScene(nextSceneIndex);
            }
        }
        else
        {
            ReturnToLobby();
        }
    }

    private void ReturnToLobby()
    {
        Debug.Log("<color=green>Hoàn thành màn chơi cuối! Quay về Lobby UI.</color>");
        
        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeTo("Lobby UI");
        else
            SceneManager.LoadScene("Lobby UI");
    }
}
