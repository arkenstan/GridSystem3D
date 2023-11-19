using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    EAST,
    WEST,
    NORTH,
    SOUTH
};

public enum RotationQuadrant
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH
}

[CreateAssetMenu(fileName = "Building", menuName = "Placeables/Building", order = 0)]
public class BuildingSO : ScriptableObject
{
    public GameObject prefab;

    public int width,
        length;
}
