using UnityEngine;

public class Egg : MonoBehaviour
{
    public Boss boss;
    public float speed;
    private Rigidbody2D rb;
    public Transform originTransform;

    private GameObject camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        rb.AddTorque(100);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Movement>().TakeDamage();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Obstacles")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Boss")
        {
            boss.firstStageHealth -= 1;
            Destroy(this.gameObject);
        }
    }

    public void Return()
    {
        transform.right = (originTransform.position - transform.position).normalized;
        rb.linearVelocity = transform.right * (speed * 2);
    }
}
