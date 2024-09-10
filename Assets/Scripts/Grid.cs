using System.Collections;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public static Grid Instance;
    int[] gridCellsData = new int[9]; // Create an empty grid, -10 means X or O not placed yet. 
    int[] originalGridCellsData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ClearCellData();
        originalGridCellsData = (int[])gridCellsData.Clone();
    }

    public void MarkCellWithPlayerID(int cellNumber)
    {
        gridCellsData[cellNumber] = GameManager.Instance.GetPlayerTurn(); // Mark the cell with the PlayerID (X = 1) (O = 2).
    }
    public void UnMarkCellWithPlayerID(int cellNumber)
    {
        gridCellsData[cellNumber] = -10;
    }

    public void CheckGridWinner()
    {
        // if the sum of 3 cells is 3 that means that X player won and 6 if the O player won,
        // that's because each time we press on a cell on the grid, we mark it with the Player ID and we know
        // that Player X = 1, and Player 0 = 2, and the sum of a full row is either 3 or 6 :).

        // All the possible ways to win in horizontal line.
        int s1 = gridCellsData[0] + gridCellsData[1] + gridCellsData[2]; 
        int s2 = gridCellsData[3] + gridCellsData[4] + gridCellsData[5];
        int s3 = gridCellsData[6] + gridCellsData[7] + gridCellsData[8];

        // All the possible ways to win in Verticle line.
        int s4 = gridCellsData[0] + gridCellsData[3] + gridCellsData[6];
        int s5 = gridCellsData[1] + gridCellsData[4] + gridCellsData[7];
        int s6 = gridCellsData[2] + gridCellsData[5] + gridCellsData[8];

        // All the possible ways to win in diagonal line.
        int s7 = gridCellsData[0] + gridCellsData[4] + gridCellsData[8];
        int s8 = gridCellsData[6] + gridCellsData[4] + gridCellsData[2];

        int[] soulotions = { s1, s2, s3, s4, s5, s6, s7, s8 };

        for (int i = 0; i < soulotions.Length; i++)
        {
            if (soulotions[i] == 3)
            {
                // Player X has won
                GameManager.Instance.AddWinScore(0);
                UIManager.Instance.UpdateWinScore(0);
                StartCoroutine(WinnerCelebrationTime(true, 0));
            }

            if (soulotions[i] == 6)
            {
                // Player O has won
                GameManager.Instance.AddWinScore(1);
                UIManager.Instance.UpdateWinScore(1);
                StartCoroutine(WinnerCelebrationTime(true, 1));
            }
        }
    }

    void ClearCellData()
    {
        for (int i = 0; i < gridCellsData.Length; i++)
        {
            gridCellsData[i] = -10;
        }
    }

    IEnumerator WinnerCelebrationTime(bool toggle, int winnerID)
    {
        UIManager.Instance.ToggleWinPanel(toggle, winnerID);

        yield return new WaitForSeconds(1.5f);

        ClearCellData();
        UIManager.Instance.ClearGridUI();
        UIManager.Instance.ToggleWinPanel(false);

        yield return null;
    }

}
