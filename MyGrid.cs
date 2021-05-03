using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid
{
    int gridWidth = 0;
    int gridHeight = 0;
    float y = 1f;
    List<Vector3> gridCoordinates;


    public void Grid(int width, int height)
    {
        this.gridWidth = width;
        this.gridHeight = height;

        gridCoordinates = new List<Vector3>();


    }
}
