public class GameService : IGameService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GameService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session =>
        _httpContextAccessor.HttpContext!.Session;

    public const string SessionKey = "GameSession";

    public Board RevealCell(int row, int column)
    {
        // Implementation for revealing a cell
        var board = Session.GetObject<Board>(SessionKey);
        if (board == null)
        {
            board = new Board(16, 16);
            Session.SetObject(SessionKey, board);
        }

        var cell = board.Cells[row][column];

        if (!board.MinesPlaced)
        {
            board.PlaceMines(row, column);
            board.MinesPlaced = true;
        }

        if (cell.IsRevealed || cell.IsFlagged)
        {
            return board;
        }

        if (cell.AdjacentMines == 0 && !cell.HasMine)
        {
            FloodFill(board, row, column);
        }
        else
        {
            cell.IsRevealed = true;
            board.CellsRevealed++;

            if (cell.HasMine)
            {
                board.Status = GameStatus.Lost.ToString();
                Session.SetObject(SessionKey, board);
            }

        }

        if (board.CellsRevealed == board.TotalCells - Board.TotalMines)
        {
            board.Status = GameStatus.Won.ToString();
            Session.SetObject(SessionKey, board);
        }

        Session.SetObject(SessionKey, board);
        return board;
    }

    private void FloodFill(Board board, int row, int column)
    {

        if (row < 0 || row >= board.Rows || column < 0 || column >= board.Columns)
        {
            return;
        }

        var cell = board.Cells[row][column];

        if (cell.IsRevealed || cell.IsFlagged)
        {
            return;
        }

        cell.IsRevealed = true;
        board.CellsRevealed++;

        if (cell.AdjacentMines == 0 && !cell.HasMine)
        {
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0)
                        continue;

                    FloodFill(board, row + dr, column + dc);
                }
            }
        }
    }

    public Board CreateGame()
    {
        var board = new Board(16, 16);


        Session.SetObject(SessionKey, board);
        return board;
    }

    public Board ToggleFlag(int row, int column)
    {
        var board = Session.GetObject<Board>(SessionKey);
        if (board == null)
        {
            board = new Board(16, 16);
            Session.SetObject(SessionKey, board);
        }

        if (board.Status == GameStatus.Won.ToString() || board.Status == GameStatus.Lost.ToString())
        {
            return board;
        }

        var cell = board.Cells[row][column];
        if (cell.IsRevealed)
        {
            return board;
        }

        cell.IsFlagged = !cell.IsFlagged;

        Session.SetObject(SessionKey, board);
        return board;
    }



   
}