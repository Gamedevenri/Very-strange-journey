using Godot;
using System;

public partial class TheEnd : Area3D
{

	public void _on_body_entered(Node3D body)
	{
		if (!body.IsInGroup("player")) return;
		GetTree().ChangeSceneToFile("res://Scenes/Menu/scenes/end_credits/end_credits.tscn");
	}
}
