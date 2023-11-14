using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public int width;
    public int height;
    public int cellSize;

    private GridSystem3D<int> grid;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position);
        grid = new GridSystem3D<int>(width, height, cellSize, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Utils.MousePosition3D(LayerMask.NameToLayer("MouseCollider"));
            Debug.Log(mousePosition);
        }
    }
}
