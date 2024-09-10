using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CommandManager commandManager = new CommandManager();

    public void PlayerPressOnCell(int cellNumber)
    {
        int playerTurn = GameManager.Instance.GetPlayerTurn();
        ICommand drawSimbolCommand = new DrawSimbolCommand(cellNumber, playerTurn, Grid.Instance);
        commandManager.ExecuteCommand(drawSimbolCommand);

        Grid.Instance.CheckGridWinner();
    }

    public void UndoMove()
    {
        commandManager.Undo();
    }

    public void RedoMove()
    {
        commandManager.Redo();
    }
}
