using Godot;
using System;

public partial class GroundChecker : Area2D
{
	public bool canPlace;
	public bool onGround;
	public override void _Ready()
	{
		BodyEntered += bodyEnter;
		BodyExited += bodyExit;
		AreaEntered += groundEntered;
		AreaExited += groundExited;
		canPlace = true;
	}
	public void bodyEnter(Node body)
	{
		if(body.GetType().IsAssignableTo(typeof(Tower)))
		{
			canPlace = false;
		}
	}
	public void bodyExit(Node body)
	{
		if(body.GetType().IsAssignableTo(typeof(Tower)))
		{
			canPlace = true;
		}
	}
	public void groundEntered(Area2D area)
	{
		if(area is Ground ground)
		{
			onGround = true;
		}
	}
	public void groundExited(Area2D area)
	{
		if(area is Ground)
			onGround = false;
	}
}
