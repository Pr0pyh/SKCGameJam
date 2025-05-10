using Godot;
using System;

public partial class Tower : StaticBody2D
{
	[Signal]
	public delegate void DestroyedEventHandler(Tower tower);
	[Export]
	public int turretDamage;
	[Export]
	public int turretCritChance;
	[Export]
	public int turretHealth;
	[Export]
	public float turretFireRate;

	public virtual void damage(int amount){}
	public virtual void shoot() {}
}
