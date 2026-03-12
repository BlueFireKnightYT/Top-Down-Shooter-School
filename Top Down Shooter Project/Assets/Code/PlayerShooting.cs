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
    bool isShooting = false;

    public TrailRenderer trailPrefab;
    public float bulletSpeed = 100f;

    private void Update()
    {
        Debug.DrawRay(shootPoint.position, shootPoint.up * bulletTravelLength);
    }

    public void ShootInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isShooting = true;
            StartCoroutine(Shoot());
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
            Vector3 targetPoint;
            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.up, bulletTravelLength, ~excludedLayer);

            if (hit.collider != null)
            {
                targetPoint = hit.point;
                Debug.Log(hit.collider.gameObject.name);
            }
            else
            {
                targetPoint = shootPoint.position + (shootPoint.up * bulletTravelLength);
            }

            TrailRenderer trail = Instantiate(trailPrefab, shootPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, targetPoint));

            muzzleFlashLight.SetActive(true);
            gunFlash.SetActive(true);
            yield return new WaitForSeconds(.1f);
            muzzleFlashLight.SetActive(false);
            gunFlash.SetActive(false);

            yield return new WaitForSeconds(shootCooldown);
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
}