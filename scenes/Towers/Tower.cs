using Godot;
using System;

public partial class Tower : StaticBody2D
{
	[Signal]
	public delegate void DestroyedEventHandler(Tower tower);
	public int turretDamage;
	public int turretCritChance;
	public int turretHealth;
	public int turretFireRate;

	public virtual void damage(int amount){}
	public virtual void shoot() {}
}
