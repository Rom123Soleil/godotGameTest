using Godot;
using System;

public partial class Quit : Button
{
    public override void _Pressed()
    {
        GetTree().Quit();
    }
}
