using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Button[] cellsButtons;
    [SerializeField] GameObject[] xAndO_Sprites;
    [SerializeField] Transform currentGrid;

    [SerializeField] GameObject[] playerTurnIndicator;
    [SerializeField] TextMeshProUGUI[] playersScore;
    [SerializeField] GameObject winnerPanel;
    [SerializeField] TextMeshProUGUI winnerText;

    private Dictionary<int, GameObject> cellObjects = new Dictionary<int, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ClearGridUI();
        ShowPlayerTurn();
    }

    public void PlaceXorO(int cellNumber)
    {
        cellsButtons[cellNumber].image.raycastTarget = false;

        GameObject xOrOSprite = Instantiate(
           xAndO_Sprites[GameManager.Instance.GetPlayerTurn() - 1],
           cellsButtons[cellNumber].transform.position,
           Quaternion.identity, currentGrid);

        cellObjects[cellNumber] = xOrOSprite;

        Grid.Instance.MarkCellWithPlayerID(cellNumber);
    }
    public void UndoPlaceXorO(int cellNumber)
    {
        if (cellObjects.ContainsKey(cellNumber))
        {
            Destroy(cellObjects[cellNumber]);
            cellObjects.Remove(cellNumber);
        }

        cellsButtons[cellNumber].image.raycastTarget = true;

        Grid.Instance.UnMarkCellWithPlayerID(cellNumber);
       
    }

    public void ClearGridUI()
    {
        for (int i = 0; i < cellsButtons.Length; i++)
        {
            cellsButtons[i].image.raycastTarget = true;
        }


        for (int i = 0; i < currentGrid.childCount; i++)
        {
            Destroy(currentGrid.GetChild(i).gameObject);
        }
    }

    public void ShowPlayerTurn()
    {
        if (GameManager.Instance.GetPlayerTurn() == 1)
        {
            playerTurnIndicator[0].SetActive(true);
            playerTurnIndicator[1].SetActive(false);
        }

        if (GameManager.Instance.GetPlayerTurn() == 2)
        {
            playerTurnIndicator[0].SetActive(false);
            playerTurnIndicator[1].SetActive(true);
        }
    }

    public void UpdateWinScore(int playerNum)
    {
        playersScore[playerNum].text = GameManager.Instance.GetPlayerWinScore(playerNum).ToString();
    }

    public void ToggleWinPanel(bool toggle)
    {
        winnerPanel.SetActive(toggle);
    }

    public void ToggleWinPanel(bool toggle, int winnerID)
    {
        winnerPanel.SetActive(toggle);

        string winMassage = winnerID == 0 ? "X Player Has Won" : "O Player Has Won";

        winnerText.text = winMassage;
    }


}
