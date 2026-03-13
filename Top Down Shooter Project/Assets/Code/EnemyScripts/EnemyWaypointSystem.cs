using UnityEngine;
using UnityEngine.Rendering;

public class EnemyWaypointSystem : MonoBehaviour
{
    // Waypoints
    [SerializeField] Vector3[] waypoints;
    [SerializeField] int waypointIndex = 0;

    // Player
    private Transform player;
    [SerializeField] private float disToMove = 5;

    float baseDisToMove;
    float followDisToMove;

    // Enemy Stats
    [SerializeField] private float baseEnemySpeed = 3f;
    private float enemySpeed = 3f;
    [SerializeField] private float enemyChasingSpeed = 4.5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemySpeed = baseEnemySpeed;

        baseDisToMove = disToMove;
        followDisToMove = disToMove * 2;
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Checks if enemy neeeds to chase or use waypoints
        if (distance < disToMove)
        { 
            enemySpeed = enemyChasingSpeed;
            disToMove = followDisToMove;
            ChasePlayer();
        }
        else
        { 
            enemySpeed = baseEnemySpeed;
            disToMove = baseDisToMove;
            Move();
        }          
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