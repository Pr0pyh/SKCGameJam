using Godot;
using System;

public partial class ShieldTower : Tower
{
	public int health;
    public override void _Ready()
    {
        health = 100;
    }
}
