using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPositions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    #region Photon Callback Methods 
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            int randomSpawnPoint = Random.Range(0, spawnPositions.Length - 1);
            Vector3 instantiatePosition = spawnPositions[randomSpawnPoint].position;
            PhotonNetwork.Instantiate(playerPrefab.name, instantiatePosition, Quaternion.identity);
        }
    }
    #endregion
}
