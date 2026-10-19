using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float MoveSpeed { get; set; } = 300.0f;
	[Export] public float Gravity { get; set; } = 980.0f;
	[Export] public float JumpVelocity { get; set; } = -650.0f;
	[Export] public float JumpHoldGravityMultiplier { get; set; } = 0.35f;
	[Export] public float MaxJumpHoldTime { get; set; } = 0.35f;

    [Export] public float MaxHealth {get;set;} = 100f;

    private float currentHealth;

	private double _jumpHoldTimer;
    private bool _isJumpHeld;

    public override void _Ready()
    {
        currentHealth=MaxHealth;        
    }

	
    public override void _PhysicsProcess(double delta)
    {
    float dt = (float)delta;

    
    float direction = Input.GetAxis("left", "right");
    Velocity = new Vector2(direction * MoveSpeed, Velocity.Y);

    
    if (Input.IsActionJustPressed("jump") && IsOnFloor())
    {
        Velocity = new Vector2(Velocity.X, JumpVelocity);
        _jumpHoldTimer = 0.0f;
        _isJumpHeld = true;
    }

   
    if (!IsOnFloor())
    {
        float gravityMultiplier = 1.0f;

       
        if (Input.IsActionPressed("jump") &&
            _isJumpHeld &&
            _jumpHoldTimer < MaxJumpHoldTime &&
            Velocity.Y < 0.0f)
        {
            gravityMultiplier = JumpHoldGravityMultiplier;
            _jumpHoldTimer += dt;
        }

        Velocity = new Vector2(
            Velocity.X,
            Velocity.Y + Gravity * gravityMultiplier * dt
        );
    }

    
    if (Input.IsActionJustReleased("jump"))
    {
        _isJumpHeld = false;
    }

    MoveAndSlide();
}
    public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("shoot"))
		{
			Shoot();
		}
	}

	private PackedScene BulletScene = GD.Load<PackedScene>("res://scenes/bullets/bullet.tscn");
	[Export]
	private float BulletSpeed=500f;
	[Export]
	private float BulletDamage =10f;

	private void Shoot()//реализация механики стрельбы 
	{
		Bullet bullet  = (Bullet)BulletScene.Instantiate();
		AddSibling(bullet);
		bullet.Position = Position+new Vector2(50,0);
		bullet.Velocity = new Vector2(BulletSpeed,0);
		bullet.SetDamage(BulletDamage);
	}

    public void TakeDamage(float damage)
    {
        currentHealth-=damage;
        GD.Print($"player was damaged {currentHealth}");
        if (currentHealth <= 0)
        {
            GD.Print("player died");
            QueueFree();
            GetTree().Quit();
        }
    }

    private void _On_Screen_Leave()
	{
		GD.Print("out of level");
		QueueFree();
        GetTree().Quit();
	}

}
