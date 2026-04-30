using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    [Header("UI Reference")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (fadeImage != null)
            {
                fadeImage.color = new Color(0, 0, 0, 1);
                // Đảm bảo Image nhận va chạm để chặn người chơi bấm linh tinh khi đang chuyển cảnh
                fadeImage.raycastTarget = true; 
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void FadeTo(int sceneIndex)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(sceneIndex));
    }

    private IEnumerator FadeOut(int index)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            if (fadeImage != null)
                fadeImage.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }
        
        SceneManager.LoadScene(index);
    }

    private IEnumerator FadeIn()
    {
        float timer = fadeDuration;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (fadeImage != null)
                fadeImage.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }
        
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0);
            fadeImage.raycastTarget = false; // Tắt chặn bấm khi đã sáng lại
        }
    }
}
