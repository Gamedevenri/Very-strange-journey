using Godot;
using System;

public partial class Item : RigidBody3D, ItemInterface
{
	[Export] public string ItemName;
	[Export] public bool InteractWhenTaken = false;
	[Export] ItemText ItemText;

	private bool isTaken = false;

    public override void _Ready()
    {
		ItemText.ItemLabel.Text = GetItemName();
    }
    public virtual void Interact()
	{
	}

	public virtual void Taken()
	{
        Freeze = true;
		isTaken = true;
    }

	public virtual void Drop(Vector3 ThrowDir)
	{
		Freeze = false;
		isTaken = false;

		ApplyCentralImpulse(ThrowDir);
	}

	public virtual string GetItemName()
	{
		return ItemName;
	}

    public override void _PhysicsProcess(double delta)
    {
        Camera3D camera = GetViewport().GetCamera3D();
        if (!isTaken && this.GlobalPosition.DistanceTo(camera.GlobalPosition) < 2)
        {
            ItemText.Visible = true;
        }
        else
        {
            ItemText.Visible = false;
        }
    }
}
