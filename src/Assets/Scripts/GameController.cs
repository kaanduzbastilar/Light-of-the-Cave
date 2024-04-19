using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject m_camera;
    public GameObject playerPrefab;
    public GameObject deadPlayerPrefab;
    public List<string> stones = new List<string>();


    private void Start()
    {
        stones.Add("yellow");
    }

    public void Respawn(Transform checkpointPosition, Transform currentPosition)
    {
        StartCoroutine(RespawnOnLastCheckPoint(checkpointPosition, currentPosition));

    }

    private IEnumerator RespawnOnLastCheckPoint(Transform checkpointPosition, Transform currentPosition)

    {

        GameObject deadPlayer = Instantiate(deadPlayerPrefab, currentPosition.position, currentPosition.rotation);

        yield return new WaitForSeconds(3f);

        Vector2 vector = new Vector2(checkpointPosition.position.x, checkpointPosition.position.y);

        GameObject player = Instantiate(playerPrefab, vector, transform.rotation);

        m_camera.GetComponent<CinemachineVirtualCamera>().m_Follow = player.transform;
        Destroy(deadPlayer);

    }

}
