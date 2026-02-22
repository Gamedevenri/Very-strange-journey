using Godot;
using System;

public partial class Car180Rotation : Node3D
{
	public override void _Ready()
	{
		Game gameNode = GetTree().CurrentScene as Game;
		if (gameNode != null)
		{
			Car car = gameNode.GetNodeOrNull("Car") as Car;
			car.LinearVelocity += new Vector3(0,7,0);
			car.AngularVelocity += new Vector3(0, 15, 0);
        }
	}
}
