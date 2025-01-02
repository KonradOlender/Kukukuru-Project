using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOffset : MonoBehaviour
{
    public Movement movement;
    public float speed = 1;
    public float bounceSpeedBonus = 1;
    public float bounceSpeedBonusTimeInSeconds = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, 0.1f * speed);
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }

    public void Bounce()
    {
        movement.OnHitEffect();
        StartCoroutine(BounceCoorutine());
    }


    public IEnumerator BounceCoorutine()
    {
        speed = -speed;
        if(speed > 0)
            speed += bounceSpeedBonus;
        else
            speed -= bounceSpeedBonus;
        
        yield return new WaitForSeconds(bounceSpeedBonusTimeInSeconds);

        if (speed > 0)
            speed -= bounceSpeedBonus;
        else
            speed += bounceSpeedBonus;

    }
}
