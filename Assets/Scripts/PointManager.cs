using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PointManager : Singleton<PointManager>
{
    public List<SpawnPoint> lsPoint;
    private SpawnPoint[] _point;

    private void Awake()
    {
        _point = GetComponentsInChildren<SpawnPoint>(true);
    }

    private void Start()
    {
        AddPointToList();
    }

    private void AddPointToList()
    {
        if (IsPointValid()) return;
        foreach (SpawnPoint spawnPoint in _point)
        {
            lsPoint.Add(spawnPoint);
        }
    }

    private bool IsPointValid()
    {
        return _point == null;
    }
}