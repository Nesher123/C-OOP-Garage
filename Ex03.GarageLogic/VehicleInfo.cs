namespace Ex03.GarageLogic
{
    public class VehicleInfo
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_Status;

        public VehicleInfo(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Status = eVehicleStatus.InProgress;
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set { m_OwnerPhoneNumber = value; }
        }

        public eVehicleStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
    }
}