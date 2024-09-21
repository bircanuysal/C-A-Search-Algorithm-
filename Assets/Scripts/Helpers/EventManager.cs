using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class EventManager
{
    public struct BuildsEvents
    {
        public static readonly UnityEvent UnitBuild = new();
        public static readonly UnityEvent<List<Vector2>> BuildedGrid = new();
    }
    public struct HealthsEvent
    {
        public static readonly UnityEvent<float> TakeDamege = new();
        public static readonly UnityEvent UnitOnDie = new();
    }
}
