using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPun
{
  
    // Start is called before the first frame update
    void Start()
    {
        // if this is local player
        if (photonView.IsMine)
        {
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().SetPlayersOneName(photonView.Owner.NickName);
        }
        // not local player
        else
        {
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().SetPlayersTwoName(photonView.Owner.NickName);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
