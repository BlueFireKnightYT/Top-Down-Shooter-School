using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject muzzleFlashLight;
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
        }
    }

    IEnumerator Shoot()
    {
        while (isShooting)
        {
            //raycast hier

            //Muzzle flash
            muzzleFlashLight.SetActive(true);
            yield return new WaitForSeconds(.1f);
            muzzleFlashLight.SetActive(false);

            //wachten voor nieuwe kogel
            yield return new WaitForSeconds(shootCooldown);
        }
    }
}
