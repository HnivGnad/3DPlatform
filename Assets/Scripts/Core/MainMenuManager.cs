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
        if (_playButton != null) _playButton.clicked += ShowLevelSelect;

        // Bind Level Select Events
        if (_backButton != null) _backButton.clicked += ShowHome;

        GenerateLevelGrid(20);
    }

    private void ShowLevelSelect()
    {
        _homeScreen.style.display = DisplayStyle.None;
        _levelSelectScreen.style.display = DisplayStyle.Flex;
        
        // Có thể ẩn nhân vật 3D ở đây nếu cần
    }

    private void ShowHome()
    {
        _homeScreen.style.display = DisplayStyle.Flex;
        _levelSelectScreen.style.display = DisplayStyle.None;
        
        // Hiện lại nhân vật 3D
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
            
            // Sử dụng các class đã định nghĩa trong USS
            levelBtn.AddToClassList("level-button");
            
            // Đánh dấu Level 1 là active (màu cam)
            if (i == 1)
            {
                levelBtn.AddToClassList("level-button-active");
            }

            int levelIndex = i;
            levelBtn.clicked += () => LoadLevel(levelIndex);
            
            _levelGrid.Add(levelBtn);
        }
    }

    private void LoadLevel(int index)
    {
        string sceneName = "Level" + index;
        Debug.Log($"Đang tải màn chơi: {sceneName}");
        
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning($"Màn chơi {sceneName} chưa được tạo hoặc chưa thêm vào Build Settings!");
            
            // Nếu chưa có các màn khác, tạm thời cho load Level1 để test
            if (index > 1) SceneManager.LoadScene("Level1");
        }
    }
}
