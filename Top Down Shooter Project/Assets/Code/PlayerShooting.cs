using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject muzzleFlashLight;
    public GameObject gunFlash;
    public Transform shootPoint;
    public TextMeshProUGUI ammoText;

    public float bulletTravelLength;
    public float shootCooldown;

    public LayerMask excludedLayer;
    bool canShoot = true;
    bool isShooting = false;
    bool reloading = false;
    bool coolingDown = false;

    public TrailRenderer trailPrefab;
    public float bulletSpeed = 100f;

    public int maxAmmo;
    public int currentAmmo;

    public int damage = 10;

    private void Start()
    {
        UpdateAmmoText();
    }
    private void Update()
    {
        Debug.DrawRay(shootPoint.position, shootPoint.up * bulletTravelLength);
        if (currentAmmo == 0)
        {
            StopShooting();
            canShoot = false;
        }
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
            StopShooting();
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

    public void reloadAmmo()
    {
        if (!reloading)
        { 
            StartCoroutine(reload());
            reloading = true;
        }
    }

    IEnumerator reload()
    {
        StopShooting();
        canShoot = false;

        yield return new WaitForSeconds(3);
        currentAmmo = maxAmmo;
        canShoot = true;
        reloading = false;
        UpdateAmmoText();
    }

    void StopShooting()
    {
        isShooting = false;
        StopCoroutine(Shoot());
        muzzleFlashLight.SetActive(false);
        gunFlash.SetActive(false);
    }
    void changeAmmo()
    {
        if (currentAmmo > 0)
        { 
            currentAmmo--;
            UpdateAmmoText();
        }
        Debug.Log("Ammo: " + currentAmmo);
    }

    void UpdateAmmoText()
    {
        ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }
}