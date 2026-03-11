using UnityEngine;

public class EnemyWaypointSystem : MonoBehaviour
{
    [SerializeField] Vector3[] waypoints;
    [SerializeField] int waypointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Debug.Log(waypointIndex);
    }

    // beweeg enemy van waypoint naar waypoint, wanneer klaar ga weer terug naar de eerste waypoint en loop
    private void Move()
    {
        if (waypointIndex < waypoints.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], 1f * Time.deltaTime);
            if (transform.position == waypoints[waypointIndex])
            {
                waypointIndex++;
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }
}
