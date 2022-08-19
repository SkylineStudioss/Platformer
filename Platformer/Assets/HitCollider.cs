using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public bool isBottom = false;
    public PlayerController pc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isBottom == false)
        {
            collision.gameObject.SetActive(false);
        } else if (collision.CompareTag("Enemy") && isBottom)
        {
            pc.collideWithEnemy = true;
            pc.justHitEnemy = true;
            pc.bounceAmount = pc.bounceAmount + 1.5f;
            Vector2 velocity = pc.rb.velocity;
            Vector2 Reflect = Vector2.Reflect(velocity, pc.transform.up).normalized;
            pc.rb.AddForce(Reflect * pc.bounceAmount, ForceMode2D.Impulse);
            collision.gameObject.SetActive(false);
            pc.enemyCombo++;
        } else
        {
            pc.collideWithEnemy = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isBottom == false)
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy") && isBottom)
        {
            pc.collideWithEnemy = true;
            pc.justHitEnemy = true;
            pc.bounceAmount = pc.bounceAmount + 3f;
            Vector2 velocity = pc.rb.velocity;
            Vector2 Reflect = Vector2.Reflect(velocity, pc.transform.up).normalized;
            pc.rb.AddForce(Reflect * pc.bounceAmount, ForceMode2D.Impulse);
            collision.gameObject.SetActive(false);
            pc.enemyCombo++;
        }
        else
        {
            pc.collideWithEnemy = false;
        }
    }
}
