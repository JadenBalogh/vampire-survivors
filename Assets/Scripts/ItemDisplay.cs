using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private ItemEntry itemEntryPrefab;

    private Dictionary<ItemType, ItemEntry> itemEntries = new Dictionary<ItemType, ItemEntry>();

    public void UpdateDisplay(List<Item> items)
    {
        foreach (Item item in items)
        {
            if (!itemEntries.ContainsKey(item.itemType))
            {
                ItemEntry itemEntry = Instantiate(itemEntryPrefab, transform);
                RectTransform itemEntryRect = itemEntry.GetComponent<RectTransform>();
                itemEntryRect.anchoredPosition = new Vector2(itemEntryRect.rect.width * itemEntries.Count, 0f);
                itemEntries.Add(item.itemType, itemEntry);
            }

            itemEntries[item.itemType].UpdateDisplay(item.itemType);
        }
    }
}
