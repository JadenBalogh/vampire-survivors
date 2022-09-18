using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemEntry : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public void UpdateDisplay(ItemType itemType)
    {
        iconImage.sprite = itemType.icon;
        itemText.text = GameManager.Player.GetItemLevel(itemType) + " / " + Item.MAX_LEVEL;
    }
}
