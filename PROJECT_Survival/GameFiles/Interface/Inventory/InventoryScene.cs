using Godot;
using System;

public partial class InventoryScene : Control
{
    CenterContainer hands;

    TextureRect Head,Neck,LSholder,RSholder,Top,Under,Back,Pants,Footware;
    Control Body;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        hands = GetNode<CenterContainer>("Hands");
        Body = GetNode<Control>("Body");

        Head = Body.GetNode<TextureRect>("Head");
        Neck = Body.GetNode<TextureRect>("Neck");
        LSholder = Body.GetNode<TextureRect>("LeftSholder");
        RSholder = Body.GetNode<TextureRect>("RightSholder");
        Top = Body.GetNode<TextureRect>("Top");
        Under = Body.GetNode<TextureRect>("Under");
        Back = Body.GetNode<TextureRect>("Back");
        Pants = Body.GetNode<TextureRect>("Pants");
        Footware = Body.GetNode<TextureRect>("Footware");

    }


    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if(@event is InputEventMouseMotion motion)
        {
            if (hands.GetChildCount() == 1)
            {
                ItemPanel item = hands.GetChild(0) as ItemPanel;
                if (item.isMoving)
                {
                    item.Position = GetGlobalMousePosition() - hands.Position; 
                }
            }

            foreach (Control part in Body.GetChildren())
            {
                if (part.GetRect().HasPoint(motion.Position))
                {
                    GD.Print(part.Name);
                }
            }
        }
    }

}
