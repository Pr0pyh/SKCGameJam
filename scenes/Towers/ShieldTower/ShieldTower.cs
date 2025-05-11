using Godot;
using System;

public partial class ShieldTower : Tower
{
    [Export]
    ProgressBar progressBar;
    [Export]
    AnimationPlayer animPlayer;
    [Export]
    AnimatedSprite2D sprite;
    [Export]
    AudioStreamPlayer audioPlayer;
    public override void _Ready()
    {
        // turretHealth = 100;
        progressBar.MaxValue = turretHealth;
        progressBar.Value = turretHealth;
        animPlayer.AnimationFinished += animFinished;
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
