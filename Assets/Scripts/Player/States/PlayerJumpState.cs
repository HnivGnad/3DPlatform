using UnityEngine;

public class PlayerJumpState : EntityState
{
    public PlayerJumpState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        // Thực hiện nhảy bằng cách thêm lực hướng lên
        player.rb.linearVelocity = new Vector3(player.rb.linearVelocity.x, 0, player.rb.linearVelocity.z);
        player.rb.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);
    }

    public override void Update()
    {
        base.Update();

        // Cho phép di chuyển trên không
        Vector3 movement = new Vector3(player.InputVector.x, 0, player.InputVector.y).normalized;

        if (movement != Vector3.zero)
        {
            player.transform.position += movement * player.moveSpeed * Time.deltaTime;

            // Xoay nhân vật trên không
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * player.rotationSpeed);
        }

        // Nếu vận tốc Y < 0 (đang rơi) và chạm đất thì quay lại Idle hoặc Move
        if (player.rb.linearVelocity.y < 0 && player.IsGrounded())
        {
            if (player.InputVector != Vector2.zero)
                stateMachine.ChangeState(player.MoveState);
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }


    public override void Exit()
    {
        base.Exit();
    }
}
