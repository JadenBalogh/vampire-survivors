using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public const int MAX_LEVEL = 8;

    public ItemType itemType;
    public int level = 1;

    public bool CanUse { get; set; }
    public float Cooldown { get => itemType.cooldown; }

    public Item(ItemType itemType)
    {
        this.itemType = itemType;
        CanUse = true;
    }

    public void Use() => itemType.Use();

    public void Upgrade()
    {
        level++;
    }
}
