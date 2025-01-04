using Cinemachine;
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

    private CinemachineImpulseSource cinemachineImpulse;
    public float cameraShakeForce = 0.2f;

    private Rigidbody2D rb;
    private Vector2 playerDirectionVector;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
        //playerDirectionVector = (player.transform.position - transform.position).normalized;
        if (canMove == true)
            rb.linearVelocity = playerDirectionVector * movementSpeed;
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
            player.health -= 1;
            cinemachineImpulse.GenerateImpulseWithForce(cameraShakeForce);
        }
    }
}
