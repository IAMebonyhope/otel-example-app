using Microsoft.AspNetCore.Mvc;
using System.Net;

public class DiceController : ControllerBase
{
    public DiceController()
    {
    }

    [HttpGet("/rolldice")]
    public List<int> RollDice(string player, int? rolls)
    {
        List<int> result = new List<int>();

        if (!rolls.HasValue)
        {
            throw new HttpRequestException("Missing rolls parameter", null, HttpStatusCode.BadRequest);
        }

        result = new Dice(1, 6).rollTheDice(rolls.Value);
        
        return result;
    }
}

