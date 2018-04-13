namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }
        
        public void PumpWheel(float i_AirToAdd)
        {
            if (CurrentAirPressure + i_AirToAdd <= MaxAirPressure)
            {
                CurrentAirPressure += i_AirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(MaxAirPressure, 0, "Exceeds Capacity");
            }
        }
    }
}