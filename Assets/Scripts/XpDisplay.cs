using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XpDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform fillBar;
    [SerializeField] private TextMeshProUGUI levelText;

    public void UpdateDisplay(int currXp, int prevLevelXp, int nextLevelXp, int level)
    {
        float progressToLevel = (float)(currXp - prevLevelXp) / (nextLevelXp - prevLevelXp);
        fillBar.anchorMax = new Vector2(progressToLevel, 1f);
        levelText.text = "Level: " + level;
    }
}
