using Godot;
using System;

public partial class Main : Node2D
{

	[Export]
    private Godot.Collections.Array<PackedScene> PlatformScenes = new Godot.Collections.Array<PackedScene>();

    [Export]
    private Godot.Collections.Array<PackedScene> EnemyScenes = new Godot.Collections.Array<PackedScene>();

	[Export]
	private Godot.Collections.Array<PackedScene> ObstacleScenes = new Godot.Collections.Array<PackedScene>();
	
	[Export] private float PlatformSpawnInterval = 2.0f;
    [Export] private float EnemySpawnInterval = 3.0f;
    [Export] private float ObstacleSpawnInterval = 2.5f;

    private float _platformTimer;
    private float _enemyTimer;
    private float _obstacleTimer;


    public override void _Process(double delta)
    {
        _platformTimer += (float)delta;
        _enemyTimer += (float)delta;
        _obstacleTimer += (float)delta;

        if (_platformTimer >= PlatformSpawnInterval)
        {
            _platformTimer = 0f;
            SpawnPlatform();
        }

        if (_enemyTimer >= EnemySpawnInterval)
        {
            _enemyTimer = 0f;
            SpawnEnemy();
        }

        if (_obstacleTimer >= ObstacleSpawnInterval)
        {
            _obstacleTimer = 0f;
            SpawnObstacle();
        }
    }

    private void SpawnPlatform()
    {
        if (PlatformScenes.Count == 0)
            return;

        int index = GD.RandRange(0, PlatformScenes.Count - 1);
        MovingObject platform = PlatformScenes[index].Instantiate<MovingObject>();

        AddChild(platform);
        platform.Position = platform.GetSpawnPosition();
    }

    private void SpawnEnemy()
    {
        if (EnemyScenes.Count == 0)
            return;

        int index = GD.RandRange(0, EnemyScenes.Count - 1);
        Enemy enemy = EnemyScenes[index].Instantiate<Enemy>();

        AddChild(enemy);
        enemy.Position = enemy.GetSpawnPosition();
    }

    private void SpawnObstacle()
    {
        if (ObstacleScenes.Count == 0)
            return;

        int index = GD.RandRange(0, ObstacleScenes.Count - 1);
        DamagingObject obstacle = ObstacleScenes[index].Instantiate<DamagingObject>();

        AddChild(obstacle);
        obstacle.Position = obstacle.GetSpawnPosition();
    }
}
