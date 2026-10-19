using Godot;
using System;

public abstract partial class FlyingObstacle : Obsatcle
{    
	[Export]
    protected float FlySpeed = 200f;

    protected override void Move(float delta)
    {
        Position += new Vector2(-FlySpeed * delta, 0);
    }
}
