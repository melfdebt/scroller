using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	private float Damage  = 10f;
	private bool friendly {get;set;}=true;
	private Vector2 _direction = Vector2.Left;
	private float _speed = 500f;

	public void SetFriendly(bool value)
	{
    	friendly = value;
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

    public override void _Ready()
    {
        Area2D hitbox = GetNode<Area2D>("Area2D");
        hitbox.BodyEntered += OnBodyEntered;
    }


	private void _On_Collision(Area2D area)
	{
		GD.Print("bullet collided with: "+area.Name);
		if (area is Enemy && friendly)
		{
			Enemy enemy = (Enemy)area;
			enemy.TakeDamage(Damage);
			GD.Print($"{enemy} damaged");
			QueueFree();
		}
		else{
			QueueFree();
		}
	}

	private void _On_Screen_Leave()
	{
		GD.Print("out of level");
		QueueFree();
	}

	public void SetDamage(float damage)
	{
		Damage=damage;
	}

	private void OnBodyEntered(Node2D body)
    {
        if (!friendly && body is Player player)
        {
            player.TakeDamage(Damage);
			QueueFree();
			
        }
		GD.Print($"bullet entered on {body}");
    }
}
