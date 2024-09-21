using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class EventManager
{
    public struct Build
    {
        public static readonly UnityEvent UnitBuild = new();
        public static readonly UnityEvent<List<Vector2>> BuildedGrid = new();
    }
}
