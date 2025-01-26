using UnityEngine;

public class WaypointsLogic : MonoBehaviour
{
    public GameObject[] waypoints;
    public float speed = 2f;
    public float reachDistance = 1f;
    private int currentWaypointIndex = 0;
    private bool isReversing = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (waypoints.Length == 0 || rb == null) return;

        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;
        Vector3 movementDirection = (targetPosition - transform.position).normalized;
        
        float verticalVelocity = rb.velocity.y;
        rb.velocity = new Vector2(movementDirection.x * speed, verticalVelocity);

        if (movementDirection.x > 0) {
            spriteRenderer.flipX = true;
        } else spriteRenderer.flipX = false;

        if (Vector3.Distance(transform.position, targetPosition) < reachDistance && waypoints.Length > 1)
        {
            UpdateWaypointIndex();
        }
    }

    private void UpdateWaypointIndex()
    {
        if (!isReversing)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                isReversing = true;
                currentWaypointIndex = waypoints.Length - 2;
            }
        }
        else
        {
            currentWaypointIndex--;
            if (currentWaypointIndex < 0)
            {
                isReversing = false;
                currentWaypointIndex = 1;
            }
        }
    }

    public void ReversePath()
    {
        isReversing = !isReversing;

        if (waypoints.Length > 1)
        {
            if (isReversing && currentWaypointIndex > 0)
            {
                currentWaypointIndex--;
            }
            else
            {
                currentWaypointIndex++;
            }
        }
    }

    public int GetCurrentWaypointIndex()
    {
        return currentWaypointIndex;
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
