using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemType : ScriptableObject
{
    public Sprite icon;
    public string desc;
    public float cooldown;
    public float damage;

    public abstract void Use();
}
