namespace LogicGatesSandbox.Engine.Time;

public static class AppTime
{
    public static ulong FrameCount { get; private set; }

    public static float DeltaTimeF => (float)DeltaTimeD;
    public static double DeltaTimeD { get; private set; }

    public static float ElapsedTimeF => (float)ElapsedTimeD;
    public static double ElapsedTimeD { get; private set; }

    internal static void Update(double deltaTime)
    {
        FrameCount++;

        DeltaTimeD = deltaTime;
        ElapsedTimeD += DeltaTimeD;
    }
}