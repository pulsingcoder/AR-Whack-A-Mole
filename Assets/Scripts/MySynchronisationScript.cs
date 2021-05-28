using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MySynchronisationScript : MonoBehaviour, IPunObservable
{
   
    PhotonView photonView;

    int networkScore;
    int myScore;
    ScoreManager myScoreManager;
    ScoreManager otherPlayerScoreManager;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            myScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        }
        else
        {
            otherPlayerScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            otherPlayerScoreManager.DisplayScore(otherPlayerScoreManager.playerScore, networkScore);
            GetComponent<ARShoot>().score = networkScore;
        }
       
        

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // if photon view is mine then I'm the one who control this player 
            // I'll send my position, velocity data to other players
            stream.SendNext(GetComponent<ARShoot>().score);
           
        }
        else
        {
            // this will call on my instance that is there on remote player
            networkScore = (int) stream.ReceiveNext();
          
        }
    }
}
