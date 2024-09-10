public class DrawSimbolCommand : ICommand
{
    private int cellNumber;
    private int playerTurn;
    private Grid grid;

    public DrawSimbolCommand(int cellNumber, int playerTurn, Grid grid)
    {
        this.cellNumber = cellNumber;
        this.playerTurn = playerTurn;
        this.grid = grid;
    }

    public void Execute()
    {
        grid.MarkCellWithPlayerID(cellNumber);
        UIManager.Instance.PlaceXorO(cellNumber);
        GameManager.Instance.SwichTurns();
    }
    public void Undo()
    {
        grid.UnMarkCellWithPlayerID(cellNumber);
        UIManager.Instance.UndoPlaceXorO(cellNumber);
        GameManager.Instance.SwichTurns();
    }
}
