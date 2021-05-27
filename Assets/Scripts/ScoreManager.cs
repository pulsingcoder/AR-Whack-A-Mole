using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ScoreManager : MonoBehaviour
{
    public Text player1Text;
    public Text player2Text;
    public int playerScore;
    public Text player1Name;
    public Text player2Name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayScore(int score1, int score2)
    {
        player1Text.text = score1.ToString();
        player2Text.text = score2.ToString();
    }
    public void UpdateScore(int score)
    {
        print("AT score");
        player1Text.text = score.ToString();
        playerScore = score;
    }

    public void SetPlayersOneName(string playerOneName)
    {
        player1Name.text = playerOneName;
    }

    public void SetPlayersTwoName(string playerTwoName)
    {
        player2Name.text = playerTwoName;
    }
}
