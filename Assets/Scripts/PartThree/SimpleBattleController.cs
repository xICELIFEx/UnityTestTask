using System;

namespace Assets.Scripts.PartThree
{
    public class SimpleBattleController
    {
        public event Action<Ship> OnEndBattle;
        
        private Ship _a;
        private Ship _b;

        public void StartBattle(Ship a, Ship b)
        {
            _a = a;
            _a.OnShipFired += OnShipFiredHandler;
            _a.OnCurrentHpChanged += OnCurrentHpChangedHandler;

            _b = b;
            _b.OnShipFired += OnShipFiredHandler;
            _b.OnCurrentHpChanged += OnCurrentHpChangedHandler;
            
            _a.IsPaused = false;
            _b.IsPaused = false;
        }
        
        public void EndBattle()
        {
            Ship winner = _a.IsAlive ? _a : _b.IsAlive ? _b : null;
            OnEndBattle?.Invoke(winner);
            
            _a.OnShipFired -= OnShipFiredHandler;
            _a.OnCurrentHpChanged -= OnCurrentHpChangedHandler;
            _a = null;
            
            _b.OnShipFired -= OnShipFiredHandler;
            _b.OnCurrentHpChanged -= OnCurrentHpChangedHandler;
            _b = null;
        }
        
        private void OnShipFiredHandler(HpChangeData hpChangeData)
        {
            if (_a.Id.Equals(hpChangeData.SourceId))
            {
                _b.ProcessIncomingHpChange(hpChangeData);
            }
            else
            {
                _a.ProcessIncomingHpChange(hpChangeData);
            }
        }
        
        private void OnCurrentHpChangedHandler(string id, int oldValue, int newValue)
        {
            if (newValue > 0) { return; }

            EndBattle();
        }
    }
}