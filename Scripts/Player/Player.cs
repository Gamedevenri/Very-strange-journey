using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public Vector2 inputDir = Vector2.Zero;
	public bool hasControl = true;

	public Item CurrentItem = null;
	public float ThrowForce = 2.0f;
	public bool isPlayerInCar = false;
	public bool allowRotation = true;

	[Export] RayCast3D InteractionRay;
	[Export] public Marker3D ItemPosition;
	[Export] public CollisionShape3D collision;
	[Export] public Camera3D camera;

    public override void _Ready()
    {
		Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact"))
		{
			if (InteractionRay.IsColliding())
			{
                Node3D collider = InteractionRay.GetCollider() as Node3D;
                if (collider.IsInGroup("item"))
				{
					Item item = collider as Item;
					if (CurrentItem == null)
					{
						CurrentItem = item;
						CurrentItem.Taken();
						
						if (CurrentItem.InteractWhenTaken) CurrentItem.Interact();
                    }
				} else if (collider.IsInGroup("interactable"))
				{
					collider.Call("interact");
				} else if (collider.IsInGroup("car"))
				{
					collider.Call("interact", this);
				}
			}
		}
		if (Input.IsActionJustPressed("drop"))
		{
			if (CurrentItem != null)
			{
				Vector3 throwDir = -GetViewport().GetCamera3D().GlobalTransform.Basis.Z * ThrowForce;
				CurrentItem.Drop(throwDir);
				CurrentItem = null;
			}
		}
        if (Input.IsActionJustPressed("CarStucked"))
        {
            Game gameNode = GetTree().CurrentScene as Game;
			gameNode.car.GlobalPosition = gameNode.player.GlobalPosition;
			gameNode.car.GlobalPosition += Vector3.Up * 3;
            gameNode.car.GlobalPosition += Vector3.Left * 3;
            gameNode.car.GlobalRotation = Vector3.Zero;
        }
    }

    public override void _PhysicsProcess(double delta)
	{
        inputDir = Input.GetVector("left", "right", "forward", "backward");
        if (!hasControl) return;

		Vector3 velocity = Velocity;


        // Add the gravity.
        if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		/* Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}*/


		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();

		if (CurrentItem != null)
		{
			CurrentItem.GlobalPosition = CurrentItem.GlobalPosition.Lerp(ItemPosition.GlobalPosition, 0.2f);
		}
	}
}
