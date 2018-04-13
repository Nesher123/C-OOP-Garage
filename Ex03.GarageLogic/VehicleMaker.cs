using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleMaker
    {
        public static Vehicle MakeVehicle(eVehicleType i_VehicleType, List<object> i_Values)
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.ElectricBike:
                    {
                        vehicle = new ElectricBike(i_Values[0].ToString(), i_Values[1].ToString(), i_Values[4].ToString(),
                            (float)i_Values[5], (eLicenseType)i_Values[6], (int)i_Values[7]);
                        break;
                    }

                case eVehicleType.FuelBike:
                    {
                        vehicle = new FuelBike(i_Values[0].ToString(), i_Values[1].ToString(), i_Values[4].ToString(),
                            (float)i_Values[5], (eLicenseType)i_Values[6], (int)i_Values[7]);
                        break;
                    }

                case eVehicleType.ElectricCar:
                    {
                        vehicle = new ElectricCar(i_Values[0].ToString(), i_Values[1].ToString(), i_Values[4].ToString(),
                            (float)i_Values[5], (eCarColor)i_Values[6], (eNumOfDoors)i_Values[7]);
                        break;
                    }

                case eVehicleType.FuelCar:
                    {
                        vehicle = new FuelCar(i_Values[0].ToString(), i_Values[1].ToString(), i_Values[4].ToString(),
                            (float)i_Values[5], (eCarColor)i_Values[6], (eNumOfDoors)i_Values[7]);
                        break;
                    }

                case eVehicleType.Truck:
                    {
                        vehicle = new Truck(i_Values[0].ToString(), i_Values[1].ToString(), i_Values[4].ToString(),
                            (float)i_Values[5], (bool)i_Values[6], (float)i_Values[7]);
                        break;
                    }
            }

            return vehicle;
        }
    }
}