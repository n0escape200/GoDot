using Godot;
using System;

public partial class StorageComponentScript : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Panel icon = GetNode<Panel>("Panel");
		InventoryGridScript inventoryGrid = GetNode<InventoryGridScript>("GridContainer");
		if(inventoryGrid.height * 40 > icon.CustomMinimumSize.Y)
		{
			this.CustomMinimumSize = new Vector2(icon.CustomMinimumSize.X + inventoryGrid.width * 40, inventoryGrid.height * 40);
		}
		else
		{
			this.CustomMinimumSize = new Vector2(icon.CustomMinimumSize.X + inventoryGrid.width * 40, icon.CustomMinimumSize.Y);
		}
	}
}
