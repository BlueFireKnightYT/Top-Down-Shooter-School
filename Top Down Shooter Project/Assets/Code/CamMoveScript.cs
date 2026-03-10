using UnityEngine;

public class CamMoveScript : MonoBehaviour
{
    public Vector3[] cameraPositions;
    public int targetPos = 0;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraPositions[targetPos], 5 * Time.deltaTime);
    }
}
