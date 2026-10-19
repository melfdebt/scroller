using Godot;
using System;

public abstract partial class MovingObject : Area2D
{

	[Export]
	protected Vector2 velocity = new Vector2(-300f,0);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		VisibleOnScreenNotifier2D notifier = new VisibleOnScreenNotifier2D();
		AddChild(notifier);
		notifier.Connect(
			"screen_exited",
			new Callable(
				this,"_On_Screen_Exited"
			)
		);
		Setup();
	}
	protected virtual void Setup()
	{
		
	}

	private async void _On_Screen_Exited()//удаление сущности если она вышла за пределы поля
	{
		Timer timer = new Timer();
		timer.WaitTime =1f;
		timer.OneShot=true;
		AddChild(timer);
		timer.Start();
		await ToSignal(timer,"timeout");
		GD.Print($"{Name} has exited the scene");
		QueueFree();
	}

	public virtual Vector2 GetSpawnPosition()//позиция спавна 
	{
		return new Vector2(1300f,496f);
	}

	
	public override void _Process(double delta)
	{
		Move((float)delta);

	}
	protected virtual void Move(float delta)
	{
		Position+=velocity*delta;
	}


}
