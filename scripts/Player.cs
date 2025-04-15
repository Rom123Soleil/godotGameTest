using Godot;

public partial class Player : CharacterBody3D
{

	[Export] private float playerSpeed = 20.0f;
	[Export] private float jump = 10.0f;
	[Export] private float gravity = 10.0f;

	[Export] private float climbingSpeed = 5.0f;

	private Camera3D camera;
	private Node3D anchorCamera;

	private bool isClimbing;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anchorCamera = GetNode<Node3D>("AnchorCamera");
		camera = anchorCamera.GetNode<Camera3D>("Camera3D");

		Input.MouseMode = Input.MouseModeEnum.Captured;
		isClimbing = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
		Vector3 position = Position;

		if (position.Y <= -10)
		{
			position = PutBackToMainFloor();
		}

		if (!IsOnFloor() && !isClimbing)
		{
			velocity.Y -= (float)(gravity * delta);
		}

		if (IsOnFloor() && Input.IsActionPressed("jump"))
		{
			velocity.Y = jump;
		}

		if (isClimbing)
		{

			if (velocity.Y <= 0)
			{
				velocity.Y = 0;
			}

			velocity.Y += (float)(climbingSpeed * delta);
		}


		Vector2 inputDirection = Input.GetVector("left", "right", "forward", "backward");
		Vector3 direction = new Vector3(inputDirection.X, 0, inputDirection.Y).Normalized();

		direction = this.Basis * direction;
		direction.Y = 0;
		direction = direction.Normalized();
		velocity.X = direction.X * playerSpeed;
		velocity.Z = direction.Z * playerSpeed;


		Position = position;
		Velocity = velocity;
		MoveAndSlide();
	}

	private static Vector3 PutBackToMainFloor()
	{
		Vector3 position;
		position.X = 0;
		position.Y = 2;
		position.Z = 0;
		return position;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode.Equals(Input.MouseModeEnum.Captured))
		{
			// Block camera to at 90°
			if ((anchorCamera.Rotation.X > -0.90 && mouseMotion.Relative.Y > 0)
			|| (anchorCamera.Rotation.X < 0.90 && mouseMotion.Relative.Y < 0))
			{
				anchorCamera.RotateX(-mouseMotion.Relative.Y * 0.002f);
			}

			RotateY(-mouseMotion.Relative.X * 0.002f);
		}
		else if (@event is InputEventKey keyEvent)
		{
			if (keyEvent.IsActionPressed("echap"))
			{
				Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
			}
		}

	}

	public void Climb()
	{
		isClimbing = true;
	}

	public void StopClimbing()
	{
		isClimbing = false;
	}

}
