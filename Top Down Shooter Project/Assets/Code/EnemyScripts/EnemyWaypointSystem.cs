using System.Collections;
using UnityEngine;

public class EnemyWaypointSystem : MonoBehaviour
{
    [SerializeField] Vector3[] waypoints;
    [SerializeField] int waypointIndex = 0;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] float detectionRange = 5f;

    [SerializeField] float baseEnemySpeed = 3f;
    [SerializeField] float enemyChasingSpeed = 4.5f;
    float enemySpeed;
    public bool isHit = false;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemySpeed = baseEnemySpeed;
    }

    void Update()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, playerLayer | obstacleLayer);

        Debug.DrawRay(transform.position, directionToPlayer * detectionRange, Color.red);

        if ((hit.collider != null && hit.collider.CompareTag("Player")))
        {
            enemySpeed = enemyChasingSpeed;
            ChasePlayer();
        }
        else if (!isHit)
        {
            enemySpeed = baseEnemySpeed;
            Move();
        }

        if(isHit)
        {
            enemySpeed = enemyChasingSpeed;
            ChasePlayer();
        }
    }

    void Move()
    {
        if (waypoints.Length == 0) return;

        if (waypointIndex < waypoints.Length)
        {
            Vector3 direction = (waypoints[waypointIndex] - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], enemySpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waypoints[waypointIndex]) < 0.1f)
                waypointIndex++;
        }
        else
        {
            waypointIndex = 0;
        }
    }

    void ChasePlayer()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > detectionRange)
        {
            Move();
            return;
        }
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.position = Vector3.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
    }

    public IEnumerator WaitAfterHit()
    {
        yield return new WaitForSeconds(5f);
        isHit = false;
    }
}
