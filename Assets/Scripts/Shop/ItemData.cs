using UnityEngine;

[System.Serializable]
public class ItemData
{
    public ItemDataColor itemDataColor;

    public ItemData(Color color)
    {
        itemDataColor.r = color.r;
        itemDataColor.g = color.g;
        itemDataColor.b = color.b;
    }
}

 [System.Serializable]
public struct ItemDataColor
{
    public float r, g, b;
}
