using System.Collections.Generic;
using UnityEngine;

public class Lodash
{
    public static bool Includes<T>(List<T> items, T compare)
    {
        return items.Contains(compare);
    }
}
