using Godot;
using System;

public abstract partial class DamagingObject : MovingObject
{
	[Export]
	protected float ContactDmg = 10f;


	public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            DealDamage(player);
        }
    }
	protected virtual void DealDamage(Player player)
    {
        
        player.TakeDamage(ContactDmg);
        
    }
}
