using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : Singleton<LevelData>
{
    public List<Wave> lsLevel;
}

[System.Serializable]
public class Wave
{
    public int level;
    public List<EnemySpawn> lsWave;
}

[System.Serializable]
public class EnemySpawn
{
    public int count;
}