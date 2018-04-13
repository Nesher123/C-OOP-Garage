namespace Ex03.GarageLogic
{
    class Truck : FuelVehicle
    {
        private const float k_MaxWheelAirPressure = 32f;
        private const float k_MaxFuelLevel = 135f;
        private const int k_NumOfWheels = 12;
        private bool m_IsCarryingHazards;
        private float m_MaxLoadAllowed;

        public Truck(string i_ModelName, string i_LicenseNumber, string i_WheelManufacturerName, float i_CurrentWheelPressure,
            bool i_IsCarryingHazards, float i_MaxLoadAllowed) :
            base(i_ModelName, i_LicenseNumber, k_NumOfWheels, eEnergyType.Octan96, k_MaxFuelLevel)
        {
            m_MaxLoadAllowed = i_MaxLoadAllowed;
            m_IsCarryingHazards = i_IsCarryingHazards;

            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_WheelManufacturerName, i_CurrentWheelPressure, k_MaxWheelAirPressure));
            }
        }

        public float MaxLoadAllowed
        {
            get { return m_MaxLoadAllowed; }
        }

        public bool IsCarryingHazards
        {
            get { return m_IsCarryingHazards; }
            set { m_IsCarryingHazards = value; }
        }
    }
}
