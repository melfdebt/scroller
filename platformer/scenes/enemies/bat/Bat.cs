using Godot;
using System;


[GlobalClass]
public partial class Bat : FlyingEnemy
{
	
	[Export] private float Amplitude = 60f;
    [Export] private float Frequency = 2f;

    private float _time = 0f;
    private float _startY;

    public override void _Ready()
    {
        base._Ready();
        _startY = 400f;
    }

    protected override void Move(float delta)
    {
        _time += delta;

        float x = GlobalPosition.X - FlySpeed * delta;
        float y = _startY + Amplitude * Mathf.Sin(_time * Frequency);

        GlobalPosition = new Vector2(x, y);
    }
}
