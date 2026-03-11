using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    GameObject player;
    public CamMoveScript camMoveScript;
    public int TriggerNumber;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (camMoveScript.targetPos == TriggerNumber - 1)
            {
                camMoveScript.targetPos = TriggerNumber;
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1);
            }
            else
            {
                camMoveScript.targetPos = TriggerNumber - 1;
                player.transform.localPosition = new Vector2(player.transform.position.x, player.transform.position.y - 1);
            }
        }
    }
}
