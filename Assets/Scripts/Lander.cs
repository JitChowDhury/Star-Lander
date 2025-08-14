using System;
using UnityEngine;

public class Lander : MonoBehaviour
{

    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnNoForce;
    [SerializeField] private float force = 700f;
    [SerializeField] private float turnSpeed = 100f;
    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        OnNoForce?.Invoke(this, EventArgs.Empty);
        if (Input.GetKey(KeyCode.W))
        {
            // rb2d.AddForce(new Vector2(0, 1));//global up not local
            rb2d.AddForce(transform.up * Time.deltaTime * force);
            OnUpForce?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(turnSpeed * Time.deltaTime);
            OnLeftForce?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKey(KeyCode.S))
        {

        }
        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(-turnSpeed * Time.deltaTime);
            OnRightForce?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log("Crashed");
            return;
        }


        float softLandingvelocityMagnitude = 4f;
        float relativeVelocityMagnitude = collision.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > softLandingvelocityMagnitude)
        {
            Debug.Log("Landed Too Hard");
            return;
        }


        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minVector = .90f;
        if (dotVector < minVector)
        {
            Debug.Log("TOO STEEP ANGLE");
            return;
        }

        Debug.Log("SuccessFul                                                                                                   l");

        float maxScoreAmountLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;

        float maxScoreAmountLandingSpeed = 100f;
        float landingSpeedScore = (softLandingvelocityMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;


        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.GetScoreMultiplier());

        Debug.Log(score);

    }
}
