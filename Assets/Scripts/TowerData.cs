using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : Singleton<TowerData>
{
    public List<Tower> lsTowers;
}

[System.Serializable]
public class Tower
{
    public Type towerType;
    public int levelMax;
    public int attackRange;
    public int attackSpeed;
    public int cellPrice;
    public int buildPrice;
    public int updatePrice;
    public Sprite iconLevel1;
    public Sprite iconLevel2;
    public Sprite iconLevel3;
}

[System.Serializable]
public enum Type
{
    Magic,
    Archer,
}