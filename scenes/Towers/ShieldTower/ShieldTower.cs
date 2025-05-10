using Godot;
using System;

public partial class ShieldTower : Tower
{
    public override void _Ready()
    {
        turretHealth = 100;
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
