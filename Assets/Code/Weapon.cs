using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Weapon : MonoBehaviour
{
    public WeaponOffset weaponOffset;
    public float cooldownInSeconds = 1;
    public bool cooldown = false;

    public float cameraShakeForce = 0.2f;

    public AudioSource impactSound;
    public AudioSource comboSound;
    public AudioSource bombSound;

    private CinemachineImpulseSource cinemachineImpulse;
    private EnemyMovement enemyMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
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
        else if (collision.gameObject.tag == "Bomb")
        {
            weaponOffset.movement.TakeDamage();
            bombSound.Play();
            weaponOffset.Bounce();
            StartCoroutine(Cooldown());
        }
        else
        {
            impactSound.pitch += 0.1f;
            impactSound.Play();
            //comboSound.pitch += 0.1f;
            //comboSound.Play();
            enemyMovementScript = collision.GetComponent<EnemyMovement>();
            enemyMovementScript.OnHitEffect();
            cinemachineImpulse.GenerateImpulseWithForce(cameraShakeForce);
            weaponOffset.Bounce();
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(cooldownInSeconds);
        cooldown = false;
    }
}
