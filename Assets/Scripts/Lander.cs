using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private const float GRAVITY_NORMAL = 0.7f;

    public static Lander Instance { get; private set; }
    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnNoForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler OnFuelPickup;
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public enum LandingType
    {
        Success,
        WrongLandingArea,
        TooSteepAngle,
        TooFastLanding,
    }
    public enum State
    {
        WaitingToStart,
        Normal,
        GameOver,
    }

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public class OnLandedEventArgs : EventArgs
    {
        public LandingType landingType;
        public int score;
        public float dotVector;
        public float landingSpeed;
        public float scoreMultiplier;
    }
    [SerializeField] private float force = 700f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float fuelAmountMax = 10f;
    private float fuelAmount;
    private Rigidbody2D rb2d;
    private State state;


    void Awake()
    {
        fuelAmount = fuelAmountMax;
        Instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        state = State.WaitingToStart;
        rb2d.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        OnNoForce?.Invoke(this, EventArgs.Empty);

        switch (state)
        {
            default:
            case State.WaitingToStart:
                // if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                // if (Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed)

                if (GameInput.Instance.IsUpActionPressed() || GameInput.Instance.IsLeftActionPressed() || GameInput.Instance.IsRightActionPressed())
                {
                    rb2d.gravityScale = GRAVITY_NORMAL;
                    SetState(State.Normal);

                }

                break;
            case State.Normal:


                if (fuelAmount <= 0f)
                {
                    return;
                }

                if (GameInput.Instance.IsUpActionPressed() || GameInput.Instance.IsLeftActionPressed() || GameInput.Instance.IsRightActionPressed())
                {
                    ConsumeFuel();

                }

                if (GameInput.Instance.IsUpActionPressed())
                {
                    // rb2d.AddForce(new Vector2(0, 1));//global up not local
                    rb2d.AddForce(transform.up * Time.deltaTime * force);
                    OnUpForce?.Invoke(this, EventArgs.Empty);

                }
                if (GameInput.Instance.IsLeftActionPressed())
                {
                    rb2d.AddTorque(turnSpeed * Time.deltaTime);
                    OnLeftForce?.Invoke(this, EventArgs.Empty);


                }

                if (GameInput.Instance.IsRightActionPressed())
                {
                    rb2d.AddTorque(-turnSpeed * Time.deltaTime);
                    OnRightForce?.Invoke(this, EventArgs.Empty);


                }
                break;
            case State.GameOver:
                break;

        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log("Crashed");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingType = LandingType.WrongLandingArea,
                dotVector = 0f,
                landingSpeed = 0f,
                scoreMultiplier = 0,
                score = 0,
            });
            SetState(State.GameOver);
            return;
        }


        float softLandingvelocityMagnitude = 4f;
        float relativeVelocityMagnitude = collision.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > softLandingvelocityMagnitude)
        {
            Debug.Log("Landed Too Hard");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingType = LandingType.TooFastLanding,
                dotVector = 0f,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier = 0,
                score = 0,
            });
            SetState(State.GameOver);
            return;
        }


        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minVector = .90f;
        if (dotVector < minVector)
        {
            Debug.Log("TOO STEEP ANGLE");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingType = LandingType.TooSteepAngle,
                dotVector = dotVector,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier = landingPad.GetScoreMultiplier(),
                score = 0,
            });
            SetState(State.GameOver);
            return;
        }

        Debug.Log("SuccessFull");

        float maxScoreAmountLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;

        float maxScoreAmountLandingSpeed = 100f;
        float landingSpeedScore = (softLandingvelocityMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;


        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.GetScoreMultiplier());


        OnLanded?.Invoke(this, new OnLandedEventArgs
        {
            landingType = LandingType.Success,
            dotVector = dotVector,
            landingSpeed = relativeVelocityMagnitude,
            scoreMultiplier = landingPad.GetScoreMultiplier(),
            score = score,
        });
        SetState(State.GameOver);

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
            OnFuelPickup?.Invoke(this, EventArgs.Empty);
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

    private void SetState(State state)
    {
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = state,
        });
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
