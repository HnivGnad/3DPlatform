using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDieState : EntityState
{
    private float deathTimer = 2f; // Đợi 2 giây trước khi restart

    public PlayerDieState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        // Luôn reset lại timer mỗi khi chết
        deathTimer = 2f;
        
        player.MarkAsDead();
        
        // Giữ lại vận tốc rơi hiện tại, chỉ triệt tiêu di chuyển ngang
        float currentYVelocity = player.rb.linearVelocity.y;
        player.SetVelocity(0, currentYVelocity, 0);

        Debug.Log("<color=red>Player đã chết! Đang chờ 2 giây để chuyển sang màn hình Retry...</color>");
    }

    public override void Update()
    {
        base.Update();

        deathTimer -= Time.deltaTime;

        if (deathTimer <= 0)
        {
            RestartLevel();
            // Đặt timer về một số dương lớn để không gọi RestartLevel liên tục
            deathTimer = 999f; 
        }
    }

    private void RestartLevel()
    {
        Debug.Log("<color=orange>Đang lưu màn chơi hiện tại và chuyển sang Scene: Retry</color>");
        
        // Ghi nhớ màn chơi vừa thua
        PlayerPrefs.SetInt("LastLevelIndex", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();

        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeTo("Retry");
        }
        else
        {
            SceneManager.LoadScene("Retry");
        }
    }
}
