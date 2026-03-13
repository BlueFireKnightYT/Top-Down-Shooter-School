using UnityEngine;

public class CamMoveScript : MonoBehaviour
{

    //public Vector3[] cameraPositions;
    //public int targetPos = 0;

    //private void FixedUpdate()
    //{
    //    transform.position = Vector3.Lerp(transform.position, cameraPositions[targetPos], 5 * Time.deltaTime);
    //}
    GameObject player;
    float playerPosY;
    public float lerpSpeed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        playerPosY = player.transform.position.y;
        Vector3 nextCamPos = new Vector3(0, playerPosY, -10);

        transform.position = Vector3.Lerp(transform.position, nextCamPos, lerpSpeed * Time.deltaTime);
    }
}
