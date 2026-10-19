using Godot;
using System;

public partial class FloatingPlatform : Platform
{

	[Export]
    private float SpawnX = 1300f;

    [Export]
    private float MinY = 250f;

    [Export]
    private float MaxY = 400f;
    public override Vector2 GetSpawnPosition()
    {
        float spawnY = (float)GD.RandRange(MinY, MaxY);
        return new Vector2(SpawnX, spawnY);
    }

}
