using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject Egg;
    public float fireRate;
    public GameObject player;

    public List<GameObject> spawnedEggs;

    public int state = 0;

    public float shootCooldownInSeconds = 2;

    public bool canShoot = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 1)
        {
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    public IEnumerator Shoot()
    {
        canShoot = false;
        GameObject spawnedBulet = Instantiate(Egg, transform.position, transform.rotation);
        spawnedBulet.GetComponent<Egg>().originTransform = transform;
        spawnedEggs.Add(spawnedBulet);
        spawnedBulet.transform.rotation = Quaternion.FromToRotation(Vector3.right, player.transform.position - spawnedBulet.transform.position);

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
