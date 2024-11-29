using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unstuck : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform refPos;

    public void UnstuckPlayer()
    {
        if (refPos.position.x > player.position.x)
        {
            player.transform.position = new Vector3(player.position.x + 0.5f, player.position.y, 0);
        }
        else
        {
            player.transform.position = new Vector3(player.position.x - 0.5f, player.position.y, 0);

        }
    }
}
