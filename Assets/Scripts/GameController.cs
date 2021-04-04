using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;

    private string user = "X";
    private string computer = "O";
    private int moveCount;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        moveCount = 0;
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;

        if (buttonList[2].text == playerSide && buttonList[1].text == playerSide && buttonList[0].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (moveCount >= 9)
        {
            GameOver("tie");
        }

        ChangeSides();
    }

    void ChangeSides()
    {
        if (playerSide == "X")
            playerSide = "O";
        else
            playerSide = "X";
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);

        if (winningPlayer == "tie")
        {
            SetGameOverText("It's a Tie!");
        }
        else
        {
            if (winningPlayer == computer)
            {
                SetGameOverText("AI Wins!");
            }
            else
            {
                SetGameOverText("Human Wins!");
            }
        }

        restartButton.SetActive(true);
        //for (int i = 0; i < buttonList.Length; i++)
        //{
        //    buttonList[i].GetComponentInParent<Button>().interactable = false;
        //}
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public void RestartGame()
    {
        playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetBoardInteractable(true);

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }

    public int FindBestMove()
    {
        int bestVal = -1000;
        int bestMove = -1;

        for (int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].text == "")
            {
                buttonList[i].text = computer;

                int moveVal = MiniMax(false);

                buttonList[i].text = "";

                if (moveVal > bestVal)
                {
                    bestMove = i;
                    bestVal = moveVal;
                }
            }
        }

        return bestMove;
    }

    private int MiniMax(bool isMax)
    {
        int score = evaluate();

        if (score == 10)
        {
            return score;
        }

        if (score == -10)
        {
            return score;
        }

        if (IsMovesLeft() == false)
        {
            return 0;
        }

        if (isMax)
        {
            int best = -1000;

            for (int i = 0; i < buttonList.Length; i++)
            {
                if (buttonList[i].text == "")
                {
                    buttonList[i].text = computer;

                    best = Mathf.Max(best, MiniMax(!isMax));

                    buttonList[i].text = "";
                }
            }

            return best;
        }
        else
        {
            int best = 1000;

            for (int i = 0; i < buttonList.Length; i++)
            {
                if (buttonList[i].text == "")
                {
                    buttonList[i].text = user;

                    best = Mathf.Min(best, MiniMax(!isMax));

                    buttonList[i].text = "";
                }
            }

            return best;
        }
    }
}
