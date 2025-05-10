using Godot;
using System;

public partial class Chest : StaticBody2D
{
	[Export]
	public CanvasLayer hud;
	[Export]
	public Godot.Collections.Array<CommonResource> commonResources;
	public Player player;
	CommonResource chosenCommonResource;
    public override void _Ready()
    {
		int number = GD.RandRange(0, commonResources.Count-1);
		chosenCommonResource = commonResources[number];
		GD.Print(number);
    }
	public void randomChoose()
	{
		int number = GD.RandRange(0, commonResources.Count-1);
		chosenCommonResource = commonResources[number];
		GD.Print(number);
	}
	public void _on_first_item_pressed()
	{
		if(player != null) 
		{
			player.transitionToNormal();
			player.upgrade(chosenCommonResource);
			hud.Visible = false;
		}
	}
}
