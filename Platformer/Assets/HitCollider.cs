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
            Vector2 velocity = pc.rb.velocity;
            Vector2 Reflect = Vector2.Reflect(velocity, pc.transform.up).normalized;
            pc.rb.AddForce(Reflect * pc.bounceAmount, ForceMode2D.Impulse);
            pc.bounceAmount = pc.bounceAmount + 1.5f;
            collision.gameObject.SetActive(false);
            pc.enemyCombo++;
        } else
        {
            pc.collideWithEnemy = false;
            pc.justHitEnemy = false;
        }
        if (collision.CompareTag("Enemy") && isBottom == true && pc.isGroundPounding)
        {
            pc.collideWithEnemy = true;
            pc.justHitEnemy = true;
            pc.rb.velocity += new Vector2(pc.rb.velocity.x, pc.bounceAmount);
            pc.bounceAmount = pc.bounceAmount + 1.5f;
            pc.justHitEnemy = true;
            StartCoroutine(pc.WaitBeforePound());
            collision.gameObject.SetActive(false);
            pc.enemyCombo++;
        }
        else
        {
            pc.collideWithEnemy = false;
            pc.justHitEnemy = false;
        }

        if (collision.CompareTag("Enemy") && isBottom == false && pc.isSliding)
        {
            pc.collideWithEnemy = true;
            pc.justHitEnemy = true;
            pc.bounceAmount = pc.bounceAmount + 3f;
            pc.rb.velocity += new Vector2(pc.bounceAmount, pc.rb.velocity.y);
            pc.justHitEnemy = true;
            StartCoroutine(pc.WaitBeforePound());
            collision.gameObject.SetActive(false);
            pc.enemyCombo++;
        }
        else
        {
            pc.collideWithEnemy = false;
            pc.justHitEnemy = false;
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
            Vector2 velocity = new Vector2(pc.momentum, pc.rb.velocity.y);
            Vector2 Reflect = Vector2.Reflect(velocity, pc.transform.up).normalized;
            pc.rb.AddForce(Reflect * pc.bounceAmount, ForceMode2D.Impulse);
            pc.bounceAmount = pc.bounceAmount + 1.5f;
            collision.gameObject.SetActive(false);
            pc.enemyCombo++;
        }
        else
        {
            pc.collideWithEnemy = false;
            pc.justHitEnemy = false;
        }

        if (collision.CompareTag("Enemy") && isBottom == true && pc.isGroundPounding)
        {
            pc.collideWithEnemy = true;
            pc.justHitEnemy = true;
            pc.bounceAmount = pc.bounceAmount + 3f;
            pc.rb.velocity += new Vector2(pc.rb.velocity.x, pc.bounceAmount);
            pc.justHitEnemy = true;
            StartCoroutine(pc.WaitBeforePound());
            collision.gameObject.SetActive(false);
            pc.enemyCombo++;
        }
        else
        {
            pc.collideWithEnemy = false;
            pc.justHitEnemy = false;
        }
        if (collision.CompareTag("Enemy") && isBottom == false && pc.isSliding)
        {
            pc.collideWithEnemy = true;
            pc.justHitEnemy = true;
            pc.bounceAmount = pc.bounceAmount + 3f;
            pc.rb.velocity += new Vector2(pc.bounceAmount, pc.rb.velocity.y);
            pc.justHitEnemy = true;
            StartCoroutine(pc.WaitBeforePound());
            collision.gameObject.SetActive(false);
            pc.enemyCombo++;
        }
        else
        {
            pc.collideWithEnemy = false;
            pc.justHitEnemy = false;
        }
    }
}
