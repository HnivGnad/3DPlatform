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
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.MarkAsDead();
                player.SetVelocity(0, 0, 0);
            }
            if (winParticles != null)
            {
                winParticles.Play();
            }
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayWinSound();

            Invoke("LoadNextLevel", delayBeforeLoad);
        }
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
            if (scenePath.Contains("Retry"))
            {
                ReturnToLobby();
            }
            else
            {
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
        
        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeTo("Lobby UI");
        else
            SceneManager.LoadScene("Lobby UI");
    }
}
