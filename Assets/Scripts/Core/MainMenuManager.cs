using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    private UIDocument _uiDocument;
    
    // Screens
    private VisualElement _homeScreen;
    private VisualElement _levelSelectScreen;
    
    // Home Buttons
    private Button _playButton;
    
    // Level Select
    private Button _backButton;
    private VisualElement _levelGrid;

    private void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (_uiDocument == null) return;

        var root = _uiDocument.rootVisualElement;

        // Query Elements
        _homeScreen = root.Q<VisualElement>("HomeScreen");
        _levelSelectScreen = root.Q<VisualElement>("LevelSelectScreen");
        
        _playButton = root.Q<Button>("PlayButton");
        _backButton = root.Q<Button>("BackButton");
        _levelGrid = root.Q<VisualElement>("LevelGrid");

        // Bind Home Events
        if (_playButton != null) _playButton.clicked += () => LoadLevel(1);

        // Bind Level Select Events
        if (_backButton != null) _backButton.clicked += ShowHome;

        GenerateLevelGrid(20);
    }

    private void ShowLevelSelect()
    {
        _homeScreen.style.display = DisplayStyle.None;
        _levelSelectScreen.style.display = DisplayStyle.Flex;
    }

    private void ShowHome()
    {
        _homeScreen.style.display = DisplayStyle.Flex;
        _levelSelectScreen.style.display = DisplayStyle.None;
    }

    private void GenerateLevelGrid(int count)
    {
        if (_levelGrid == null) return;
        _levelGrid.Clear();

        for (int i = 1; i <= count; i++)
        {
            Button levelBtn = new Button();
            levelBtn.text = i.ToString();
            levelBtn.name = $"LevelBtn_{i}";
            levelBtn.AddToClassList("level-button");
            
            if (i == 1) levelBtn.AddToClassList("level-button-active");

            int levelIndex = i;
            levelBtn.clicked += () => LoadLevel(levelIndex);
            
            _levelGrid.Add(levelBtn);
        }
    }

    private void LoadLevel(int index)
    {
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeTo(index);
        }
        else
        {
            SceneManager.LoadScene("Level" + index);
        }
    }

    public void StartGame()
    {
        LoadLevel(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RetryLevel()
    {
        int lastLevel = PlayerPrefs.GetInt("LastLevelIndex", 1);
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeTo(lastLevel);
        }
        else
        {
            SceneManager.LoadScene(lastLevel);
        }
    }
    public void GoHome()
    {
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeTo("Lobby UI");
        }
        else
        {
            SceneManager.LoadScene("Lobby UI");
        }
    }
}
