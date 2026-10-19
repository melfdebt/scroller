using Godot;
using System;


public abstract partial class Enemy : DamagingObject
{

	[Export] 
	protected float MaxHealth = 100f;

	protected float currentHealth;
	
	public override void _Ready()
	{
		base._Ready();
		currentHealth=MaxHealth;
	}

	public void TakeDamage(float dmg)
	{
		currentHealth-=dmg;
		GD.Print($"{Name} health {currentHealth}");
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		GD.Print($"{Name} died");
		QueueFree();
	}
}
