using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDieState : EntityState
{
    private float deathTimer = 2f;

    public PlayerDieState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        deathTimer = 2f;
        
        player.MarkAsDead();
        
        float currentYVelocity = player.rb.linearVelocity.y;
        player.SetVelocity(0, currentYVelocity, 0);
    }

    public override void Update()
    {
        base.Update();

        deathTimer -= Time.deltaTime;

        if (deathTimer <= 0)
        {
            RestartLevel();
            deathTimer = 999f; 
        }
    }

    private void RestartLevel()
    {
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
