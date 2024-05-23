using Godot;
using System;

public partial class InventoryManager : Node
{
    RayCast3D rayCast;
    Control HUD;
    Control InventoryScene;
    CenterContainer Hands;

    public bool hasItemInHand { get; set; } = false;

    public override void _Ready()
    {
        base._Ready();
        rayCast = GetParent().GetNode<RayCast3D>("PlayerCamera/RayCast3D");
        HUD = GetParent().GetNode<Control>("HUD");
        InventoryScene = HUD.GetNode<Control>("InventoryScene");
        Hands = InventoryScene.GetNode<CenterContainer>("Hands");
    }
    public override void _Process(double delta)
    {
        
        base._Process(delta);
        GodotObject _object = rayCast.GetCollider();
        Label interact = HUD.GetNode<Label>("Interact");
        if (_object != null)
        {
            if (_object is Item)
            {
                Item item = (Item)_object;
                interact.Text = interact.Text.ReplaceN("undefined", item.Name);
                interact.Show();
                if (Input.IsActionJustPressed("Interact"))
                {
                    if (!hasItemInHand)
                    {
                        PackedScene scene = GD.Load<PackedScene>("res://GameFiles/Interface/Items/ItemPanel.tscn");
                        var node = scene.Instantiate();

                        if (node is ItemPanel)
                        {
                            ItemPanel panel = node as ItemPanel;
                            panel.width = (int)item.InventoryGridSize.X;
                            panel.height = (int)item.InventoryGridSize.Y;
                            Hands.AddChild(panel);
                            
                        }

                        GD.Print("Equiped item");
                        hasItemInHand = true;
                        item.Destroy();
                    }
                    else
                    {
                        GD.Print("Hands are full");
                    }
                }
            }
            else
            {
                interact.Hide();
                string[] aux = interact.Text.Split(" ");
                interact.Text = interact.Text.ReplaceN(aux[aux.Length - 1], "undefined");
            }
        }
        else
        {
            interact.Hide();
            string[] aux = interact.Text.Split(" ");
            interact.Text = interact.Text.ReplaceN(aux[aux.Length - 1], "undefined");
        }
    }
}
