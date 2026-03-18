using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 2f;
    public float attackCooldown = 1f;

    private float AttackSpeed = 0.2f;
    private Transform player;
    PlayerHealth ph;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ph = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= AttackSpeed)
        {
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }

            AttackSpeed = Time.time + attackCooldown;
        }
    }
}