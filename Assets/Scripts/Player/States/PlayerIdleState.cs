using UnityEngine;

public class PlayerIdleState : EntityState
{
    public PlayerIdleState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, player.rb.linearVelocity.y, 0);
    }

    public override void Update()
    {
        base.Update();

        if (player.InputVector.x != 0 || player.InputVector.y != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }


    public override void Exit()
    {
        base.Exit();
    }
}
