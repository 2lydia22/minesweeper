public class Board 
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public Cell[][] Cells { get; set; }
    public int TotalCells => Rows * Columns;
    public const int TotalMines = 40; // Example for a standard 16x16 board
    public bool MinesPlaced { get; set; }
    public int CellsRevealed { get; set; } = 0;
    public string Status { get; set; } = GameStatus.NotStarted.ToString();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }


    public Board(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Status = GameStatus.NotStarted.ToString();
        StartTime = DateTime.Now;

        Cells = new Cell[rows][];
        for (int r = 0; r < rows; r++)
        {
            Cells[r] = new Cell[columns];
            for (int c = 0; c < columns; c++)
            {
                Cells[r][c] = new Cell();
            }
        }
    }

    public void CalculateAdjacentMines()
    {
        // Implementation for calculating adjacent mines for each cell
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (Cells[r][c].HasMine)
                {
                    continue;
                }

                int count = 0;

                for (int dr = -1; dr <= 1; dr++)
                {
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        if (dr == 0 && dc == 0)
                            continue;

                        int nr = r + dr;
                        int nc = c + dc;

                        if (nr < 0 || nr >= Rows || nc < 0 || nc >= Columns)
                            continue;

                        if (Cells[nr][nc].HasMine)
                            count++;
                    }
                }

                Cells[r][c].AdjacentMines = count;
            }
        }
        
    }

    public void PlaceMines(int row, int column)
    {
        // Implementation for placing mines on the board, avoiding the first clicked cell at (row, column)
       
        int placedMines = 0;
        var rand = new Random();

        if (MinesPlaced == true)
        {
            return;
        }

        while (placedMines < TotalMines)
        {

            int r = rand.Next(0, Rows);
            int c = rand.Next(0, Columns);

            // Avoid placing a mine on the first clicked cell
            if ((r == row && c == column) || Cells[r][c].HasMine)
            {
                continue;
            }

            Cells[r][c].HasMine = true;
            placedMines++;
        }

    
        CalculateAdjacentMines();

    }
}