using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashTime = 0.1f;
    [SerializeField] private Animation2D idleAnim;
    [SerializeField] private Animation2D moveAnim;
    [SerializeField] private bool defaultRight = true;

    private float health;

    protected SpriteRenderer spriteRenderer;
    protected new Rigidbody2D rigidbody2D;
    protected Animator2D animator2D;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator2D = GetComponent<Animator2D>();

        health = maxHealth;
    }

    protected virtual void Update()
    {
        bool isMoving = rigidbody2D.velocity.sqrMagnitude > 0;

        animator2D.Play(isMoving ? moveAnim : idleAnim, true);

        if (isMoving)
        {
            spriteRenderer.flipX = defaultRight ? rigidbody2D.velocity.x < 0 : rigidbody2D.velocity.x > 0;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        StartCoroutine(HitFlash());

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator HitFlash()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.color = Color.white;
    }
}
