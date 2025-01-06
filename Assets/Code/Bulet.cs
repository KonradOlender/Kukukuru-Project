using UnityEngine;

public class Bulet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Movement>().TakeDamage();
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
