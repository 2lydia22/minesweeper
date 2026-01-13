
public interface IGameService
{

    Board RevealCell(int row, int column);
    Board CreateGame();
    Board ToggleFlag(int row, int column);

}



