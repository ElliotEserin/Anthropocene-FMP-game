using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManagement : MonoBehaviour
{
    List<SpriteRenderer> foregroundObjects = new List<SpriteRenderer>();

    void Start()
    {
        foreach(SpriteRenderer i in FindObjectsOfType<SpriteRenderer>())
        {
            if (i.sortingLayerName == "Foreground")
            {
                foregroundObjects.Add(i);
                i.sortingOrder = (int)(i.gameObject.transform.position.y*-100);
            }
        }
        string[] items = AssetDatabase.FindAssets("t:Item", new[] { "Assets/Items" });

        foreach(string i in items)
        {
            Item item = (Item)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(i), typeof(Item));
            item.quantity = 0;
        }
    }
}
