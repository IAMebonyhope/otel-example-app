using System.Diagnostics;
using System.Diagnostics.Metrics;

/// <summary>
/// It is recommended to use a custom type to hold references for ActivitySource. 
/// This avoids possible type collisions with other components in the DI container.
/// </summary>
public class Instrumentation : IDisposable
{
    internal const string Version = "1.0.0";
    internal const string ActivitySourceName = "dice-server";
    internal const string MeterName = "dice-server";

    public Instrumentation()
    {
        this.ActivitySource = new ActivitySource(ActivitySourceName, Version);
        this.Meter = new Meter(MeterName, Version);
    }

    public ActivitySource ActivitySource { get; }

    public Meter Meter { get; }
    
    public void Dispose()
    {
        this.ActivitySource.Dispose();
    }
}