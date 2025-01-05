using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float health = 2;

    public float movementSpeed;
    public bool canMove;
    public float cantMoveCooldownInSeconds;
    public GameObject player;

    public bool agroed = false;
    public float agroRange;

    protected Rigidbody2D rb;
    protected Vector2 playerDirectionVector;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.2f);
        Gizmos.DrawSphere(this.transform.position, agroRange);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            player.GetComponent<Movement>().PlaySound();
            Destroy(this.gameObject);
        }

        if (Math.Abs(transform.position.x - player.transform.position.x) <= agroRange && Math.Abs(transform.position.y - player.transform.position.y) <= agroRange)
        {
            agroed = true;
        }


        playerDirectionVector = (player.transform.position - transform.position).normalized;
        if (canMove == true)
        {
            if (agroed == true)
            {
                rb.linearVelocity = playerDirectionVector * movementSpeed;
            }
            else
            {
                rb.linearVelocity = new Vector2(0, 0);
            }
        }
        else
            rb.linearVelocity = new Vector2(0, 0);
    }

    public void OnHitEffect()
    {
        health -= 1;
        StartCoroutine(AfterHitStop());
    }

    public IEnumerator AfterHitStop()
    {
        canMove = false;
        yield return new WaitForSeconds(cantMoveCooldownInSeconds);
        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Movement player = collision.gameObject.GetComponent<Movement>();
            player.TakeDamage();
            
        }
    }
}
