using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static TextMesh CreateText(string text, Transform parent, Vector3 position)
    {
        var textGO = new GameObject("world_text", typeof(TextMesh));

        var transform = textGO.transform;
        transform.SetParent(parent);
        transform.localPosition = position;
        var textMesh = textGO.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = Color.white;
        textMesh.anchor = TextAnchor.MiddleCenter;

        return textMesh;
    }

    public static Vector3 MousePosition3D(int colliderLayer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
