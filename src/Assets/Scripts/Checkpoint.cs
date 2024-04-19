using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform lastCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "checkpoint")
        {
            lastCheckpoint = collision.gameObject.transform;
            Debug.Log("CheckPoint !");
        }
    }

    public Transform getLastCheckPoint()
    {
        return lastCheckpoint;
    }
}
