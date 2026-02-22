using Godot;
using System;

public partial class ItemText : Node3D
{
	[Export] public Label3D ItemLabel;
    RigidBody3D parent;

    public override void _Ready()
    {
        this.TopLevel = true;
        parent = GetParent() as RigidBody3D;    
    }

    public override void _PhysicsProcess(double delta)
    {
        this.GlobalPosition = parent.GlobalPosition + new Vector3(0,0.5f,0);
    }
}
