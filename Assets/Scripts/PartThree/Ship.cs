using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.PartThree.Characteristics;
using Assets.Scripts.PartThree.Modules;
using Assets.Scripts.PartThree.Weapons;

namespace Assets.Scripts.PartThree
{
    public class Ship
    {
        public event Action<string, int, int> OnCurrentHpChanged;
        public event Action<string, int, int> OnCurrentSheeldChanged;
        public event Action<string, int, int> OnSheeldRestoreChanged;
        public event Action<HpChangeData> OnShipFired;
        
        private CharacteristicContainer _characteristicContainer = new CharacteristicContainer();
        private List<IShipModule> _modules = new List<IShipModule>();
        private List<IShipWeapon> _weapons = new List<IShipWeapon>();

        private List<TimerData> timers = new List<TimerData>();
        private bool _isUpdateActive = true;
        private bool _isPaused = false;

        private string _id;
        
        private int _currentHp;
        private int _currentSheeld;

        public string Id => _id;
        
        public int CurrentHp
        {
            get { return _currentHp; }
            private set
            {
                int oldValue = _currentHp;
                _currentHp = value;
                OnCurrentHpChanged?.Invoke(Id, oldValue, _currentHp);
            }
        }

        public int CurrentSheeld
        {
            get { return _currentSheeld; }
            private set
            {
                int oldValue = _currentSheeld;
                _currentSheeld = value;
                OnCurrentSheeldChanged?.Invoke(Id, oldValue, _currentSheeld);
            }
        }

        public bool IsPaused
        {
            get { return _isPaused; }
            set { _isPaused = value; }
        }

        public CharacteristicContainer CharacteristicContainer => _characteristicContainer;
        public IReadOnlyList<IShipModule> Modules => _modules;
        public IReadOnlyList<IShipWeapon> Weapons => _weapons;
        
        public bool IsAlive => _currentHp >= 0;

        public Ship(CharacteristicShipBase characteristicShipBase)
        : this (new CharacteristicShipWrapper(characteristicShipBase)) { }
        
        public Ship(CharacteristicShipWrapper characteristicShipWrapper)
        {
            _characteristicContainer.Add(characteristicShipWrapper);

            characteristicShipWrapper.OnHpAmountChanged += OnHpAmountChangedHandler;
            characteristicShipWrapper.OnSheeldAmountChanged += OnSheeldAmountChangedHandler;
            characteristicShipWrapper.OnSheeldRestoreChanged += OnSheeldRestoreChangedHandler;
            characteristicShipWrapper.OnModuleCountChanged += OnModuleCountChangedHandler;
            characteristicShipWrapper.OnWeaponCountChanged += OnWeaponCountChangedHandler;

            _id = this.GetHashCode().ToString(); 
            
            _currentHp = characteristicShipWrapper.HpAmount;
            _currentSheeld = characteristicShipWrapper.SheeldAmount;

            TimerData timerData = new TimerData();
            timerData.Id = "SheeldRestore";
            timerData.Delay = 1f/(float)characteristicShipWrapper.SheeldRestore/100f;
            timers.Add(timerData);
            
            Update();
        }

        ~Ship()
        {
            _isUpdateActive = false;
            
            List<IShipModule> modulesCopy = new List<IShipModule>(_modules);
            for (int i = 0; i < modulesCopy.Count; i++)
            {
                RemoveModule(modulesCopy[i].Id);
            }
            
            List<IShipWeapon> weaponsCopy = new List<IShipWeapon>(_weapons);
            for (int i = 0; i < weaponsCopy.Count; i++)
            {
                RemoveWeapon(weaponsCopy[i].Id);
            }
            
            CharacteristicShipWrapper characteristicShipWrapper = _characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship);
            if (characteristicShipWrapper == null) { return; }

            characteristicShipWrapper.OnHpAmountChanged -= OnHpAmountChangedHandler;
            characteristicShipWrapper.OnSheeldAmountChanged -= OnSheeldAmountChangedHandler;
            characteristicShipWrapper.OnSheeldRestoreChanged -= OnSheeldRestoreChangedHandler;
            characteristicShipWrapper.OnModuleCountChanged -= OnModuleCountChangedHandler;
            characteristicShipWrapper.OnWeaponCountChanged -= OnWeaponCountChangedHandler;
        }

