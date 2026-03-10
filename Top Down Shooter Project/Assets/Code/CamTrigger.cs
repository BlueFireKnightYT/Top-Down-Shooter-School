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
        if (camMoveScript.targetPos == TriggerNumber -1)
        {
            camMoveScript.targetPos = TriggerNumber;
        }
        else camMoveScript.targetPos = TriggerNumber -1;
    }
}
