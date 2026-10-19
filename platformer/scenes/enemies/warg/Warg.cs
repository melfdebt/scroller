using Godot;
using System;

public partial class Warg : GroundEnemy
{
	protected override void Move(float delta)
    {
		if (player == null || !GodotObject.IsInstanceValid(player))
        {
            Position += Vector2.Left * MoveSpeed * delta;
            return;
        }

        float directionX = Mathf.Sign(
            player.GlobalPosition.X - GlobalPosition.X
        );

        GlobalPosition += new Vector2(directionX, 0f) * MoveSpeed * delta;
    }
}
