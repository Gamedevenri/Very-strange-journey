using Godot;
using System;

public partial class GetOutTrigger : Area3D
{
    [Export] Car car;
    public void interact(Player player)
    {
        CarHandler carHandler = GetTree().Root.GetNodeOrNull("CarHandler") as CarHandler;
        carHandler.GetOutOfTheCarEvent(player, car);
    }
}
