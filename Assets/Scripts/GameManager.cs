using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject targetOneSpawn;
    [SerializeField] private GameObject targetTwoSpawn;

    [SerializeField] private Target targetOne;
    [SerializeField] private Target targetTwo;

    private int numOfPlayers = 1;
    private int score1;
    private int score2;

    void Start()
    {
        score1 = 0; score2 = 0;
    }

    public void RoundOver(int winner)
    {
        if (winner > 0)
        {
            if (winner == 1) score1++; else if (winner == 2) score2++;
        }

        targetOne.Reset();
        Debug.Log("target one was reset");
        if (numOfPlayers > 1)
        {
            targetTwo.Reset();
        }
    }

    // round over(winner)
    // increase score of winner
    // spawn new target for player 1
    // if numPlayers == 2
    //  spawn new target for player 2
    
}
