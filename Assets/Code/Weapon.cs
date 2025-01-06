using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Weapon : MonoBehaviour
{
    public WeaponOffset weaponOffset;
    public float cooldownInSeconds = 1;
    public bool cooldown = false;
    public int comboCounter = 0;

    public float cameraShakeForce = 0.2f;

    public AudioSource impactSound;
    public AudioSource comboSound;
    public AudioSource bombSound;

    private CinemachineImpulseSource cinemachineImpulse;
    private EnemyMovement enemyMovementScript;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            weaponOffset.Bounce();
            StartCoroutine(Cooldown());
        }
        else if (collision.gameObject.tag == "Bomb" )
        {
            weaponOffset.movement.TakeDamage();
            bombSound.Play();
            weaponOffset.Bounce();
            StartCoroutine(Cooldown());
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            comboCounter++;
            if (comboCounter % 5 > 0)
            {
                impactSound.pitch += 0.1f;
            }
            else
            {
                impactSound.pitch = 1;
            }
            
            impactSound.Play();
            //comboSound.pitch += 0.1f;
            //comboSound.Play();
            enemyMovementScript = collision.GetComponent<EnemyMovement>();
            enemyMovementScript.OnHitEffect();
            cinemachineImpulse.GenerateImpulseWithForce(cameraShakeForce);
            weaponOffset.Bounce();
            StartCoroutine(Cooldown());
        }
        else if(collision.gameObject.tag == "Bullet")
        {
            bombSound.Play();
            weaponOffset.Bounce();
            StartCoroutine(Cooldown());
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Egg")
        {
            impactSound.Play();
            weaponOffset.Bounce();
            StartCoroutine(Cooldown());
            collision.gameObject.GetComponent<Egg>().Return();
        }
        else if(collision.gameObject.tag == "Boss")
        {
            impactSound.Play();
            weaponOffset.Bounce();
            StartCoroutine(Cooldown());
            collision.gameObject.GetComponent<Boss>().health -= 1;
        }
        spriteRenderer.flipY = !spriteRenderer.flipY;
    }

    private IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(cooldownInSeconds);
        cooldown = false;
    }
}
