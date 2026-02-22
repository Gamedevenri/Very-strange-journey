using Godot;
using Godot.Collections;

public partial class TriggersHandler : Node
{
	[Export] Array<Area3D> Triggers;
	[Export] Array<PackedScene> Events;

	public override void _Ready()
	{
		foreach (Area3D trigger in Triggers)
		{
			trigger.BodyEntered += (Node3D body) => OnTriggered(trigger, body);
		}
	}

	private async void OnTriggered(Area3D trigger, Node3D body)
	{
		if(body.IsInGroup("player"))
		{
			GD.Print("sdsadsa");
			int eventId = GD.RandRange(0, Events.Count - 1);
			PackedScene EventScene = Events[eventId];
			Node3D CurrentEvent = EventScene.Instantiate() as Node3D;
			AddChild(CurrentEvent);
            CurrentEvent.GlobalPosition = trigger.GlobalPosition;
			CurrentEvent.GlobalRotation = trigger.GlobalRotation;

			Events.Remove(EventScene);

            trigger.QueueFree();
			await ToSignal(GetTree().CreateTimer(10), "timeout");
			CurrentEvent.QueueFree();		
		}
	}
}
