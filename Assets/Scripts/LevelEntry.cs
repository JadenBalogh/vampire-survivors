using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelEntry : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI descText;

    private ItemType itemType;

    public void UpdateDisplay(ItemType itemType)
    {
        this.itemType = itemType;
        iconImage.sprite = itemType.icon;
        countText.text = "" + GameManager.Player.GetItemLevel(itemType) + " / " + Item.MAX_LEVEL;
        descText.text = itemType.desc;
    }

    public void Select()
    {
        GameManager.Player.AddItem(itemType);
    }
}
