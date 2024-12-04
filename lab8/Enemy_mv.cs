using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform viewPoint;
    [SerializeField] private float viewRange = 0.5f;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private Transform enemyGFX;

    private int currentWaypointIndex = 0;
    private Transform playerTransform;
    private bool isChasing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckForPlayer();
        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    private void CheckForPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(viewPoint.position, viewRange, playerLayers);
        isChasing = player != null;
        if (isChasing)
            playerTransform = player.transform;
    }

    private void Patrol()
    {
        Vector3 target = waypoints[currentWaypointIndex].transform.position;

        if (Vector2.Distance(target, transform.position) < 0.5f)
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        MoveTowards(target);
    }

    private void ChasePlayer()
    {
        if (playerTransform == null) return;

        Vector3 target = playerTransform.position;
        target.y -= 0.75f;

        float minX = Mathf.Min(waypoints[0].transform.position.x, waypoints[1].transform.position.x);
        float maxX = Mathf.Max(waypoints[0].transform.position.x, waypoints[1].transform.position.x);

        target.x = Mathf.Clamp(target.x, minX, maxX);
        MoveTowards(target);
    }

    private void MoveTowards(Vector3 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);

        float direction = target.x - transform.position.x;
        enemyGFX.localScale = new Vector3(Mathf.Sign(direction) * 0.3f, 0.3f, 0.3f);
    }

    private void OnDrawGizmosSelected()
    {
        if (viewPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(viewPoint.position, viewRange);
    }
}
