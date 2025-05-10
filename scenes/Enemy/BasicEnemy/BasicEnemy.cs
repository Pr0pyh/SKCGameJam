using Godot;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Linq.Expressions;

public partial class BasicEnemy : Enemy
{
	[Export]
	public int speed;
	[Export]
	public Area2D area;
	public int health = 100;
	Vector2 moveDir;
    public override void _Ready()
    {
        area.BodyEntered += bodyEntered;
    }
	public override void _PhysicsProcess(double delta)
	{
		moveDir = new Vector2(-1.0f, 0.0f);
		Velocity = moveDir*speed;
		MoveAndSlide();
	}
    public override void damage(int amount)
    {
        GD.Print(health);
		health -= amount;
		if(health <= 0)
			die();
    }
	private void die()
	{
		QueueFree();
	}
	public void bodyEntered(Node body)
	{
		if(body.GetType().IsAssignableTo(typeof(Tower)))
			((Tower)body).damage(10);
	}
}
