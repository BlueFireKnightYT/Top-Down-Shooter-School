using UnityEngine;
using UnityEngine.InputSystem;

public class rotateToMouse : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        // de rigidbody2d
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // positie van muis
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // richting van de speler tot de muis
        Vector2 direction = mousePos - rb.position;
        // bereken de rotatie voor de speler
        float rotatie = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        // zet rotatie van speler
        rb.rotation = rotatie;
    }
}
