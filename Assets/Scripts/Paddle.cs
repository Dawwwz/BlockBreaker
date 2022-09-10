using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Paddle : MonoBehaviour
{
    [HideInInspector] public new Rigidbody rigidbody { get; private set; }
    [HideInInspector] public Vector2 direction { get; private set; }
    public float speed ;
    [HideInInspector] public float maxBounceAngle = 75f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ResetPaddle();
    }

    public void ResetPaddle()
    {
        rigidbody.velocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            direction = Vector2.left;
            if (rigidbody.velocity.x > 0)
            {
                rigidbody.velocity = new Vector3(0,0,0);
            }
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction = Vector2.right;
            if (rigidbody.velocity.x < 0)
            {
                rigidbody.velocity = new Vector3(0, 0, 0);
            }

        } else {
            direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero) {
            rigidbody.AddForce(direction * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector2 paddlePosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float maxOffset = collision.collider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float bounceAngle = (offset / maxOffset) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }

}
