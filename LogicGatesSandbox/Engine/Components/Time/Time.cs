namespace LogicGatesSandbox.Engine.Components.Time;

public static class Time
{
    public static ulong FrameCount { get; private set; }

    public static float DeltaTimeF => (float)DeltaTimeD;
    public static double DeltaTimeD { get; private set; }

    public static float ElapsedTimeF => (float)ElapsedTimeD;
    public static double ElapsedTimeD { get; private set; }

    public static void Update(double deltaTime)
    {
        FrameCount++;

        DeltaTimeD = deltaTime;
        ElapsedTimeD += DeltaTimeD;
    }
}