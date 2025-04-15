using Godot;
using System;

public partial class Ladder : Area3D
{

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Player player)
        {
            player.Climb();
        }
    }

    private void OnBodyExited(Node3D body)
    {
        if (body is Player player)
        {
            player.StopClimbing();
        }
    }

}
