using Godot;
using System;

public partial class Bullet : Area2D
{
	public int turretDamage;
	public int turretCritChance;
	[Export]
	public float speed;
	[Export]
	public AudioStreamPlayer audioPlayer;
	public override void _Ready()
	{

		BodyEntered += bodyEntered;
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 moveDir = new Vector2(1.0f, 0.0f);
		Position += new Vector2(moveDir.X * speed * (float)delta, moveDir.Y);
		if(Position.X > 2500.0f) QueueFree();
	}
	public void bodyEntered(Node body)
	{
		if (body.GetType().IsAssignableTo(typeof(Enemy)))
		{
			((Enemy)body).damage(turretDamage);
			audioPlayer.Play();
			QueueFree();
		}
	}
}
