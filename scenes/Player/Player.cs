using Godot;
using System;
using System.Data;
using System.Threading.Tasks;

public partial class Player : Node2D
{
	enum PLAYER_STATE {
		NORMAL,
		UPGRADE
	}
	[Export]
	public int health;
	[Export]
	Area2D shootArea;
	[Export]
	AnimatedSprite2D spriteImpact;
	[Export]
	Timer timer;
	[Export]
	Timer moneyTimer;
	[Export]
	Label moneyLabel;
	[Export]
	Label damageLabel;
	[Export]
	Camera2D camera;
	[Export]
	PackedScene BasicTurretScene;
	[Export]
	int neededMoneyBasic;
	[Export]
	PackedScene ShieldTurretScene;
	[Export]
	int neededMoneyShield;
	PackedScene chosenTurretScene;
	[Export]
	GroundChecker groundChecker;
	int neededMoney;
	bool canShoot;
	float amount;
	float trauma;
	PLAYER_STATE playerState;
	//stats
	float fireRate;
	int shotDamage;
	float critChance;
	float critDamage;
	int cash;
	float cashRegen;
	bool slowOnHit;
	bool pushBackOnHit;
	Godot.Collections.Array<Tower> towers;
	public override void _Ready()
	{
		playerState = PLAYER_STATE.NORMAL;
		shootArea.BodyEntered += bodyEntered;
		timer.Timeout += resetShot;
		moneyTimer.Timeout += addMoney;
		canShoot = true;
		shotDamage = 5;
		towers = new Godot.Collections.Array<Tower>();
	}
	public override void _Process(double delta)
	{
		switch(playerState)
		{
			case PLAYER_STATE.NORMAL:
				attackState();
				spawnState();
				if(camera != null) cameraUpdate((float)delta);
				break;
			case PLAYER_STATE.UPGRADE:
				break;
		}
	}
	public void transitionToUpgrade()
	{
		playerState = PLAYER_STATE.UPGRADE;
		GetTree().Paused = true;
	}
	public void transitionToNormal()
	{
		playerState = PLAYER_STATE.NORMAL;
		GetTree().Paused = false;
	}
	private void attackState()
	{
		shootArea.GlobalPosition = GetGlobalMousePosition();
		if(Input.IsActionPressed("shoot") && canShoot)
		{
			shoot();
		}
	}
	private void spawnState()
	{
		groundChecker.GlobalPosition = new Vector2(Mathf.Round(GetGlobalMousePosition().X/102)*102, Mathf.Round(GetGlobalMousePosition().Y/102)*102);
		if(Input.IsActionJustPressed("spawn") && (cash>=neededMoney) && (groundChecker.canPlace) && (groundChecker.onGround))
		{
			Tower tower = (Tower)chosenTurretScene.Instantiate();
			AddChild(tower);
			tower.GlobalPosition = new Vector2(Mathf.Round(GetGlobalMousePosition().X/102)*102, Mathf.Round(GetGlobalMousePosition().Y/102)*102);
			tower.Destroyed += deleteTower;
			towers.Add(tower);
			GD.Print(towers);
			cash -= neededMoney;
			moneyLabel.Text = "Money: " + cash;
		}
	}
	private void cameraUpdate(float delta)
	{
		if(!(trauma < 0.0))
        {
            shake();
            trauma = Mathf.Max((float)(trauma - 3*delta), 0.0f);
        }
	}
	private void shoot()
	{
		canShoot = false;
		shootArea.Visible = true;
		addTrauma();
		checkCollision();
		spriteImpact.Stop();
		spriteImpact.Play();
		timer.Start();
	}
	public void bodyEntered(Node body)
	{
		if(body.GetType().IsAssignableTo(typeof(Enemy)))
		{
			damageEnemy((Enemy)body);
		}
		else if(body.GetType().IsAssignableTo(typeof(Chest)))
		{
			((Chest)body).player = this;
			((Chest)body).hud.Visible = true;
			((Chest)body).randomChoose();
			transitionToUpgrade();
		}
	}
	public void resetShot()
	{
		canShoot = true;
		shootArea.Monitoring = false;
		shootArea.Visible = false;
	}
	public void upgrade(CommonResource commonResource)
	{
		if(timer.WaitTime > 0.1) timer.WaitTime -= commonResource.fireRate;
		health += commonResource.health;
		shotDamage += commonResource.shotDamage;
		critChance += commonResource.critChance;
		critDamage += commonResource.critDamage;
		moneyTimer.WaitTime -= commonResource.cashRegen;
		foreach(Tower tower in towers)
		{
			tower.turretDamage += commonResource.turretDamage;
			tower.turretCritChance += commonResource.turretCritChance;
			tower.turretHealth += commonResource.turretHealth;
			tower.turretFireRate -= commonResource.turretFireRate;
		}
		slowOnHit = commonResource.slowOnHit;
		pushBackOnHit = commonResource.pushBackOnHit;
		GD.Print(timer.WaitTime);
		GD.Print(health);
		GD.Print(shotDamage);
	}
	private async void checkCollision()
	{
		shootArea.Monitoring = true;
		await ToSignal(GetTree().CreateTimer(0.1), "timeout");
		shootArea.Monitoring = false;
	}
	public void addTrauma()
	{
		trauma = 7.0f;
	}
	private void shake()
    {
        amount = trauma;
		camera.Offset = new Vector2((float)(amount * (-1)), (float)(amount * (-1)));
    }
	private void damageEnemy(Enemy enemy)
	{
		int chosenDamage;
		if(GD.RandRange(0, 100) < critChance)
			chosenDamage = (int)(shotDamage*(1+critDamage));
		else
			chosenDamage = shotDamage;
		if(pushBackOnHit) enemy.pushBack();
		if(slowOnHit) enemy.slowDown();
		
		enemy.damage(shotDamage);
	}
	public async void showLabel(int shotDamage)
	{
		damageLabel.Text = shotDamage.ToString();
		damageLabel.Visible = true;
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		damageLabel.Visible = false;
	}
	public void deleteTower(Tower tower)
	{
		towers.Remove(tower);
		GD.Print(towers);
	}
	public void addMoney()
	{
		cash += 10;
		moneyLabel.Text = "Money: " + cash;
	} 
	public void _on_button_pressed()
	{
		chosenTurretScene = BasicTurretScene;
		neededMoney = neededMoneyBasic;
	}
	public void _on_button_2_pressed()
	{
		chosenTurretScene = ShieldTurretScene;
		neededMoney = neededMoneyShield;
	}
}
