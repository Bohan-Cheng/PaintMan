using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().ResetPlayer();
        }
    }
}
