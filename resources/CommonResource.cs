using Godot;
using System;

public partial class CommonResource : Resource
{
	[Export]
	public Texture2D image;
	[Export]
	public int shotDamage;
	[Export]
	public float critChance;
	[Export]
	public float critDamage;
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
	public bool slowOnHit;
	[Export]
	public bool pushBackOnHit;
	[Export]
	public string imagePath;
	[Export]
	public string name;
}
