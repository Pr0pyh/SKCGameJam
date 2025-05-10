using Godot;
using System;

public partial class BasicTower : Tower
{
	[Export]
	public PackedScene bulletScene;
	[Export]
	public Timer timer;
	public int towerDamage;
	public int health;
    public override void _Ready()
    {
		towerDamage = 10;
		health = 100;
        timer.Timeout += onTimeout;
    }
	public void onTimeout()
	{
		Bullet bullet = bulletScene.Instantiate<Bullet>();
		GetParent().AddChild(bullet);
		bullet.GlobalPosition = new Vector2(GlobalPosition.X + 10.0f, GlobalPosition.Y);
		bullet.turretDamage = towerDamage;
		timer.Start();
	}
    public override void damage(int amount)
    {
		if(health <= 0)
		{
        	EmitSignal(SignalName.Destroyed, this);
			QueueFree();
		}
    }
}
