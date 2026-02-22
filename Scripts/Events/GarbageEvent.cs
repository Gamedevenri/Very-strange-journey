using Godot;
public partial class GarbageEvent : Node3D
{
    [Export] AnimationPlayer anims;
    [Export] PackedScene GargageScene;
    Player player;
    Car car;

    bool GarbageSpawned = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Game gameNode = GetTree().CurrentScene as Game;
        player = gameNode.player;
        car = gameNode.car;
        OnReady();
    }

    private async void OnReady()
    {
        CarHandler carHandler = GetTree().Root.GetNodeOrNull("CarHandler") as CarHandler;
        car.Freeze = true;
        anims.Play("on_ready");
        await ToSignal(GetTree().CreateTimer(2), "timeout");
        carHandler.GetOutOfTheCarEvent(player, car);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!player.isPlayerInCar)
        {
            if (GarbageSpawned) return;
            Node3D garbage = GargageScene.Instantiate() as Node3D;
            GetTree().CurrentScene.AddChild(garbage);
            Vector3 forward = -player.GlobalTransform.Basis.Z.Normalized();
            Vector3 garbagePosition = player.GlobalPosition + forward * 2;
            garbage.GlobalPosition = garbagePosition;
            GarbageSpawned = true;
            car.Freeze = false;
        }
    }
}
