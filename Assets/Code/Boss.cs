using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject Egg;
    public GameObject Bomb;
    public float fireRate;
    public GameObject player;

    public Vector3 originPosition;
    public Vector3 originFirePosition;

    public List<GameObject> spawnedEggs;

    public Transform shootingPoint;

    public Transform topLeft;
    public Transform downRight;

    public float health = 10;
    public float firstStageHealth = 3;

    public int state = 0;

    public float shootCooldownInSeconds = 2;

    public float dahsingCooldown = 3;
    public float dashingTime = 1;

    public bool canShoot = true;
    public bool canDash = true;
    public bool canSpawnBomb = true;
    public bool isDashing = false;
    public float dashingPower = 10;

    public SpriteRenderer spriteRenderer;

    public GameObject winDisplay;

    public List<Sprite> sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPosition = transform.position;
        originFirePosition = shootingPoint.position;
        rb = GetComponent<Rigidbody2D>();
        state = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing == false)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        if(health <=0)
        {
            winDisplay.SetActive(true);
            Destroy(this.gameObject);
        }

        if(firstStageHealth <= 0 && state == 1)
        {
            state = 2;
        }
        if(health <= 10 && state == 2)
        {
            state = 3;
            transform.position = originPosition;
            shootingPoint.transform.position = originFirePosition;
            firstStageHealth = 3;
        }
        if(firstStageHealth <= 0 && state == 3)
        {
            state = 4;
        }

        if(state == 1)
        {
            spriteRenderer.sprite = sprites[0];
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
        else if(state == 2)
        {
            spriteRenderer.sprite = sprites[1];
            if (canDash)
            {
                StartCoroutine(Dash());
            }
        }
        else if (state == 3)
        {
            spriteRenderer.sprite = sprites[2];
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
            if (canSpawnBomb)
            {
                StartCoroutine(SpawnBomb());
            }
        }
        else if (state == 4)
        {
            spriteRenderer.sprite = sprites[3];
            if (canDash)
            {
                StartCoroutine(Dash());
            }
            if (canSpawnBomb)
            {
                StartCoroutine(SpawnBomb());
            }
        }
    }

    public IEnumerator Shoot()
    {
        canShoot = false;
        GameObject spawnedBulet = Instantiate(Egg, shootingPoint.position, shootingPoint.rotation);
        spawnedBulet.GetComponent<Egg>().originTransform = transform;
        spawnedBulet.GetComponent<Egg>().boss = this;
        spawnedEggs.Add(spawnedBulet);
        spawnedBulet.transform.rotation = Quaternion.FromToRotation(Vector3.right, player.transform.position - spawnedBulet.transform.position);

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public IEnumerator SpawnBomb()
    {
        canSpawnBomb = false;
        Instantiate(Bomb, new Vector2(UnityEngine.Random.Range(topLeft.position.x, downRight.position.x), UnityEngine.Random.Range(topLeft.position.y, downRight.position.y)), shootingPoint.rotation);
        yield return new WaitForSeconds(3);
        canSpawnBomb = true;
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        rb.linearVelocity = (player.transform.position - transform.position).normalized * dashingPower;

        yield return new WaitForSeconds(dashingTime);

        isDashing = false;

        yield return new WaitForSeconds(dahsingCooldown - 1);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1);
        canDash = true;
        spriteRenderer.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Movement>().TakeDamage();
        }
    }
}
