using UnityEngine;

public class PlayerMoveState : EntityState
{
    public PlayerMoveState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        Vector3 movement = new Vector3(player.InputVector.x, 0, player.InputVector.y).normalized;
        
        if (movement != Vector3.zero)
        {
            // Di chuyển bằng velocity
            player.SetVelocity(movement.x * player.moveSpeed, player.rb.linearVelocity.y, movement.z * player.moveSpeed);
            
            // Xoay nhân vật hướng về phía di chuyển (Dùng Slerp để mượt mà)
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * player.rotationSpeed);
        }
        else
        {
            // Dừng di chuyển ngang khi không có input
            player.SetVelocity(0, player.rb.linearVelocity.y, 0);
        }

        // Chuyển về Idle nếu không có input
        if (player.InputVector == Vector2.zero)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }



    public override void Exit()
    {
        base.Exit();
    }
}
