using Godot;

public partial class FoodClass : Item
{

    [Export]
    public float HungerFill { get; set; } = 32.0f;

    public FoodClass() : base() { 
        HungerFill = 32.0f;
    }

    public FoodClass(string Name, string Description, ItemType type, Vector2 GridSize,float hungerFill):
        base(Name, Description, type, GridSize)
    {
        HungerFill = hungerFill;
    }

    public override void _Ready()
    {
        base._Ready();
    }
}