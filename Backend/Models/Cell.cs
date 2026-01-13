public class Cell
{
    public int Row {get; set;}
    public int Column {get; set;}

    public bool IsRevealed {get; set;} = false;
    public bool IsFlagged {get; set;} = false;
    public bool HasMine {get; set;} = false;
    public int AdjacentMines {get; set;}
}