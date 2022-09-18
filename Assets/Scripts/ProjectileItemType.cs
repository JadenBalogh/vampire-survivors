using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileItemType", menuName = "ProjectileItemType", order = 51)]
public class ProjectileItemType : ItemType
{
    public float moveSpeed = 5f;
    public float targetRadius = 10f;
    public float spawnOffset = 0.8f;
    public ProjectileTargetType targetType = ProjectileTargetType.Nearest;
    public Projectile projectilePrefab;

    private Collider2D[] targets = new Collider2D[50];

    public override void Use()
    {
        Player player = GameManager.Player;

        int hitCount = Physics2D.OverlapCircleNonAlloc(player.transform.position, targetRadius, targets, GameManager.EnemyMask);
        if (hitCount == 0)
        {
            return;
        }

        Enemy target = null;

        if (targetType == ProjectileTargetType.Nearest)
        {
            float nearestDistSqr = float.MaxValue;
            for (int i = 0; i < hitCount; i++)
            {
                Collider2D col = targets[i];
                float distSqr = (col.transform.position - player.transform.position).sqrMagnitude;
                if (distSqr < nearestDistSqr && targets[i].TryGetComponent<Enemy>(out target))
                {
                    nearestDistSqr = distSqr;
                }
            }
        }
        else if (targetType == ProjectileTargetType.Random)
        {
            targets[Random.Range(0, hitCount)].TryGetComponent<Enemy>(out target);
        }

        if (target != null)
        {
            Vector2 targetDir = (target.transform.position - player.transform.position).normalized;
            Vector2 spawnPos = (Vector2)player.transform.position + targetDir * spawnOffset;
            Quaternion targetRot = Quaternion.FromToRotation(Vector2.right, targetDir);
            Projectile projectile = Instantiate(projectilePrefab, spawnPos, targetRot);
            projectile.GetComponent<Rigidbody2D>().velocity = targetDir * moveSpeed;
            projectile.Damage = damage;
        }
    }

    public enum ProjectileTargetType
    {
        Nearest, Random
    }
}
