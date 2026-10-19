using Godot;
using System;

public abstract partial class GroundEnemy : Enemy
{
	[Export] protected float MoveSpeed = 10f;

	protected Player player;

    public override void _Ready()
    {
        base._Ready();
		player = GetTree().GetFirstNodeInGroup("player") as Player;
    }

	protected Vector2 GetDirectionToPlayer()
	{
    	if (player == null || !GodotObject.IsInstanceValid(player)){
			return Vector2.Zero;
		}

    	return GlobalPosition.DirectionTo(player.GlobalPosition);
	}

	protected void MoveTowardsPlayer(float delta)
	{
    	Vector2 direction = GetDirectionToPlayer();
    	Position += direction * MoveSpeed * delta;
	}

}
