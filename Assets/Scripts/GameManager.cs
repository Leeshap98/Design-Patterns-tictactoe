using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _playerTurn; // 1 = X, 2 = O
    private int[] playersWinScore = {0,0};
    enum Turn
    {
        X = 1,
        O
    }

    private void Awake()
    {
        Instance = this;
        _playerTurn = (int)Turn.X; 
    }

    private void Start()
    {
        for (int i = 0; i < playersWinScore.Length; i++)
        {
            playersWinScore[i] = PlayerPrefs.GetInt($"{i}Score");
            UIManager.Instance.UpdateWinScore(i);
        }

      
    }
    public void SwichTurns()
    {
        _playerTurn = _playerTurn == ((int)Turn.X) ? (int)Turn.O : (int)Turn.X;
        UIManager.Instance.ShowPlayerTurn();
    }

    public int GetPlayerTurn()
    {
        return _playerTurn;
    }

    public void AddWinScore(int playerID)
    {
        playersWinScore[playerID]++;
        PlayerPrefs.SetInt($"{playerID}Score", playersWinScore[playerID]);
    }

    public int GetPlayerWinScore(int playerID)
    {
        return playersWinScore[playerID]; 
    }
}