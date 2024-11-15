using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Study/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public string Name;
    public int Price;
}
