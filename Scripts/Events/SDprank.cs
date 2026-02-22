using Godot;
using System;

public partial class SDprank : Node3D
{
    [Export] Label label;
    [Export] AnimationPlayer anims;
    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("S") && Input.IsActionPressed("D"))
        {
            label.Text = "HAHAHAHAHA";
            HideAnimation();
        }
    }

    private async void HideAnimation()
    {
        await ToSignal(GetTree().CreateTimer(2), "timeout");
        anims.Play("hide");
    }
}
