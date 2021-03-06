﻿namespace Ex03.GarageLogic
{
    class FuelBike : FuelVehicle
    {
        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineCcVolume;
        private const float k_MaxWheelAirPressure = 33f;
        private const float k_MaxFuelLevel = 5.5f;
        private const int k_NumOfWheels = 2;

        public FuelBike(string i_ModelName, string i_LicenseNumber,
        string i_WheelManufacturerName, float i_CurrentWheelPressure, eLicenseType i_LicenseType, int i_EngineCCVolume) : 
        base(i_ModelName, i_LicenseNumber, k_NumOfWheels, eEnergyType.Octan95, k_MaxFuelLevel)
        {
            r_LicenseType = i_LicenseType;
            r_EngineCcVolume = i_EngineCCVolume;
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_WheelManufacturerName, i_CurrentWheelPressure, k_MaxWheelAirPressure));
            }
        }

        public int EngineCCVolume
        {
            get { return r_EngineCcVolume; }
        }

        public eLicenseType LicenseType
        {
            get { return r_LicenseType; }
        }
    }
}