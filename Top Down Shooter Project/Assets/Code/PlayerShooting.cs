using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject muzzleFlashLight;
    public GameObject gunFlash;
    public Transform shootPoint;

    public float shootCooldown;

    bool isShooting = false;

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
            StopAllCoroutines();
            muzzleFlashLight.SetActive(false);
            gunFlash.SetActive(false);
        }
    }

    IEnumerator Shoot()
    {
        while (isShooting)
        {
            //raycast hier

            //Muzzle flash
            muzzleFlashLight.SetActive(true);
            gunFlash.SetActive(true);
            yield return new WaitForSeconds(.1f);
            muzzleFlashLight.SetActive(false);
            gunFlash.SetActive(false);

            //wachten voor nieuwe kogel
            yield return new WaitForSeconds(shootCooldown);
        }
    }
}
