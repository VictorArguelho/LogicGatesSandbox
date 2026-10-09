    using System;
    using System.Collections.Generic;

    namespace LogicGatesSandbox.Engine.Time;

    public static class FPS
    {
        private static int _maxAverageSeconds;
        public static int MaxAverageSeconds
        {
            get => _maxAverageSeconds;
            set => _maxAverageSeconds = Math.Max(value, 0);
        }

        public static int AverageFPSiLastSecond =>
            (int)Math.Round(AverageFPSdLastSecond);

        public static float AverageFPSfLastSecond =>
            (float)AverageFPSdLastSecond;

        public static double AverageFPSdLastSecond { get; private set; }

        public static int FPSi => (int)Math.Round(FPSd);
        public static float FPSf => (float)FPSd;
        public static double FPSd { get; private set; }

        private static double _timeSinceLastAverageSet;
        private static int _framesSinceLastAverageSet;

        private static readonly List<double> _averageFPSdLastsSeconds = [];

        internal static void Update()
        {
            FPSd = AppTime.DeltaTimeD > 0.0
                ? 1.0 / AppTime.DeltaTimeD
                : 0.0;

            _framesSinceLastAverageSet++;
            _timeSinceLastAverageSet += AppTime.DeltaTimeD;

            if (_timeSinceLastAverageSet >= 1.0)
                SetLastSecondAverage();
        }

        public static int GetFPSiAverage(int seconds) =>
            (int)Math.Round(GetFPSdAverage(seconds));

        public static float GetFPSfAverage(int seconds) =>
            (float)GetFPSdAverage(seconds);

        public static double GetFPSdAverage(int seconds)
        {
            if (seconds <= 0 || seconds > _averageFPSdLastsSeconds.Count)
                return 0.0;

            var sum = 0.0;
            var lastIndex = _averageFPSdLastsSeconds.Count - 1;

            for (int i = lastIndex; i > lastIndex - seconds; i--)
                sum += _averageFPSdLastsSeconds[i];

            return sum / seconds;
        }

        private static void SetLastSecondAverage()
        {
            AverageFPSdLastSecond =
                _framesSinceLastAverageSet / _timeSinceLastAverageSet;

            _framesSinceLastAverageSet = 0;
            _timeSinceLastAverageSet -= 1.0;

            if (_averageFPSdLastsSeconds.Count >= MaxAverageSeconds)
                _averageFPSdLastsSeconds.RemoveAt(0);

            _averageFPSdLastsSeconds.Add(AverageFPSdLastSecond);
        }
    }