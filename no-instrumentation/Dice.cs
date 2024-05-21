using System;
using System.Diagnostics;

public class Dice
{
    private int min;
    private int max;

    public Dice(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public List<int> rollTheDice(int rolls)
    {
        List<int> results = new List<int>();

        for (int i = 0; i < rolls; i++)
        {
            results.Add(rollOnce());
        }

        return results;
    }

    private int rollOnce()
    {
        int result;
            
        try
        {
            result = Random.Shared.Next(min, max + 1);
        }
        catch (Exception ex)
        {
            throw;
        }

        return result;
    }
}
