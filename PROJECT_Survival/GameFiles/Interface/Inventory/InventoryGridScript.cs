using Godot;
using System;

public partial class InventoryGridScript : GridContainer
{
	[Export]
	public int width { get; set; } = 3;
	[Export]
	public int height { get; set; } = 3;

    /// <summary>
    /// Keeps track of all the cells inside if they are empty or not (1 = filled; 0 = empty)
    /// </summary>
    public int[,] data { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Columns = width;
		for(int i = 0;i < height * width; i++)
		{
			AddChild((Control)GD.Load<PackedScene>("res://GameFiles/Interface/Inventory/Cell.tscn").Instantiate());
		}
		this.CustomMinimumSize = this.Size;

		data = new int[width,height];
		for(int i = 0; i < width; i++)
		{
			for(int j = 0; j < height; j++)
			{
				data[i, j] = 0;
			}
		}

	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		Rect2 gridPosition = new Rect2(GlobalPosition,Size);

		if (@event is InputEventMouseMotion mouse)
		{
			if (gridPosition.HasPoint(mouse.Position))
			{
				Vector2 GridPosition = new Vector2((int)((mouse.Position.X - GlobalPosition.X) / 40), (int)((mouse.Position.Y - GlobalPosition.Y) / 40));
				GD.Print(GridPosition);
			}
		}

	}
}
