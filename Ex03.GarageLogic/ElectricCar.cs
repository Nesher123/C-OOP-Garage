namespace Ex03.GarageLogic
{
    class ElectricCar : ElectricVehicle
    {
        private readonly eCarColor r_CarColor;
        private readonly eNumOfDoors r_NumOfDoors;
        private const float k_MaxWheelAirPressure = 30f;
        private const float k_MaxBatteryTime = 2.5f;
        private const int k_NumOfWheels = 4;

        // $G$ DSN-011 (-5) No use internal constructors.
        public ElectricCar(string i_ModelName, string i_LicenseNumber, string i_WheelManufacturerName,
            float i_CurrentWheelPressure, eCarColor i_CarColor, eNumOfDoors i_NumOfDoors) :
            base(i_ModelName, i_LicenseNumber, k_NumOfWheels, k_MaxBatteryTime)
        {
            r_CarColor = i_CarColor;
            r_NumOfDoors = i_NumOfDoors;
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_WheelManufacturerName, i_CurrentWheelPressure, k_MaxWheelAirPressure));
            }
        }

        public eCarColor CarColor
        {
            get { return r_CarColor; }
        }

        public eNumOfDoors NumOfDoors
        {
            get { return r_NumOfDoors; }
        }
    }
}