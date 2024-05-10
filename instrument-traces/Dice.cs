using OpenTelemetry.Trace;
using System;
using System.Diagnostics;

public class Dice
{
    public ActivitySource activitySource;
    private int min;
    private int max;

    public Dice(int min, int max, ActivitySource activitySource)
    {
        this.min = min;
        this.max = max;
        this.activitySource = activitySource;
    }

    public List<int> rollTheDice(int rolls)
    {
        List<int> results = new List<int>();
        
        using (var myActivity = activitySource.StartActivity("rollTheDice"))
        {
            for (int i = 0; i < rolls; i++)
            {
                results.Add(rollOnce());
            }

            return results;
        }
        
        // activity?.SetTag("http.method", "GET");
    }

    private int rollOnce()
    {
        
        using (var childActivity = activitySource.StartActivity("rollOnce"))
        {
            int result;
            
            try
            {
                result = Random.Shared.Next(min, max + 1);
                childActivity?.SetTag("dicelib.rolled", result);
            }
            catch (Exception ex)
            {
                childActivity?.SetStatus(ActivityStatusCode.Error, "Something bad happened!");
                childActivity?.RecordException(ex);
                throw;
            }

            return result;
        }
    }
}
