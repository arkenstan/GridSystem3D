using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public int width;
    public int height;
    public int cellSize;

    public GameObject buildingPrefab;

    public BuildingSO buildingSO;

    public GameObject floatingBuilding = null;
    public float snapThreshold = 10f;

    private GridSystem3D<GridCell> grid;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(transform.position);
        grid = new GridSystem3D<GridCell>(
            width,
            height,
            cellSize,
            transform.position,
            (GridSystem3D<GridCell> g, int x, int z) => new GridCell(g, x, z)
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Utils.MousePosition3D(LayerMask.NameToLayer("MouseCollider"));
            var gridCellOrigin = grid.GetCellPositionInWorld(mousePosition);

            var gridCell = grid.GetCellAtPosition(gridCellOrigin);

            var buildingCells = GetBuildingCells(gridCell, buildingSO, RotationQuadrant.FIRST);

            if (buildingCells.Count > 0)
            {
                Debug.Log(buildingCells);
                if (IsAllPlaceable(buildingCells))
                {
                    var buildingInstance = Instantiate(
                        buildingSO.prefab,
                        gridCellOrigin,
                        Quaternion.identity
                    );
                    UpdateTransforms(buildingCells, buildingInstance.transform);
                }
            }
        }
        else
        {
            Vector3 mousePosition = Utils.MousePosition3D(LayerMask.NameToLayer("MouseCollider"));

            var gridCellOrigin = grid.GetCellPositionInWorld(mousePosition);

            if (!floatingBuilding)
            {
                floatingBuilding = Instantiate(
                    buildingSO.prefab,
                    gridCellOrigin,
                    Quaternion.identity
                );
            }

            var diff = mousePosition - gridCellOrigin;

            Debug.Log(diff.magnitude + "," + snapThreshold);

            if (diff.magnitude <= snapThreshold)
            {
                floatingBuilding.transform.position = gridCellOrigin;
            }
            else
            {
                mousePosition.y = grid.yLevel;
                floatingBuilding.transform.position = mousePosition;
            }
        }
    }

    void UpdateTransforms(List<GridCell> cells, Transform transform)
    {
        foreach (var cell in cells)
        {
            cell.SetTransform(transform);
        }
    }

    bool IsAllPlaceable(List<GridCell> cells)
    {
        bool isPlacable = true;

        foreach (var cell in cells)
        {
            if (!cell.IsPlaceable())
            {
                isPlacable = false;
                break;
            }
        }
        return isPlacable;
    }

    List<GridCell> GetBuildingCells(
        GridCell originCell,
        BuildingSO building,
        RotationQuadrant rotation
    )
    {
        List<GridCell> cells = new List<GridCell> { originCell };

        int currentLength = Lodash.Includes<RotationQuadrant>(
            new List<RotationQuadrant>() { RotationQuadrant.SECOND, RotationQuadrant.FOURTH },
            rotation
        )
            ? building.width
            : building.length;

        int currentWidth = Lodash.Includes<RotationQuadrant>(
            new List<RotationQuadrant>() { RotationQuadrant.SECOND, RotationQuadrant.FOURTH },
            rotation
        )
            ? building.length
            : building.width;

        int incrementX = Lodash.Includes<RotationQuadrant>(
            new List<RotationQuadrant>() { RotationQuadrant.SECOND, RotationQuadrant.THIRD },
            rotation
        )
            ? -1
            : 1;

        int incrementZ = Lodash.Includes<RotationQuadrant>(
            new List<RotationQuadrant>() { RotationQuadrant.THIRD, RotationQuadrant.FOURTH },
            rotation
        )
            ? -1
            : 1;
        ;

        for (int x = originCell.GetX(); x < currentLength + originCell.GetX(); x = x + incrementX)
        {
            for (
                int z = originCell.GetZ();
                z < currentWidth + originCell.GetZ();
                z = z + incrementZ
            )
            {
                try
                {
                    Debug.Log("CELL x,Z" + x + "," + z);
                    var cell = grid.GetCell(x, z);
                    cells.Add(cell);
                }
                catch (System.Exception)
                {
                    cells = new List<GridCell>();
                    break;
                }
            }
        }

        return cells;
    }
}
