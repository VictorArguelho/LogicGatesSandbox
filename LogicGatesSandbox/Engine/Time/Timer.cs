using System;
using System.Collections.Generic;

namespace LogicGatesSandbox.Engine.Time;

public class Timer : IDisposable
{
    private static readonly List<Timer> _timers = [];

    internal static void Update()
    {
        for (int i = _timers.Count - 1; i >= 0; i--)
            _timers[i].InternalUpdate();
    }

    private double _duration;
    public double Duration
    {
        get => _duration;

        set => _duration = Math.Max(value, 0.0);
    }

    public bool Loop { get; set; }

    public double ElapsedTime { get; private set; }

    public double ElapsedRatio => Duration > 0.0
        ? Math.Clamp(ElapsedTime / Duration, 0.0, 1.0)
        : 1.0;

    public event Action? OnEnd;

    public Timer(Action onEnd, double duration, bool loop = false)
    {
        ArgumentNullException.ThrowIfNull(onEnd);

        Duration = duration;
        Loop = loop;
        OnEnd += onEnd;

        _timers.Add(this);
    }

    private void InternalUpdate()
    {
        ElapsedTime += AppTime.DeltaTimeD;

        if (ElapsedTime < Duration)
            return;

        End();
    }

    private void End()
    {
        if (Loop)
            if (Duration == 0.0)
                ElapsedTime = 0.0;
            else
                ElapsedTime %= Duration;
        else
            Dispose();

        OnEnd?.Invoke();
    }

    public void Dispose() =>
        _timers.Remove(this);
}