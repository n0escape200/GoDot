using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;
using System.Linq;

public partial class PlayerScript : CharacterBody3D
{

	Vector3 direction = Vector3.Zero;
	Vector3 _velocity = Vector3.Zero;

	[Export] 
	float speed = 5.0f;
	[Export]
	float run = 8f;
	[Export]
	float health = 100.0f;
	[Export]
	float stamina = 100;

	bool isMoving = false;
	bool isInventoryOn = false;

	Control HUD;
	Camera3D camera;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
	public override void _Ready()
	{
		base._Ready();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		camera = GetNodeOrNull<Camera3D>("PlayerCamera");

		HUD = GetNode<Control>("HUD");

		if (HUD != null)
		{
			HUD.GetNode<ProgressBar>("HealthLabel/HealthBar").Value = health;
			HUD.GetNode<ProgressBar>("StaminaLabel/StaminaBar").Value = stamina;
		}
	}

	float _time = 1;
	
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (!isMoving)
		{
			_time += (1 * (float)delta);
			//GD.Print(_time);
			if (_time >= 3)
			{
				stamina = Math.Clamp(stamina + 10 * (float)delta, 0, 100);
			}
			HUD.GetNode<ProgressBar>("StaminaLabel/StaminaBar").Value = stamina;
		}

		if (Input.IsActionJustPressed("Inventory"))
		{
			isInventoryOn = !isInventoryOn;
		}

		if (isInventoryOn)
		{
            HUD.GetNode<Control>("InventoryScene").ProcessMode = ProcessModeEnum.Always;
            HUD.GetNode<Control>("InventoryScene").Show();
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else
		{
            HUD.GetNode<Control>("InventoryScene").ProcessMode = ProcessModeEnum.Disabled;
            HUD.GetNode<Control>("InventoryScene").Hide();
            Input.MouseMode = Input.MouseModeEnum.Captured;
		}
    }

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseMotion mouse)
		{
			if (!isInventoryOn)
			{
				//Rotating the Player leaft and right
				Rotation = new Vector3(0, Rotation.Y - mouse.Relative.X / 1000, 0);

				//Rotating the camera up and down and clapimng the value 
				camera.Rotation = new Vector3(camera.Rotation.X - mouse.Relative.Y / 1000, 0, 0);
				camera.Rotation = camera.Rotation.Clamp(new Vector3(-0.8f, 0, 0), new Vector3(0.5f, 0, 0));
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		//Getting the key inputs
		Vector2 input = Input.GetVector("Left", "Right", "Forward", "Backward");
		Vector3 direction = (Transform.Basis * new Vector3(input.X,0,input.Y)).Normalized();


		//Cheking if the player is grounded, otherwise will be able to move and jump 
		if (!IsOnFloor())
		{
			_velocity.Y -= 9.8f * (float)delta;
		}
		else
		{
			if (direction != Vector3.Zero && !isInventoryOn)
			{
				isMoving = true;
				_time = 0;
				if (Input.IsActionPressed("Run") && stamina > 0)
				{
					_velocity.X = direction.X * run;
					_velocity.Z = direction.Z * run;

					stamina = Math.Clamp(stamina - 10 * (float)delta,0,100);
					HUD.GetNode<ProgressBar>("StaminaLabel/StaminaBar").Value = stamina;
					
				}
				else
				{
					_velocity.X = direction.X * speed;
					_velocity.Z = direction.Z * speed;
				}
			}
			else
			{
				_velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
				_velocity.Z = Mathf.MoveToward(Velocity.X, 0, speed);
				isMoving = false;
			}
			

			if (Input.IsActionPressed("Jump"))
			{
				_velocity.Y = 6;
			}
		}
		
		Velocity = _velocity;
		MoveAndSlide();
	}
}
