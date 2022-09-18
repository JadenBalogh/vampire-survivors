using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private LevelEntry[] levelEntries;

    public void Open()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        
        foreach (LevelEntry levelEntry in levelEntries)
        {
            ItemType itemType = null;
            while (itemType == null)
            {
                ItemType possibleType = GameManager.ItemTypes[Random.Range(0, GameManager.ItemTypes.Length)];
                if (GameManager.Player.GetItemLevel(possibleType) != 0)
                {
                    itemType = possibleType;
                }
            }
            levelEntry.UpdateDisplay(itemType);
        }
    }

    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
