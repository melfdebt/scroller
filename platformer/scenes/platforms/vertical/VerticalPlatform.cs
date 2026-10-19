using Godot;
using System;

public partial class VerticalPlatform : Platform
{
	[Export]
    private float SpawnX = 1300f;

    [Export]
    private float FloorY = 390f;

    public override Vector2 GetSpawnPosition()
    {
        return new Vector2(SpawnX, FloorY);
    }
}
