using Godot;

public partial class Start : Button
{
    [Export] private PackedScene NextScene;

    public override void _Pressed()
    {
        GetTree().ChangeSceneToPacked(NextScene);
    }
}
