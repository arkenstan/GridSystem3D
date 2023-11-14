using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem3D<T>
{
    int width,
        height,
        cellSize;

    float yLevel;

    T[,] gridEls;
    TextMesh[,] debugArray;
    Vector3 origin;

    private bool inDebug = true;

    public GridSystem3D(int width, int height, int cellSize, Vector3 origin)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;
        yLevel = origin.y;
        GenerateGrid();
    }

    void GenerateGrid()
    {
        gridEls = new T[width, height];
        debugArray = new TextMesh[width, height];
        if (inDebug)
        {
            DrawDebug();
        }
    }

    private void DrawDebug()
    {
        for (int x = 0; x < gridEls.GetLength(0); x++)
        {
            for (int z = 0; z < gridEls.GetLength(1); z++)
            {
                if (debugArray[x, z] != null)
                {
                    GameObject.Destroy(debugArray[x, z]);
                }
                debugArray[x, z] = Utils.CreateText(
                    CellName(x, z) + " - " + gridEls[x, z],
                    null,
                    GetCellWorldPosition(x, z) + new Vector3(cellSize, origin.y, cellSize) * 0.5f
                );
                Debug.DrawLine(
                    GetCellWorldPosition(x, z),
                    GetCellWorldPosition(x, z + 1),
                    Color.white,
                    100f
                );
                Debug.DrawLine(
                    GetCellWorldPosition(x, z),
                    GetCellWorldPosition(x + 1, z),
                    Color.white,
                    100f
                );
            }
        }
        Debug.DrawLine(
            GetCellWorldPosition(0, height),
            GetCellWorldPosition(width, height),
            Color.white,
            100f
        );
        Debug.DrawLine(
            GetCellWorldPosition(width, height),
            GetCellWorldPosition(width, 0),
            Color.white,
            100f
        );
    }

    private string CellName(int x, int z)
    {
        return "(" + x + "," + z + ")";
    }

    public Vector3 GetCellWorldPosition(int x, int z)
    {
        return new Vector3(x, origin.y, z) * cellSize + origin;
    }
}
