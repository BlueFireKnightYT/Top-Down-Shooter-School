using UnityEngine;

public class EnemyWaypointSystem : MonoBehaviour
{
    // Waypoints
    [SerializeField] Vector3[] waypoints;
    [SerializeField] int waypointIndex = 0;

    // Player
    private Transform player;
    [SerializeField] private float DisToMove = 5;

    // Enemy Stats
    [SerializeField] private float enemySpeed = 2f;

    private bool isChasing = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Check of enemy moet chasen of waypoints
        if (distance < DisToMove)
            isChasing = true;
        else
            isChasing = false;

        if (isChasing)
            ChasePlayer();
        else
            Move();
    }

    // waypoints
    private void Move()
    {
        if (waypointIndex < waypoints.Length)
        {
            Vector3 direction = (waypoints[waypointIndex] - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], enemySpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex])
                waypointIndex++;
        }
        else
        {
            waypointIndex = 0;
        }
    }

    //chase player
    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.position = Vector3.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
    }
}