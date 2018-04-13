using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        private float m_EnergyPercentageRemaining;
        protected List<Wheel> m_Wheels;

        public Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumOfWheels)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            m_EnergyPercentageRemaining = 0f;
            m_Wheels = new List<Wheel>(i_NumOfWheels);
        }

        public string ModelName
        {
            get { return r_ModelName; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public float EnergyPercentageRemaining
        {
            get { return m_EnergyPercentageRemaining; }
            set { m_EnergyPercentageRemaining = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }
        
        public virtual float AddEnergy(Vehicle i_Vehicle, float i_MaximalValue, float i_CurrentValue, float i_QuantityToAdd)
        {
            if(i_CurrentValue + i_QuantityToAdd <= i_MaximalValue)
            {
                i_CurrentValue += i_QuantityToAdd;
                i_Vehicle.EnergyPercentageRemaining = i_CurrentValue / i_MaximalValue;

                return i_CurrentValue;
            }
            else
            {
                throw new ValueOutOfRangeException(i_MaximalValue, 0, "Exceeds Max Capacity");
            }
        }
    }
}