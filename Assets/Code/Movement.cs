using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 velocity;
    Rigidbody2D rb;

    public int health = 5;
    public bool canMove = true;
    public float cantMoveCooldownInSeconds = 0.5f;
    public float moveSpeed;
    public float sprintMoveSpeed;

    private float currentMoveSpeed;

    public bool isDashing;
    public bool canDash = true;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    private CinemachineImpulseSource cinemachineImpulse;
    public float cameraShakeForce = 0.2f;

    public GameObject gameOverScreen;

    public AudioSource takeDamageSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        }
        else
        {
            gameOverScreen.SetActive(true);
        }
        
    }

    private void FixedUpdate()
    {
        if (isDashing)
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
        health -= 1;
        cinemachineImpulse.GenerateImpulseWithForce(cameraShakeForce);
        takeDamageSound.Play();
    }
}
