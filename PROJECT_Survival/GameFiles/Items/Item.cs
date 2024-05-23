using Godot;
using System;

public partial class Item : Node3D
{
	public enum ItemType
	{
		Default,
		Food,
		Water,
		Gun,
		Attachment,
		Ammo,
		Material,
		Clothing
	};
	[Export]
	public new string Name { get; set; }

	[Export(PropertyHint.MultilineText)]
	public string Description { get; set; }

	[Export]
	public ItemType Type { get; set; }

	[Export]
	public Vector2 InventoryGridSize { get; set; }

	public Item()
	{
		Name = "undefined";
		Description = "undefined";
		Type = ItemType.Default;
		InventoryGridSize = new Vector2(0,0);
	}
	
	public Item(string _name, string _description, ItemType _type, Vector2 GridSize)
	{
		Name = _name;
		Description = _description;
		Type = _type;
		InventoryGridSize = GridSize;
	}

	public void Destroy()
	{
		this.Free();
	}
}
