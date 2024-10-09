using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    public List<PathCreator> lspath;
    private PathCreator[] _path;

    private void Awake()
    {
        _path = GetComponentsInChildren<PathCreator>(true);
    }

    private void Start()
    {
        AddPathToList();
    }

    private void AddPathToList()
    {
        if (IsPathValid()) return;
        foreach (PathCreator pathCreator in _path)
        {
            lspath.Add(pathCreator);
        }
    }

    private bool IsPathValid()
    {
        return _path == null;
    }
}