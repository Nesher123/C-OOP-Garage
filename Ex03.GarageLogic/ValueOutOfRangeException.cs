using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        readonly float r_MaxValue;
        readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue, string i_Message) : base(i_Message)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public float MaxValue
        {
            get { return r_MaxValue; }
        }

        public float MinValue
        {
            get { return r_MinValue; }
        }
    }
}