using Godot;
public partial class Enemy : Area3D
{
    [Export] private float EnemySpeed;
    [Export] private Vector3 MovementDirection;
    private Vector3 basePosition;
    private Vector3 currentPosition;
    private Vector3 targetPosition;

    public override void _Ready()
    {
        basePosition = Position;
        currentPosition = Position;
        targetPosition = basePosition + MovementDirection;
    }

    public override void _Process(double delta)
    {
        currentPosition = Position;
        currentPosition = currentPosition.MoveToward(targetPosition, (float)(EnemySpeed * delta));

        if (currentPosition == targetPosition)
        {
            if (currentPosition == basePosition)
            {
                targetPosition = basePosition + MovementDirection;
            }
            else
            {
                targetPosition = basePosition;
            }
        }

        Position = currentPosition;
    }
}
