using UnityEngine;

public class Lander : MonoBehaviour
{

    [SerializeField] private float force = 700f;
    [SerializeField] private float turnSpeed = 100f;
    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // rb2d.AddForce(new Vector2(0, 1));//global up not local
            rb2d.AddForce(transform.up * Time.deltaTime * force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {

        }
        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(-turnSpeed * Time.deltaTime);
        }
    }
}
