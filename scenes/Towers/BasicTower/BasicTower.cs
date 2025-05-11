using Godot;
using System;

public partial class BasicTower : Tower
{
	[Export]
	public PackedScene bulletScene;
	[Export]
	public Timer timer;
	[Export]
	public int towerDamage;
	[Export]
    ProgressBar progressBar;
    [Export]
    AnimationPlayer animPlayer;
    [Export]
    AnimatedSprite2D sprite;
    [Export]
    AudioStreamPlayer audioPlayer;
	[Export]
    AudioStreamPlayer shootAudio;
    public override void _Ready()
    {
		// turretDamage = 10;
		// turretHealth = 100;
		// turretFireRate = 10;
        timer.Timeout += onTimeout;
		animPlayer.AnimationFinished += animFinished;
    }
    public override void _Process(double delta)
    {
        if(timer.WaitTime <= 0.05) return;
		turretFireRate = Mathf.Max(turretFireRate, 0.1f);
		timer.WaitTime = turretFireRate;
    }
	public void onTimeout()
	{
		Bullet bullet = bulletScene.Instantiate<Bullet>();
		GetParent().AddChild(bullet);
		bullet.GlobalPosition = new Vector2(GlobalPosition.X + 10.0f, GlobalPosition.Y);
		bullet.turretDamage = turretDamage;
		shootAudio.Play();
		timer.Start();
	}
    public override void damage(int amount)
    {
		turretHealth -= amount;
		progressBar.Value = turretHealth;
        animPlayer.Play("shake");
        sprite.Play("hurt");
        audioPlayer.Play();
		if(turretHealth <= 0)
		{
        	EmitSignal(SignalName.Destroyed, this);
			QueueFree();
		}
    }
	public void animFinished(StringName animName)
    {
        if(animName == "shake")
            sprite.Play("walk");
    }
}
