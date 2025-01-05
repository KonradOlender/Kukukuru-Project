using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public Movement player;
    public int healAmount = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.gameObject.GetComponent<Movement>().HealDamage(healAmount);
            Destroy(this.gameObject);
        }
    }
}
