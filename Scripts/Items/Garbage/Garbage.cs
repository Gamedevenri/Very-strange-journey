using Godot;
using System;

public partial class Garbage : Item
{
    [Export] AnimationPlayer anims;
    public override void Interact()
    {
        anims.Play("on_ready");
    }
}
