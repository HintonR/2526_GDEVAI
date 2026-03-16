using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    static readonly World instance = new World();

    static GameObject[] hidingSpots;

    static World()
    {
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }

    World() {}

    public static World Instance => instance;

    public GameObject[] HidingSpots => hidingSpots;


}
