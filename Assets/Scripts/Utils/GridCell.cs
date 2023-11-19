using UnityEngine;

public class GridCell : iTCell
{
    private GridSystem3D<GridCell> gridReference;

    private int x;
    private int z;

    public int GetX()
    {
        return x;
    }

    public int GetZ()
    {
        return z;
    }

    public Transform transform;

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
    }

    public GridCell(GridSystem3D<GridCell> gridRef, int x, int z)
    {
        gridReference = gridRef;
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return x + " - " + z;
    }

    public bool IsPlaceable()
    {
        return transform == null;
    }
}
