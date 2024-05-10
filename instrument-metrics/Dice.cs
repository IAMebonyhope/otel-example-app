using OpenTelemetry.Trace;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;

public class Dice
{
    public ActivitySource activitySource;
    public Meter meter;
    private int min;
    private int max;

    public Dice(int min, int max, ActivitySource activitySource, Meter meter)
    {
        this.min = min;
        this.max = max;
        this.activitySource = activitySource;
        this.meter = meter;
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
        
    }

    private int rollOnce()
    {
        Counter<long> counter = meter.CreateCounter<long>("dice-rolled", description: "How many times a value was rolled.");
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

            counter.Add(1, new KeyValuePair<string, object?>("dice-value", result.ToString()));
            return result;
        }
    }
}
