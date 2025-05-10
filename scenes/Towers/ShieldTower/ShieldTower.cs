using Godot;
using System;

public partial class ShieldTower : Tower
{
	public int health;
    public override void _Ready()
    {
        health = 100;
    }
    public override void damage(int amount)
    {
        health -= amount;
		if(health <= 0)
		{
        	EmitSignal(SignalName.Destroyed, this);
			QueueFree();
		}
    }
}
