using Godot;
using System;

public partial class SpawnTemplate : Resource
{
    [Export]
    public float timeForWaveToLastFor;

    [Export]
    public Godot.Collections.Array<PackedScene> enemyVariants;

    [Export]
    public Godot.Collections.Array<float> spawnTimer; //contains two values, first is min time and second is max time to spawn

    [Export]
    public Godot.Collections.Array<int> lanesToSpawnIn; //contains two values, first is min lanes and second is max amount of lanes to spawn in


}
