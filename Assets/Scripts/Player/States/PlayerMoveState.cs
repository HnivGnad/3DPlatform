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
            // Di chuyển vị trí
            player.transform.position += movement * player.moveSpeed * Time.deltaTime;
            
            // Xoay nhân vật hướng về phía di chuyển (Dùng Slerp để mượt mà)
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * player.rotationSpeed);
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
