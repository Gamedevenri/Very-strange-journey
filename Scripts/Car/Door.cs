using Godot;
using System;

public partial class Door : Node3D
{
	private bool is_open = false;
    private float open_angle = 90;
	private float open_speed = 1;

	[Export] public bool invert_rotation = false;
	[Export] Node3D pivot;

    public override void _Ready()
    {
		//yeah yeah, i am know about this bad idea.
		if (invert_rotation) open_angle = -open_angle;
    }

    public void interact()
	{
		Tween tween = this.CreateTween();
		if (is_open)
		{
			tween.TweenProperty(pivot, "rotation:y", Mathf.DegToRad(0), open_speed);
			is_open = false;
		} else
		{
            tween.TweenProperty(pivot, "rotation:y", Mathf.DegToRad(open_angle), open_speed);
			is_open = true;
        }
	}
}
