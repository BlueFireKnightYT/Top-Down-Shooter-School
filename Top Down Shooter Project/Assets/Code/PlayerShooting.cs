using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject muzzleFlashLight;
    public GameObject gunFlash;
    public Transform shootPoint;

    public float bulletTravelLength;
    public float shootCooldown;

    public LayerMask excludedLayer;
    bool canShoot;
    bool isShooting = false;
    bool coolingDown;

    public TrailRenderer trailPrefab;
    public float bulletSpeed = 100f;

    public int maxAmmo;
    public int currentAmmo;

    public int damage = 10;

    private void Update()
    {
        Debug.DrawRay(shootPoint.position, shootPoint.up * bulletTravelLength);
    }

    public void ShootInput(InputAction.CallbackContext context)
    {
        if (context.started && !coolingDown && canShoot)
        {
            isShooting = true;
            StartCoroutine(Shoot());
            coolingDown = true;
        }
        if (context.canceled)
        {
            isShooting = false;
            StopCoroutine(Shoot());
            muzzleFlashLight.SetActive(false);
            gunFlash.SetActive(false);
        }
    }

    IEnumerator Shoot()
    {
        while (isShooting)
        {
            changeAmmo();
            coolingDown = true;
            Vector3 targetPoint;
            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.up, bulletTravelLength, ~excludedLayer);

            if (hit.collider != null)
            {
                targetPoint = hit.point;
                DealDamage(hit.collider.gameObject);
                string hitName = hit.collider.name;
            }
            else
            {
                targetPoint = shootPoint.position + (shootPoint.up * bulletTravelLength);
            }

            TrailRenderer trail = Instantiate(trailPrefab, shootPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, targetPoint));

            muzzleFlashLight.SetActive(true);
            gunFlash.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            gunFlash.SetActive(false);
            muzzleFlashLight.SetActive(false);

            yield return new WaitForSeconds(shootCooldown);
            coolingDown = false;
        }
    }



    void DealDamage(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            EnemyWaypointSystem EWS = target.GetComponent<EnemyWaypointSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                StopCoroutine(EWS.WaitAfterHit());
                EWS.isHit = true;
                StartCoroutine(EWS.WaitAfterHit());
            }
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, time);
            time += Time.deltaTime * bulletSpeed;

            yield return null;
        }

        trail.transform.position = hitPoint;
        Destroy(trail.gameObject, trail.time);
    }

    public IEnumerator reload()
    {
        yield return new WaitForSeconds(5);
        currentAmmo = maxAmmo;
        canShoot = true;
        StopCoroutine(reload());
    }

    void changeAmmo()
    {
        if (currentAmmo > 0)
        { 
            currentAmmo--;
        }
        else
        {
            canShoot = false;
        }
        Debug.Log("Ammo: " + currentAmmo);
    }
}