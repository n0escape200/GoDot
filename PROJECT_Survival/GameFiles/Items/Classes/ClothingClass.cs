using System;
using Godot;

public partial class ClothingClass: Item
{
    [Export]
    public bool hasInventory = false;
    [Export]
    public int GridX = 2;
    [Export]
    public int GridY = 2;

    public ClothingClass() : base() { }

    public ClothingClass(string Name, string Description, ItemType type, Vector2 GridSize, bool hasInventory, int gridX, int gridY)
        :base(Name,Description,type,GridSize)
    {
        this.hasInventory = hasInventory;
        GridX = gridX;
        GridY = gridY;
    }
}
