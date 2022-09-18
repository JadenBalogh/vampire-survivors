using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int baseXp = 60;
    [SerializeField] private int xpStep = 40;
    [SerializeField] private XpDisplay xpDisplay;
    [SerializeField] private ItemType defaultItem;
    [SerializeField] private ItemDisplay itemDisplay;
    [SerializeField] private LevelMenu levelMenu;

    private int level = 1;
    private int currXp = 0;
    private int prevLevelXP = 0;
    private int nextLevelXP = 0;
    private int xpToLevel = 0;

    private List<Item> items = new List<Item>();

    protected override void Awake()
    {
        base.Awake();

        GameManager.SetPlayer(this);

        nextLevelXP = baseXp;
        xpToLevel = baseXp;
        xpDisplay.UpdateDisplay(currXp, prevLevelXP, nextLevelXP, level);

        AddItem(defaultItem);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainXP(20);
        }

        foreach (Item item in items)
        {
            if (item.CanUse)
            {
                StartCoroutine(ItemCooldown(item));
                item.Use();
            }
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        rigidbody2D.velocity = new Vector2(inputX, inputY) * moveSpeed;
    }

    public int GetItemLevel(ItemType itemType)
    {
        foreach (Item item in items)
        {
            if (item.itemType == itemType)
            {
                return item.level;
            }
        }
        return 0;
    }

    public void AddItem(ItemType itemType)
    {
        foreach (Item item in items)
        {
            if (item.itemType == itemType)
            {
                item.Upgrade();
                itemDisplay.UpdateDisplay(items);
                return;
            }
        }

        items.Add(new Item(itemType));
        itemDisplay.UpdateDisplay(items);
    }

    public void GainXP(int xp)
    {
        currXp += xp;

        if (currXp >= nextLevelXP)
        {
            level++;
            prevLevelXP = nextLevelXP;
            xpToLevel += xpStep;
            nextLevelXP += xpToLevel;

            levelMenu.Open();
        }

        xpDisplay.UpdateDisplay(currXp, prevLevelXP, nextLevelXP, level);
    }

    private IEnumerator ItemCooldown(Item item)
    {
        item.CanUse = false;
        yield return new WaitForSeconds(item.Cooldown);
        item.CanUse = true;
    }
}
