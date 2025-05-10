using Godot;
using System;

public partial class Tower : StaticBody2D
{
	[Signal]
	public delegate void DestroyedEventHandler(Tower tower);
	public virtual void damage(int amount){}
	public virtual void shoot() {}
}
