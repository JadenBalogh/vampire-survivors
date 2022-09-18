using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private LayerMask enemyMask;
    public static LayerMask EnemyMask { get => instance.enemyMask; }

    [SerializeField] private ItemType[] itemTypes;
    public static ItemType[] ItemTypes { get => instance.itemTypes; }

    public static Player Player { get => instance.player; }
    private Player player;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static void SetPlayer(Player player)
    {
        instance.player = player;
    }
}
