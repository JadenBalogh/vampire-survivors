using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Gem gemPrefab;
    [SerializeField] private float damagePerTick = 1f;
    [SerializeField] private float damageTickRate = 0.25f;

    private bool canDamage = true;

    protected override void Update()
    {
        base.Update();

        Vector2 playerDir = (GameManager.Player.transform.position - transform.position).normalized;
        rigidbody2D.velocity = playerDir * moveSpeed;
    }

    protected override void Die()
    {
        Instantiate(gemPrefab, transform.position, Quaternion.identity);
        base.Die();
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (canDamage && col.gameObject.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(DamageCooldown());
            player.TakeDamage(damagePerTick);
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageTickRate);
        canDamage = true;
    }
}
