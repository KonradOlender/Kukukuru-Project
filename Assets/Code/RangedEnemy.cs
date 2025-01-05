using UnityEngine;

public class RangedEnemy : EnemyMovement
{
    public GameObject bulet;
    void Start()
    {
        InvokeRepeating("Shoot", 0, 1f);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        playerDirectionVector = (player.transform.position - transform.position).normalized;
        
        if (canMove == true)
            rb.linearVelocity = playerDirectionVector * movementSpeed;
        else
            rb.linearVelocity = new Vector2(0, 0);
    }

    public void Shoot()
    {
        GameObject spawnedBulet = Instantiate(bulet, transform.position, transform.rotation);
        spawnedBulet.transform.rotation = Quaternion.FromToRotation(Vector3.right, player.transform.position - spawnedBulet.transform.position);
    }
}
