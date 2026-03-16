using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHeal : MonoBehaviour
{
    PlayerHealth hpScript;
    public TextMeshProUGUI medkitText;
    int medkits = 0;

    public LayerMask pickupLayer;
    public float pickupRange;
    public int addedHP;

    private void Start()
    {
        hpScript = GetComponent<PlayerHealth>();
    }

    public void PickUpItems(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, pickupRange, pickupLayer);

            if (hit != null)
            {
                medkits++;
                UpdateMedkitText();
                Destroy(hit.gameObject);
                Debug.Log(medkits);
            }
        }
    }

    public void UseMedkit(InputAction.CallbackContext context)
    {
        if (medkits > 0 && context.performed && hpScript.currentHealth != hpScript.maxHealth)
        {
            medkits--;
            UpdateMedkitText();
            hpScript.currentHealth += addedHP;
            hpScript.UpdateHPBar();
        }
    }

    void UpdateMedkitText()
    {
        medkitText.text = medkits.ToString();
    }
}
