using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class DiceController : ControllerBase
{
    private ILogger<DiceController> logger;

    private ActivitySource activitySource;
    private Meter meter;

    public DiceController(ILogger<DiceController> logger, Instrumentation instrumentation)
    {
        this.logger = logger;
        this.activitySource = instrumentation.ActivitySource;
        this.meter = instrumentation.Meter;
    }

    [HttpGet("/rolldice")]
    public List<int> RollDice(string player, int? rolls)
    {
        List<int> result = new List<int>();
        using (var myActivity = activitySource.StartActivity("StartDice"))
        {
            myActivity?.SetTag("foo", 1);
            if (!rolls.HasValue)
            {
                logger.LogError("Missing rolls parameter");
                throw new HttpRequestException("Missing rolls parameter", null, HttpStatusCode.BadRequest);
            }

            result = new Dice(1, 6, activitySource, meter).rollTheDice(rolls.Value);

            if (string.IsNullOrEmpty(player))
            {
                logger.LogInformation("Anonymous player is rolling the dice: {result}", result);
            }
            else
            {
                logger.LogInformation("{player} is rolling the dice: {result}", player, result);
            }
        }

        return result;
    }
}

