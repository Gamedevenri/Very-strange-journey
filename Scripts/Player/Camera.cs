using Godot;

public partial class Camera : Camera3D
{
    private float mouse_sens = 0.002f;
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion input)
        {
            Player Parent = (Player)GetParent();
            if (!Parent.allowRotation) return;

            Vector3 RotateTo = new Vector3(0, input.Relative.X, 0) * mouse_sens;
            Node3D RotateToNode;

            if (!Parent.isPlayerInCar) RotateToNode = Parent;
            else RotateToNode = this;

            RotateToNode.GlobalRotation -= RotateTo;
            GlobalRotation -= new Vector3(input.Relative.Y, 0, 0) * mouse_sens;
            GlobalRotation = new Vector3(Mathf.Clamp(GlobalRotation.X, Mathf.DegToRad(-60), Mathf.DegToRad(60)), GlobalRotation.Y, GlobalRotation.Z);
        }
    }
}
