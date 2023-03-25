namespace Assets.Scripts.PartThree.Modules
{
    public class ModuleFactory
    {
        public IShipModule Create(ModuleConfig moduleConfig)
        {
            IShipModule result = null; 
            if (moduleConfig == null) { return result; }
            
            switch (moduleConfig.ModuleType)
            {
                case ModuleType.HpAmount:
                    result = new ShipModuleB(moduleConfig.Value);
                    break;
                case ModuleType.SheeldAmount:
                    result = new ShipModuleA(moduleConfig.Value);
                    break;
                case ModuleType.SheeldRestore:
                    result = new ShipModuleD(moduleConfig.Value);
                    break;
                case ModuleType.WeaponReload:
                    result = new ShipModuleC(moduleConfig.Value);
                    break;
                case ModuleType.WeaponDamage:
                case ModuleType.None:
                default:
                    break;
            }

            return result;
        }
    }
}