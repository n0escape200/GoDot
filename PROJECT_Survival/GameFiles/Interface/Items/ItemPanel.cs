using Godot;
using System;

public partial class ItemPanel : ColorRect
{
    [Export]
    public int width { get; set; } = 1;

    [Export]
    public int height { get; set; } = 1;

    public bool isMoving = false;

    ItemPanel() { width =1; height =1; }
    ItemPanel(int _width, int _height) { width = _width; height = _height; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Vector2 newSize = new Vector2(this.Size.X * width, this.Size.Y * height);
        this.CustomMinimumSize = newSize;

        GetNode<Sprite2D>("Icon").Position = newSize / 2;   
	}


    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);
        if(@event is InputEventMouseButton mouse)
        {
            if(mouse.ButtonIndex == MouseButton.Left && mouse.Pressed)
            {
                isMoving = true;
                GD.Print("moving");
            }
            else
            {
                isMoving = false;
                GD.Print("STOPING");
            }
        }
    }

    public void TurnMOVING()
    {
        this.Color = Color.Color8(255, 255, 0);
    }

    public void TurnDEFAULT()
    {
       this.Color = Color.Color8(255, 255, 255);
    }

    public void TurnON()
    {
        this.Color = Color.Color8(0, 255, 0);
    }
    public void TurnOFF()
    {
        this.Color = Color.Color8(255, 0, 0);
    }


}
