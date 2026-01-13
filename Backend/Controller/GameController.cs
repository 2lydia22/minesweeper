using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/games")]
public class GameController : Controller
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("create")]
    public IActionResult ResetGame()
    {
        var board = _gameService.CreateGame();
        return Ok(board);
    }

    [HttpPost("reveal")]
    public IActionResult RevealCell([FromBody] RevealCellRequest request)
    {
        var board = _gameService.RevealCell(request.Row, request.Column);
        return Ok(board);
    }

    [HttpPost("flag")]
    public IActionResult ToggleFlag([FromBody] RevealCellRequest request)
    {
        var board = _gameService.ToggleFlag(request.Row, request.Column);
        return Ok(board);
    }
    
    
    

    
}