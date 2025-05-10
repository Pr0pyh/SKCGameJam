using Godot;
using System;

public partial class Chest : StaticBody2D
{
	[Export]
	public CanvasLayer hud;
	[Export]
	public Button firstButton;
	[Export]
	public Button secondButton;
	[Export]
	public Button thirdButton;
	[Export]
	public Godot.Collections.Array<CommonResource> commonResources;
	public Player player;
	CommonResource chosenCommonResource;
	CommonResource chosenCommonResource2;
	CommonResource chosenCommonResource3;
	public override void _Ready()
	{
		int number = GD.RandRange(0, commonResources.Count-1);
		chosenCommonResource = commonResources[number];
		firstButton.Text = chosenCommonResource.name;
		number = GD.RandRange(0, commonResources.Count-1);
		chosenCommonResource2 = commonResources[number];
		secondButton.Text = chosenCommonResource2.name;
		number = GD.RandRange(0, commonResources.Count-1);
		chosenCommonResource3 = commonResources[number];
		thirdButton.Text = chosenCommonResource3.name;
		GD.Print(number);
	}
	public void randomChoose()
	{
		int number = GD.RandRange(0, commonResources.Count-1);
		chosenCommonResource = commonResources[number];
		firstButton.Text = chosenCommonResource.name;
		number = GD.RandRange(0, commonResources.Count-1);
		chosenCommonResource2 = commonResources[number];
		secondButton.Text = chosenCommonResource2.name;
		number = GD.RandRange(0, commonResources.Count-1);
		chosenCommonResource3 = commonResources[number];
		thirdButton.Text = chosenCommonResource3.name;
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
	public void _on_second_item_pressed()
	{
		if(player != null) 
		{
			player.transitionToNormal();
			player.upgrade(chosenCommonResource2);
			hud.Visible = false;
		}
	}
	public void _on_third_item_pressed()
	{
		if(player != null) 
		{
			player.transitionToNormal();
			player.upgrade(chosenCommonResource3);
			hud.Visible = false;
		}
	}
}
