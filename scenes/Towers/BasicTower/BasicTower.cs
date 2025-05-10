using Godot;
using System;

public partial class BasicTower : Tower
{
	[Export]
	public PackedScene bulletScene;
	[Export]
	public Timer timer;
	public int towerDamage;
    public override void _Ready()
    {
		turretDamage = 10;
		turretHealth = 100;
		turretFireRate = 10;
        timer.Timeout += onTimeout;
    }
    public override void _Process(double delta)
    {
        if(timer.WaitTime <= 0.05) return;
		timer.WaitTime = turretFireRate;
    }
	public void onTimeout()
	{
		Bullet bullet = bulletScene.Instantiate<Bullet>();
		GetParent().AddChild(bullet);
		bullet.GlobalPosition = new Vector2(GlobalPosition.X + 10.0f, GlobalPosition.Y);
		bullet.turretDamage = turretDamage;
		timer.Start();
	}
    public override void damage(int amount)
    {
		turretHealth -= amount;
		if(turretHealth <= 0)
		{
        	EmitSignal(SignalName.Destroyed, this);
			QueueFree();
		}
    }
}
