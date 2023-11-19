using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public interface iTCell
{
    bool IsPlaceable();
}

public class GridSystem3D<TCell>
{
    public int width,
        height,
        cellSize;

    public float yLevel;

    TCell[,] gridEls;
    TextMesh[,] debugArray;
    public Vector3 origin;

    private bool inDebug = true;

    public GridSystem3D(
        int width,
        int height,
        int cellSize,
        Vector3 origin,
        Func<GridSystem3D<TCell>, int, int, TCell> createGridObject
    )
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;
        yLevel = origin.y;

        gridEls = new TCell[width, height];
        for (int x = 0; x < gridEls.GetLength(0); x++)
        {
            for (int z = 0; z < gridEls.GetLength(1); z++)
            {
                gridEls[x, z] = createGridObject(this, x, z);
            }
        }

        if (inDebug)
        {
            DrawDebug();
        }
    }

    private void DrawDebug()
    {
        debugArray = new TextMesh[width, height];
        for (int x = 0; x < gridEls.GetLength(0); x++)
        {
            for (int z = 0; z < gridEls.GetLength(1); z++)
            {
                debugArray[x, z] = Utils.CreateText(
                    gridEls[x, z].ToString(),
                    null,
                    GetCellPositionInWorld(x, z) + new Vector3(cellSize, origin.y, cellSize) * 0.5f
                );
                Debug.DrawLine(
                    GetCellPositionInWorld(x, z),
                    GetCellPositionInWorld(x, z + 1),
                    Color.white,
                    100f
                );
                Debug.DrawLine(
                    GetCellPositionInWorld(x, z),
                    GetCellPositionInWorld(x + 1, z),
                    Color.white,
                    100f
                );
            }
        }
        Debug.DrawLine(
            GetCellPositionInWorld(0, height),
            GetCellPositionInWorld(width, height),
            Color.white,
            100f
        );
        Debug.DrawLine(
            GetCellPositionInWorld(width, height),
            GetCellPositionInWorld(width, 0),
            Color.white,
            100f
        );
    }

    public void GetXY(Vector3 position, out int x, out int z)
    {
        x = Mathf.FloorToInt((position - origin).x / cellSize);
        z = Mathf.FloorToInt((position - origin).z / cellSize);
    }

    private Vector3 GetCellPositionInWorld(int x, int z)
    {
        return new Vector3(x, origin.y, z) * cellSize + origin;
    }

    public TCell GetCell(int x, int z)
    {
        return gridEls[x, z];
    }

    public TCell GetCellAtPosition(Vector3 position)
    {
        int x,
            z;
        GetXY(position, out x, out z);
        return GetCell(x, z);
    }

    public Vector3 GetCellPositionInWorld(Vector3 position)
    {
        int x,
            z;
        GetXY(position, out x, out z);
        return GetCellPositionInWorld(x, z);
    }
}
