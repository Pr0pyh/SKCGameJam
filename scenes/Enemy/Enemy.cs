using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	public virtual void damage(int amount){}
	public virtual void pushBack(){}
	public virtual void slowDown(){}
}
