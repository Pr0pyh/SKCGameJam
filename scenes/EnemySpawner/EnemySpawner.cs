using Godot;
using System;

public partial class EnemySpawner : Node2D
{

    [Export]
    public Godot.Collections.Array<SpawnTemplate> spawnTemplates;
    [Export]
    public Godot.Collections.Array<Node2D> spawnPositions;
    [Export]
    public int currentWave = 0;
    [Export]
    Timer waveTimer;
    [Export]
    Timer enemySpawnTimer;
    [Export]
    PackedScene chestScene;

    public override void _Ready()
    {
        waveTimer.Timeout += StartNextWave;
        enemySpawnTimer.Timeout += RunWave;
        SetWaveTimer(spawnTemplates[currentWave]);
    }
    public override void _Process(double delta)
    {

    }

    public void RunWave()
    {
        //GD.Print("Radimo wave:" + currentWave + " * " + (timer.WaitTime - timer.TimeLeft));
        int numberOfLanesToSpawnIn = GD.RandRange(
                spawnTemplates[currentWave].lanesToSpawnIn[0],
                spawnTemplates[currentWave].lanesToSpawnIn[1]);

        GD.Print("lane count: " + numberOfLanesToSpawnIn);
        for (int i = 0; i < numberOfLanesToSpawnIn; i++)
        {
            SpawnInLane(
                GD.RandRange(
                    0,
                    spawnPositions.Count - 1),
                1);
        }
        SetEnemySpawnTimer();
    }

    public void SpawnInLane(int laneId, int amount)
    {
        GD.Print("-spawn in lane " + laneId + " count: " + amount);
        for (int i = 0; i < amount; i++)
        {
            int enemyId = GD.RandRange(0, spawnTemplates[currentWave].enemyVariants.Count - 1);
            Enemy mob = spawnTemplates[currentWave].enemyVariants[enemyId].Instantiate<Enemy>();

            mob.Position = spawnPositions[laneId].Position;
            AddChild(mob);
            GD.Print("--" + enemyId + " " + mob.Position);
        }
    }

    public void StartNextWave()
    {
        GD.Print("next wave " + currentWave);
        currentWave++;
        if (spawnTemplates.Count - 1 < currentWave) currentWave = 0;
        SetWaveTimer(spawnTemplates[currentWave]);
        SetEnemySpawnTimer();
        if (Chest.numberOfChests == 0)
        {
            Chest chest = chestScene.Instantiate<Chest>();
            AddChild(chest);
            chest.GlobalPosition = new Vector2(GetViewportRect().Size.X / 2, GetViewportRect().Size.Y / 2);

            GD.Print(chest.Position);
        }
        Chest.numberOfChests++;
        //mze i neki tekst da je poceo novi wave da se stavi na ekran
    }

    public void SetEnemySpawnTimer()
    {
        enemySpawnTimer.WaitTime = GD.RandRange(
            spawnTemplates[currentWave].spawnTimer[0],
            spawnTemplates[currentWave].spawnTimer[1]);
        enemySpawnTimer.Start();
    }

    public void SetWaveTimer(SpawnTemplate spawnTemplate)
    {
        waveTimer.WaitTime = spawnTemplate.timeForWaveToLastFor;
        waveTimer.Start();
    }

}
