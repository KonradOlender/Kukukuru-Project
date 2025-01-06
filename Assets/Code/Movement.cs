using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Vector2 velocity;
    Rigidbody2D rb;
    SpriteRenderer sprite;

    public int health = 5;
    public bool canMove = true;
    public float cantMoveCooldownInSeconds = 0.5f;
    public float moveSpeed;
    public float sprintMoveSpeed;

    private float currentMoveSpeed;

    public bool isDashing;
    public bool isSpearing;
    public bool isSpinning;
    public bool canDash = true;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    private CinemachineImpulseSource cinemachineImpulse;
    public float cameraShakeForce = 0.2f;

    public GameObject gameOverScreen;

    public AudioSource takeDamageSound;
    public AudioSource healSound;
    public AudioSource onEnemyDeathSound;

    public GameObject escapeMenuDisplay;
    public bool escapeMenuDisplaed = false;

    public bool HealthBarReset = false;


    public List<Sprite> spriteList;

    public float spearSpeed;

    public WeaponOffset weaponOfset;

    public float spinningBonusSpeed;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 5)
        {
            health = 5;
        }
        if(health > 0)
        {
            if (canMove)
                velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            else
                velocity = new Vector2(0, 0);

            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                StartCoroutine(Dash());
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                currentMoveSpeed = sprintMoveSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && canDash)
            {
                currentMoveSpeed = moveSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Spear");
                isSpearing = true;
                rb.linearVelocity = weaponOfset.transform.right * spearSpeed;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Debug.Log("Spear");
                isSpearing = false;
                //rb.linearVelocity = transform.right * spearSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Spin");
                isSpinning = true;
                if(weaponOfset.speed > 0)
                {
                    weaponOfset.speed += spinningBonusSpeed;
                }
                else if(weaponOfset.speed < 0)
                {
                    weaponOfset.speed -= spinningBonusSpeed;
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Debug.Log("Spin");
                isSpinning = false;
                if (weaponOfset.speed > 0)
                {
                    weaponOfset.speed -= spinningBonusSpeed;
                }
                else if (weaponOfset.speed < 0)
                {
                    weaponOfset.speed += spinningBonusSpeed;
                }
            }
        }
        else
        {
            gameOverScreen.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeMenuDisplaed = !escapeMenuDisplaed;
            if (escapeMenuDisplaed)
            {
                escapeMenuDisplay.SetActive(true);
            }
            else
            {
                escapeMenuDisplay.SetActive(false);
            }
        }
        
        //direction of sprite
        if (velocity.y < 0)
            sprite.sprite = spriteList[0];
        else if (velocity.y > 0)
            sprite.sprite = spriteList[1];
        
        if (velocity.x > 0)
        {
            sprite.sprite = spriteList[2];
            sprite.flipX = false;
        }
        else if (velocity.x < 0)
        {
            sprite.sprite = spriteList[2];
            sprite.flipX = true;
        }
    }


    private void FixedUpdate()
    {
        if (isDashing || isSpearing)
        {
            return;
        }
        rb.linearVelocity = (velocity * moveSpeed);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        //dashing mechanic

        rb.linearVelocity = rb.linearVelocity * dashingPower;
        //
        
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        
        canDash = true;
        //trail.emitting = true;
        //trail.emitting = false;
    }

    //when we atack
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

    //when we take damage
    public void TakeDamage()
    {
        HealthBarReset = true;
        health -= 1;
        cinemachineImpulse.GenerateImpulseWithForce(cameraShakeForce);
        takeDamageSound.Play();
    }

    public void HealDamage(int amount)
    {
        HealthBarReset = true;
        health += amount;
        healSound.Play();
    }

    public void ResumeGame()
    {
        escapeMenuDisplay.SetActive(false);
    }

    public void PlaySound()
    {
        onEnemyDeathSound.Play();
    }
}
