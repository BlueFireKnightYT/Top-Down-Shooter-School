using UnityEngine;

public class CamMoveScript : MonoBehaviour
{
    GameObject player;
    public float lerpSpeed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Vector3 nextCamPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        transform.position = Vector3.Lerp(transform.position, nextCamPos, lerpSpeed * Time.deltaTime);
    }
}
