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
        enemyMovementScript = collision.GetComponent<EnemyMovement>();
        enemyMovementScript.OnHitEffect();
        cinemachineImpulse.GenerateImpulseWithForce(cameraShakeForce);
        weaponOffset.Bounce();
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(cooldownInSeconds);
        cooldown = false;
    }
}
