using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    private GameController gm;
    private bool isDead = false;
    //public List<GameObject> bodyParts = new List<GameObject>();

    private void Awake()
    {
        gm = GameObject.Find("GM").GetComponent<GameController>();
    }

    private void Start()
    {
        isDead = false;
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        Checkpoint checkpoint = GetComponent<Checkpoint>();
        Transform lastCheckPoint = checkpoint.getLastCheckPoint();

        gm.Respawn(lastCheckPoint, transform);

        Destroy(gameObject);
    }
}
