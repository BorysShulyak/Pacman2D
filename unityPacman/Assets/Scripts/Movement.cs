using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D Rigidbody { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 NextDirection { get; private set; }
    public Vector3 StartingPosition { get; private set; }

    private void Awake()
    {
        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.StartingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.Direction = this.initialDirection;
        this.NextDirection = Vector2.zero;
        this.transform.position = this.StartingPosition;
        this.Rigidbody.isKinematic = false;
        this.enabled = true;
    }

    private void Update()
    {
        if (this.NextDirection != Vector2.zero)
        {
            SetDirection(this.NextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = this.Rigidbody.position;
        Vector2 translation = this.Direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;

        this.Rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !IsDirectionOccupied(direction))
        {
            this.Direction = direction;
            this.NextDirection = Vector2.zero;
        }
        else
        {
            this.NextDirection = direction;
        }
    }

    public bool IsDirectionOccupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }

}