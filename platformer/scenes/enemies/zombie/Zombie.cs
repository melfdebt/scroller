using Godot;
using System;

public partial class Zombie : GroundEnemy
{
	[Export] private float DashSpeed = 450f;
    [Export] private float DashDuration = 0.4f;
    [Export] private float DashInterval = 2.0f;

    private float _dashTimer = 0f;
    private float _dashRemaining = 0f;
    private Vector2 _dashDirection = Vector2.Left;

    protected override void Move(float delta)
    {
        _dashTimer += delta;

        if (_dashRemaining <= 0f && _dashTimer >= DashInterval)
        {
            _dashTimer = 0f;
            _dashRemaining = DashDuration;

            if (player != null &&
                GodotObject.IsInstanceValid(player))
            {
                float directionX = Mathf.Sign(
                    player.GlobalPosition.X - GlobalPosition.X
                );

                _dashDirection = new Vector2(directionX, 0f);
            }
        }

        if (_dashRemaining > 0f)
        {
            float step = Mathf.Min(delta, _dashRemaining);

            GlobalPosition += _dashDirection * DashSpeed * step;
            _dashRemaining -= step;
        }
        else
        {
            GlobalPosition += Vector2.Left * MoveSpeed * delta;
        }
    }
}
