using Godot;
using System;

public partial class Dragon : FlyingEnemy
{
    [Export] private PackedScene BulletScene;
    [Export] private float ShootInterval = 2.0f;
    [Export] private float BulletSpeed = 400f;
    [Export] private float BulletDamage = 10f;

    private Player TargetPlayer;
    private float _shootTimer = 0f;

    public override void _Ready()
    {
        base._Ready();
        TargetPlayer = GetTree().GetFirstNodeInGroup("player") as Player;
    }

    protected override void Move(float delta)
    {
        Position += Vector2.Left * FlySpeed * delta;
        _shootTimer += delta;

        if (_shootTimer >= ShootInterval)
        {
            _shootTimer = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (BulletScene == null)
        {
            GD.PrintErr("Dragon: BulletScene не назначена!");
            return;
        }

        if (TargetPlayer == null || !GodotObject.IsInstanceValid(TargetPlayer))
        {
            TargetPlayer = GetTree().GetFirstNodeInGroup("player") as Player;
            return;
        }

        Bullet bullet = BulletScene.Instantiate<Bullet>();

        GetTree().CurrentScene.AddChild(bullet);
		Vector2 direction = (TargetPlayer.GlobalPosition - GlobalPosition).Normalized();

		bullet.GlobalPosition = GlobalPosition + direction * 40f;
		bullet.Velocity = direction * BulletSpeed;

		bullet.SetDamage(BulletDamage);
		bullet.SetFriendly(false);
    }
}
