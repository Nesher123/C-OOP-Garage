namespace Ex03.GarageLogic
{
    abstract class ElectricVehicle : Vehicle
    {
        private float m_RemainingBatteryTime;
        private readonly float r_MaximalBatteryTime;
        private readonly eEnergyType r_EnergyType;

        public ElectricVehicle(string i_ModelName, string i_LicenseNumber, 
            int i_NumOfWheels, float i_MaximalBatteryTime) : 
            base(i_ModelName, i_LicenseNumber, i_NumOfWheels)
        {
            m_RemainingBatteryTime = 0f;
            r_MaximalBatteryTime = i_MaximalBatteryTime;
            r_EnergyType = eEnergyType.Electricity;
        }

        public float RemainingBatteryTime
        {
            get { return m_RemainingBatteryTime; }
            set { m_RemainingBatteryTime = value; }
        }

        public float MaximalBatteryTime
        {
            get { return r_MaximalBatteryTime; }
        }

        public eEnergyType EnergyType
        {
            get { return r_EnergyType; }
        }

        public void ChargeBattery(ElectricVehicle i_Ev, float i_HoursToCharge)
        {
            RemainingBatteryTime = AddEnergy(i_Ev, r_MaximalBatteryTime, RemainingBatteryTime, i_HoursToCharge);
        }
    }
}