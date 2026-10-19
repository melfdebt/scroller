using Godot;
using System;

public partial class Saw : FlyingObstacle
{
	[Export] private float Amplitude = 50f;
    [Export] private float Frequency = 2f;

    private float _time;
    private float _initialY;

    public override void _Ready()
    {
        base._Ready();
        _initialY = 450f;
        GD.Print($"saw spawned {GlobalPosition}");
    }

    protected override void Move(float delta)
    {
        _time += delta;

        float x = GlobalPosition.X - FlySpeed * delta;
        float y = _initialY + Amplitude * Mathf.Sin(_time * Frequency);

        GlobalPosition = new Vector2(x, y);
    }
}