        public void ProcessIncomingHpChange(HpChangeData hpChangeData)
        {
            if (hpChangeData.IsDamage)
            {
                if (CurrentSheeld > 0)
                {
                    if (CurrentSheeld > hpChangeData.HpChangeValue)
                    {
                        CurrentSheeld -= hpChangeData.HpChangeValue;    
                    }
                    else
                    {
                        int hpDamage = hpChangeData.HpChangeValue - CurrentSheeld;  
                        CurrentHp -= hpDamage;
                    }
                }
                else
                {
                    CurrentHp -= hpChangeData.HpChangeValue;                    
                }
            }
        }
        
        public void AddModule(IShipModule shipModule)
        {
            if (shipModule == null) { return; }

            CharacteristicShipWrapper characteristicShipWrapper = _characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship);
            if (characteristicShipWrapper == null || _modules.Count >= characteristicShipWrapper.ModuleCount) { return; }
            
            shipModule.OnAdd(_characteristicContainer);
            _modules.Add(shipModule);
            _characteristicContainer.Add(shipModule.Characteristic);
            
            //TODO: may add repeat update if some module is made changes  
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].OnUpdate(_characteristicContainer);
            }
        }
        
        public void RemoveModule(string id)
        {
            if (String.IsNullOrEmpty(id)) { return; }

            IShipModule shipModule = null;
            for (int i = 0; i < _modules.Count; i++)
            {
                if (_modules[i].Id.Equals(id))
                {
                    shipModule = _modules[i];
                    break;
                }
            }

            if (shipModule == null) { return; }

            _modules.Remove(shipModule);
            _characteristicContainer.Remove(shipModule.Characteristic);
            shipModule.OnRemove(_characteristicContainer);
            //TODO: may add repeat update if some module is made changes  
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].OnUpdate(_characteristicContainer);
            }
        }
        
        public void AddWeapon(IShipWeapon shipWeapon)
        {
            if (shipWeapon == null) { return; }

            //TODO: optimize get
            if (_weapons.Count == (_characteristicContainer.GetAll(CharacteristicType.Ship)[0] as CharacteristicShipWrapper).WeaponCount) { return; }
            
            _weapons.Add(shipWeapon);
            _characteristicContainer.Add(shipWeapon.Characteristic);
            shipWeapon.Characteristic.OnDamageChanged += OnDamageChangedHandler;
            shipWeapon.Characteristic.OnReloadChanged += OnReloadChangedHandler;
            
            //TODO: may add repeat update if some module is made changes  
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].OnUpdate(_characteristicContainer);
            }

            TimerData timerData = new TimerData();
            timerData.Id = shipWeapon.Id.ToString();
            timerData.Delay = shipWeapon.Characteristic.Reload;
            timers.Add(timerData);
        }

        public void RemoveWeapon(string id)
        {
            if (String.IsNullOrEmpty(id)) { return; }

            IShipWeapon shipWeapon = null;
            for (int i = 0; i < _weapons.Count; i++)
            {
                if (_weapons[i].Id.Equals(id))
                {
                    shipWeapon = _weapons[i];
                    break;
                }
            }

            if (shipWeapon == null) { return; }

            _weapons.Remove(shipWeapon);
            _characteristicContainer.Remove(shipWeapon.Characteristic);
            
            //TODO: may add repeat update if some module is made changes  
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].OnUpdate(_characteristicContainer);
            }

            TimerData timerData = null;
            for (int i = 0; i < timers.Count; i++)
            {
                if (timers[i].Id.Equals(shipWeapon.GetHashCode().ToString()))
                {
                    timerData = timers[i];
                    break;
                }
            }

            if (timerData != null)
            {
                timers.Remove(timerData);
            }
        }

        private async void Update()
        {
            while (_isUpdateActive)
            {
                if (IsPaused)
                {
                    await Task.Yield();
                    continue;
                }
                
                for (int i = 0; i < timers.Count; i++)
                {
                    if (timers[i].Last.AddSeconds(timers[i].Delay) < DateTime.Now)
                    {
                        timers[i].Last = DateTime.Now;
                        OnTimerFire(timers[i]);
                    }
                }
                await Task.Yield();
            }
        }

        private void OnHpAmountChangedHandler(int oldValue, int newValue)
        {
            int delta = newValue - oldValue;
            CurrentHp += delta;
            CurrentHp = CurrentHp <= 0 ? 1 : _currentHp;
        }
     
        private void OnSheeldAmountChangedHandler(int oldValue, int newValue)
        {
            int delta = newValue - oldValue;
            CurrentSheeld += delta;
            CurrentSheeld = CurrentSheeld <= 0 ? 1 : CurrentSheeld;
        }

        private void OnSheeldRestoreChangedHandler(int oldValue, int newValue)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                if (timers[i].Id.Equals("SheeldRestore"))
                {
                    timers[i].Delay = 1f/(float)newValue/100f;
                    break;
                }
            }
            
            OnSheeldRestoreChanged?.Invoke(Id, oldValue, newValue);   
        }
        
        private void OnModuleCountChangedHandler(int oldValue, int newValue)
        {
            if (oldValue <= newValue) { return; }

            if (_modules.Count > newValue)
            {
                List<IShipModule> modulesCopy = new List<IShipModule>(_modules);
                for (int i = newValue; i < modulesCopy.Count; i++)
                {
                    RemoveModule(modulesCopy[i].Id);
                }
            }
        }
        
        private void OnWeaponCountChangedHandler(int oldValue, int newValue)
        {
            if (oldValue <= newValue) { return; }

            if (_weapons.Count > newValue)
            {
                List<IShipWeapon> weaponsCopy = new List<IShipWeapon>(_weapons);
                for (int i = newValue; i < weaponsCopy.Count; i++)
                {
                    RemoveWeapon(weaponsCopy[i].Id);
                }
            }
        }
        
        private void OnDamageChangedHandler(CharacteristicWeaponWrapperBase weaponCharacteristicWrapper, int newValue)
        {
            //do nothing
        }

        private void OnReloadChangedHandler(CharacteristicWeaponWrapperBase weaponCharacteristicWrapper, int newValue)
        {
            for (int i = 0; i < _weapons.Count; i++)
            {
                bool isUpdated = false;
                if (_weapons[i].Characteristic == weaponCharacteristicWrapper)
                {
                    for (int j = 0; j < timers.Count; j++)
                    {
                        if (timers[j].Id == _weapons[i].GetHashCode().ToString())
                        {
                            timers[j].Delay = weaponCharacteristicWrapper.Reload;
                            isUpdated = true;
                            break;
                        }
                    }

                    if (isUpdated) { break; }
                }
            }
        }
        
        private void OnTimerFire(TimerData timerData)
        {
            if (timerData.Id.Equals("SheeldRestore"))
            {
                OnSheeldRestore();
            }
            else
            {
                //At the moment we believe that the timers are only weapons, when other types of timers appear, the system will need to be completed
                OnWeaponTimerTriggered(timerData);
            }
        }

        private void OnSheeldRestore()
        {
            CharacteristicShipWrapper characteristicShipWrapper = _characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship);
            if (_currentSheeld < characteristicShipWrapper.SheeldAmount) { CurrentSheeld++; }
        }

        private void OnWeaponTimerTriggered(TimerData timerData)
        {
            for (int i = 0; i < _weapons.Count; i++)
            {
                if (_weapons[i].Id.Equals(timerData.Id))
                {
                    HpChangeData hpChangeData = new HpChangeData(_weapons[i].Characteristic.Damage, true, Id);
                    OnShipFired?.Invoke(hpChangeData);
                    break;
                }
            }
            CharacteristicShipWrapper characteristicShipWrapper = _characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship);
            if (_currentSheeld < characteristicShipWrapper.SheeldAmount) { CurrentSheeld++; }
        }
    }
}