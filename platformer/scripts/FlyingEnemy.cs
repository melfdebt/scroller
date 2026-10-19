using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public abstract partial class FlyingEnemy : Enemy
{
	[Export] public float FlySpeed=200f;

	public override Vector2 GetSpawnPosition(){
		return new Vector2(1300f,400f);
	}
}
