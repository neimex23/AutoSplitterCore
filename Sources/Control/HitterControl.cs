using HitCounterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public class HitterControl
    {
        public static SaveModule _saveModule;
        private ISplitterControl _splitterControl = SplitterControl.GetControl();

        #region Singleton
        private static readonly Lazy<HitterControl> instance = new Lazy<HitterControl>(() => new HitterControl());

        private HitterControl() {}

        public static HitterControl GetControl() => instance.Value;
        #endregion

        #region HollowKnight
        private CancellationTokenSource _ctsHollow;
        public void StartHollow()
        {
            if (_ctsHollow == null || _ctsHollow.IsCancellationRequested)
            {
                _ctsHollow = new CancellationTokenSource();
                Task.Run(() => CheckingHollow(_ctsHollow.Token), _ctsHollow.Token);
            }
        }
        public void StopHollow()
        {
            _ctsHollow?.Cancel();
        }
    
        private async Task CheckingHollow(CancellationToken token)
        {
            try
            {
                int lastHealth = HollowSplitter.GetIntance().GetHealth();

                while (!token.IsCancellationRequested)
                {
                    if (!_saveModule.generalAS.AutoHitHollow) StopHollow();
                    await Task.Delay(1000, token);

                    int currentHealth = HollowSplitter.GetIntance().GetHealth();
                    if (currentHealth < lastHealth && currentHealth != -1)
                    {
                        _splitterControl.HitCheck("HitFlag produced on Hollow Knight");
                    }

                    lastHealth = currentHealth;
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"Error in CheckingHollow: {ex.Message}");
            }
        }
        #endregion

        #region Celeste
        private CancellationTokenSource _ctsCeleste;
        public void StartCeleste()
        {
            if (_ctsCeleste == null || _ctsCeleste.IsCancellationRequested)
            {
                _ctsCeleste = new CancellationTokenSource();
                Task.Run(() => CheckingCeleste(_ctsCeleste.Token), _ctsCeleste.Token);
            }
        }
        public void StopCeleste()
        {
            _ctsCeleste?.Cancel();
        }
        private async Task CheckingCeleste(CancellationToken token)
        {
            try
            {
                int lastDeaths = CelesteSplitter.GetIntance().GetDeaths();

                while (!token.IsCancellationRequested)
                {
                    if (!_saveModule.generalAS.AutoHitHollow) StopHollow();
                    await Task.Delay(1000, token);

                    int currentDeaths = CelesteSplitter.GetIntance().GetDeaths();
                    //DebugLog.LogMessage($"Death Celeste: {currentDeaths}");
                    if (currentDeaths < lastDeaths)
                    {
                        _splitterControl.HitCheck("HitFlag produced on Celeste");
                    }

                    lastDeaths = currentDeaths;
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"Error in CheckingHollow: {ex.Message}");
            }
        }
        #endregion
    }
}
