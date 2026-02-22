using Godot;
using System;

public partial class Car : VehicleBody3D
{
	public Player current_driver;
    public int engineForce = 1000;
    public float steeringSpeed = 0.4f;
    public bool canMove = false;

    [Export] public Marker3D DriverSeatPosition;
    [Export] public Marker3D DriverGetOutPosition;
    [Export] public CollisionShape3D GetOutCollision;
    public override void _PhysicsProcess(double delta)
    {
        this.EngineForce = 0;
        if (current_driver != null)
        {
            if (!canMove) return;
            this.Steering = Mathf.Lerp(Steering, -current_driver.inputDir.X * steeringSpeed, 5 * (float)delta);
            this.EngineForce = -current_driver.inputDir.Y * engineForce;
        }
    }
}
