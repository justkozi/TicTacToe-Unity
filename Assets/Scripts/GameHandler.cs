using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public int whoTurn = 0; //x = 0 || O = 1
    public int counter = 0;
    public Button[] Gameboard; //playeable space for a game
    public string[] Icons;
    public Text XOturn;
    public Text LastWin;
    public GameObject WinPanel;
    public GameObject XScore;
    public GameObject OScore;
    public int[] MarkedFields;
    public int WinMark = 0;
    public int Mode = 0;
    public int XWins = 0;
    public int OWins = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
        XOturn.text = Icons[whoTurn];
        LastWin.text = " ";
    }

    void GameSetup()
    {
        WinPanel.SetActive(false);
        for (int i = 0; i < Gameboard.Length; i++)
        {
            Gameboard[i].interactable = true;
            Gameboard[i].GetComponentInChildren<Text>().text = " ";
            MarkedFields[i] = -10;
        }
        UpdateScoring();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void TicTacToeButton(int WhichNumber)
    {
        if (WinMark == 0)
        {
            Gameboard[WhichNumber].GetComponentInChildren<Text>().text = Icons[whoTurn];
            Gameboard[WhichNumber].interactable = false;

            MarkedFields[WhichNumber] = whoTurn + 1;
            counter++;
            if (counter > 4)
                WinnerCheck();
            if (counter > 8)
            {
                ResetButton();
            }


            if (whoTurn == 0)
            {
                whoTurn = 1;

            }
            else
            {
                whoTurn = 0;
            }
            XOturn.text = Icons[whoTurn];
        }
        else
        {
            GameSetup();
            WinMark = 0;
        }

    }

    void WinnerCheck()
//    0 | 1 | 2
//    3 | 4 | 5
//    6 | 7 | 8
    {
        int s1 = MarkedFields[0] + MarkedFields[1] + MarkedFields[2];
        int s2 = MarkedFields[3] + MarkedFields[4] + MarkedFields[5];
        int s3 = MarkedFields[6] + MarkedFields[7] + MarkedFields[8];
        int s4 = MarkedFields[0] + MarkedFields[3] + MarkedFields[6];
        int s5 = MarkedFields[1] + MarkedFields[4] + MarkedFields[7];
        int s6 = MarkedFields[2] + MarkedFields[5] + MarkedFields[8];
        int s7 = MarkedFields[0] + MarkedFields[4] + MarkedFields[8];
        int s8 = MarkedFields[2] + MarkedFields[4] + MarkedFields[6];
        var solution = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solution.Length; i++)
        {
            if (solution[i] == 3 * (whoTurn + 1))
            {
                LastWin.text = Icons[whoTurn];
                WinnerDisplay();
                WinMark = 1;
                for (int j = 0; j < Gameboard.Length; j++)
                    Gameboard[j].interactable = true;
                return;
            }
        }
    }

    void WinnerDisplay()
    {
        WinPanel.SetActive(true);
        if (whoTurn == 0)
        {
            WinPanel.GetComponentInChildren<Text>().text = "X WON!";
            XWins++;
        }
        else
        {
            WinPanel.GetComponentInChildren<Text>().text = "O WON!";
            OWins++;
        }
        counter = 0;
        UpdateScoring();
    }

    void UpdateScoring()
    {
        XScore.GetComponentInChildren<Text>().text = XWins.ToString();
        OScore.GetComponentInChildren<Text>().text = OWins.ToString();
    }

    public void ResetButton()
    {
        GameSetup();
        WinMark = 0;
        LastWin.text = " ";
        whoTurn = 0;
        XOturn.text = Icons[whoTurn];
        counter = 0;
        XWins = 0;
        OWins = 0;
        UpdateScoring();
    }
}
