using Godot;
using System;

public partial class CommonResource : Resource
{
	[Export]
	public int shotDamage;
	[Export]
	public int critChance;
	[Export]
	public int critDamage;
	[Export]
	public int health;
	[Export]
	public float fireRate;
	[Export]
	public int turretDamage;
	[Export]
	public int turretCritChance;
	[Export]
	public int turretHealth;
	[Export]
	public int turretFireRate;
	[Export]
	public int cashRegen;
	[Export]
	public int cashOnKill;
	[Export]
	public string imagePath;
}
