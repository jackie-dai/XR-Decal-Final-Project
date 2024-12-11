using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private TextMeshProUGUI playerOneText;
    [SerializeField] private TextMeshProUGUI playerTwoText;
    [SerializeField] private TextMeshProUGUI playerOneTimer;
    [SerializeField] private TextMeshProUGUI playerTwoTimer;

    [SerializeField] private Target targetOne;
    [SerializeField] private Target targetTwo;

    private int maxTimer = 30;
    private int timer = 0;
    private int numOfPlayers = 1;
    private int score1;
    private int score2;

    void Start()
    {
        score1 = 0; score2 = 0;
        timer = maxTimer;
        UpdateUI();
    }

    public void RoundOver(int winner)
    {
        if (winner > 0)
        {
            if (winner == 1) score1++; else if (winner == 2) score2++;
            UpdateUI();
        }

        targetOne.Reset();
        if (numOfPlayers > 1)
        {
            targetTwo.Reset();
        }
    }

    private void UpdateUI()
    {
        playerOneText.text = "Player One: " + score1.ToString();
        playerTwoText.text = "Player Two: " + score2.ToString();
        playerOneTimer.text = "Timer: " + timer.ToString();
        playerTwoTimer.text = "Timer: " + timer.ToString();
    }
}
