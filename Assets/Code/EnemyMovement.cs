using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed;
    public bool canMove;
    public float cantMoveCooldownInSeconds;
    public GameObject player;

    private Rigidbody2D rb;
    private Vector2 playerDirectionVector;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDirectionVector = (player.transform.position - transform.position).normalized;
        if (canMove == true)
            rb.velocity = playerDirectionVector * movementSpeed;
        else
            rb.velocity = new Vector2(0, 0);
    }

    public void OnHitEffect()
    {
        StartCoroutine(AfterHitStop());
    }

    public IEnumerator AfterHitStop()
    {
        canMove = false;
        yield return new WaitForSeconds(cantMoveCooldownInSeconds);
        canMove = true;
    }
}
