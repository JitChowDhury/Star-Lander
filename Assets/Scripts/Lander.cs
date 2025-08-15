using System;
using UnityEngine;

public class Lander : MonoBehaviour
{
    public static Lander Instance { get; private set; }
    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnNoForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler<OnLandedEventArgs> OnLanded;

    public class OnLandedEventArgs : EventArgs
    {
        public int score;
    }
    [SerializeField] private float force = 700f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float fuelAmountMax = 10f;
    private float fuelAmount;
    private Rigidbody2D rb2d;



    void Awake()
    {
        fuelAmount = fuelAmountMax;
        Instance = this;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        OnNoForce?.Invoke(this, EventArgs.Empty);



        if (fuelAmount <= 0f)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            ConsumeFuel();
        }


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


        OnLanded?.Invoke(this, new OnLandedEventArgs
        {
            score = score,
        });

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FuelPickup fuelPickup))
        {
            float addFuelAmount = 7f;
            fuelAmount += addFuelAmount;

            if (fuelAmount > fuelAmountMax)
            {
                fuelAmount = fuelAmountMax;
            }
            fuelPickup.DestroySelf();
        }
        if (collision.gameObject.TryGetComponent(out CoinPickup coinPickup))
        {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroySelf();
        }

    }

    private void ConsumeFuel()
    {
        float fuelConsumptionAmount = 1f;
        fuelAmount -= fuelConsumptionAmount * Time.deltaTime;
    }

    public float GetSpeedX()
    {
        return rb2d.linearVelocityX;
    }
    public float GetSpeedY()
    {
        return rb2d.linearVelocityY;
    }
    public float GetFuel()
    {
        return fuelAmount;
    }

    public float GetFuelAmountNormalized()
    {
        return fuelAmount / fuelAmountMax;
    }
}
