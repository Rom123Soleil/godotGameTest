using Godot;
using System;

public partial class Control : Godot.Control
{
    Label label;
    public override void _Ready()
    {
        label = GetNode<Label>("Label");
    }

    public override void _Process(double delta)
    {
        label.Text = $"fps {Engine.GetFramesPerSecond()}";
    }
}
