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
        
        player.MarkAsDead();
        
        // Giữ lại vận tốc rơi hiện tại, chỉ triệt tiêu di chuyển ngang
        float currentYVelocity = player.rb.linearVelocity.y;
        player.SetVelocity(0, currentYVelocity, 0);

        Debug.Log("<color=red>Player đã chết và đang rơi...</color>");
    }

    public override void Update()
    {
        base.Update();

        deathTimer -= Time.deltaTime;

        if (deathTimer <= 0)
        {
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
