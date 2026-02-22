using Godot;
using System;

public partial class CarHandler : Node
{
	public void SeatPlayerAtCarEvent(Player player, Car car)
	{
		if (car.current_driver != null) return;
		player.hasControl = false;
        player.collision.Disabled = true;
        player.isPlayerInCar = true;

        car.current_driver = player;
		car.canMove = false;
		car.GetOutCollision.Disabled = false;
		
		Vector3 MovePlayerTo = car.DriverSeatPosition.GlobalPosition;
		Vector3 RotatePlayerTo = car.DriverSeatPosition.GlobalRotation;
		player.Reparent(car);
		
		Tween tween = car.CreateTween();
		tween.SetParallel(true);
		tween.TweenProperty(player, "global_position", MovePlayerTo, 0.5f);
		tween.TweenProperty(player, "global_rotation", RotatePlayerTo, 0.5f);
		tween.TweenCallback(Callable.From(() => { car.canMove = true; }));
	}
	
	public void GetOutOfTheCarEvent(Player player, Car car)
	{
        if (car.current_driver == null) return;

        car.current_driver = null;
        car.canMove = false;
        car.GetOutCollision.Disabled = true;

        Vector3 MovePlayerTo = car.DriverGetOutPosition.GlobalPosition;
		Vector3 RotatePlayerTo = Vector3.Zero;
		RotatePlayerTo.Y = car.DriverGetOutPosition.GlobalRotation.Y;
        player.Reparent(GetTree().CurrentScene);

        Tween tween = car.CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(player, "global_position", MovePlayerTo, 0.5f);
        tween.TweenProperty(player, "global_rotation", RotatePlayerTo, 0.5f);
		tween.TweenCallback(Callable.From(() =>
		{
			player.hasControl = true;
			player.collision.Disabled = false;
			player.isPlayerInCar = false;
			Vector3 plrRot = player.GlobalRotation;
			plrRot.Y = player.camera.GlobalRotation.Y;

			player.GlobalRotation = plrRot;
			player.camera.GlobalRotation = plrRot;
		})).SetDelay(1);
    }
}
