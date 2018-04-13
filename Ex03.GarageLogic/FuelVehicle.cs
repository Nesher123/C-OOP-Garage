namespace Ex03.GarageLogic
{
    class FuelVehicle : Vehicle
    {
        private eEnergyType m_EnergyType;
        private float m_CurrentFuelLevel;
        private readonly float r_MaximalFuelLevel;
        // $G$ DSN-001 (-3) Code duplication. except in Fuel type, Fuel and Electric Energy Sources are identical.

        public FuelVehicle(string i_ModelName, string i_LicenseNumber, int i_NumOfWheels,
            eEnergyType i_EnergyType, float i_MaximalFuelLevel)
            : base(i_ModelName, i_LicenseNumber, i_NumOfWheels)
        {
            m_EnergyType = i_EnergyType;
            m_CurrentFuelLevel = 0f;
            r_MaximalFuelLevel = i_MaximalFuelLevel;
        }

        public eEnergyType EnergyType
        {
            get { return m_EnergyType; }
            set { m_EnergyType = value; }
        }

        public float CurrentFuelLevel
        {
            get { return m_CurrentFuelLevel; }
            // $G$ DSN-005 (-5) The setter of this property should not have been public. Modification of the current energy level should be done in the refuel / recharge methods exclusively
            set { m_CurrentFuelLevel = value; }
        }

        public float MaximalFuelLevel
        {
            get { return r_MaximalFuelLevel; }
        }

        public void AddFuel(FuelVehicle i_FV, float i_LittersToAdd)
        {
            m_CurrentFuelLevel = AddEnergy(i_FV, r_MaximalFuelLevel, m_CurrentFuelLevel, i_LittersToAdd);
        }
    }
}