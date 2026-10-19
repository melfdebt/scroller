using Godot;
using System;

public partial class Spike : GroundObstacle
{
	 [Export]
    private float RetractedTime = 2.0f;

    [Export]
    private float ExtendedTime = 1.0f;

    [Export]
    private float MoveSpeed = 150f;

    [Export]
    private float RetractedY = 5f;

    [Export]
    private float ExtendedY = -5f;

    private Sprite2D _sprite;
    private CollisionShape2D _collisionShape;

    private bool _isExtended = false;
    private float _stateTimer = 0f;

    public override void _Ready()
    {
        base._Ready();

        _sprite = GetNode<Sprite2D>("Sprite2D");
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

        _sprite.Position = new Vector2(0, RetractedY);
        _collisionShape.Position = new Vector2(0, RetractedY);

        Monitoring = false;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        _stateTimer += (float)delta;

        float duration = _isExtended ? ExtendedTime : RetractedTime;

        if (_stateTimer >= duration)
        {
            _stateTimer = 0f;
            _isExtended = !_isExtended;
            Monitoring = _isExtended;
        }

        float targetY = _isExtended ? ExtendedY : RetractedY;

        Vector2 spritePosition = _sprite.Position;
        spritePosition.Y = Mathf.MoveToward(
            spritePosition.Y,
            targetY,
            MoveSpeed * (float)delta
        );
        _sprite.Position = spritePosition;

        Vector2 collisionPosition = _collisionShape.Position;
        collisionPosition.Y = spritePosition.Y;
        _collisionShape.Position = collisionPosition;
    }
}
