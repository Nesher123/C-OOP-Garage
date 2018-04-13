using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        // $G$ DSN-999 (-3) This Dictionary should be readonly.
        private Dictionary<Vehicle, VehicleInfo> m_GarageVehicles;

        public GarageManager()
        {
            m_GarageVehicles = new Dictionary<Vehicle, VehicleInfo>();
        }

        public Dictionary<Vehicle, VehicleInfo> GarageVehicles
        {
            get { return m_GarageVehicles; }
        }

        /*The system will supply the user with the following functions:
         * 
        1. “Insert” a new vehicle into the garage. The user will be asked to select a
        vehicle type out of the supported vehicle types, and to input the license
        number of the vehicle. If the vehicle is already in the garage (based on
        license number) the system will display an appropriate message and will use
        the vehicle in the garage (and will change the vehicle status to “In Repair”), if
        not, a new object of that vehicle type will be created and the user will be
        prompted to input the values for the properties of his vehicle, according to the
        type of vehicle he wishes to add. */
        public void AddNewVehicle(eVehicleType i_VehicleType, List<object> i_Values)
        {
            Vehicle newVehicle = VehicleMaker.MakeVehicle(i_VehicleType, i_Values);
            m_GarageVehicles.Add(newVehicle, new VehicleInfo(i_Values[2].ToString(), i_Values[3].ToString()));
        }

        /* 2. Display a list of license numbers currently in the garage, with a filtering option
         *  based on the status of each vehicle */
        public List<string> GetLicenseNumbers()
        {
           List<string> licenseNumbers = new List<string>();
           foreach (Vehicle vehicle in GarageVehicles.Keys)
           {
               licenseNumbers.Add(vehicle.LicenseNumber);
           }

           return licenseNumbers;
        }

        public List<string> GetLicenseByStatus(eVehicleStatus i_VehicleStatus)
        {
            List<string> licenseNumbers = new List<string>();
            VehicleInfo vehicleInfo = null;

            foreach (Vehicle vehicle in GarageVehicles.Keys)
            {
                vehicleInfo = GetVehicleInfoByLicense(vehicle.LicenseNumber);
                if(vehicleInfo.Status == i_VehicleStatus)
                {
                    licenseNumbers.Add(vehicle.LicenseNumber);
                }
            }

            return licenseNumbers;
        }

        public VehicleInfo GetVehicleInfoByLicense(string i_LicenseNumber)
        {
            VehicleInfo vehicleInfo = null;

            foreach (Vehicle vehicle in GarageVehicles.Keys)
            {
                if (vehicle.LicenseNumber == i_LicenseNumber)
                {
                    GarageVehicles.TryGetValue(vehicle, out vehicleInfo);
                }
            }

            return vehicleInfo;
        }

        //3. Change a certain vehicle’s status(Prompting the user for the license number and new desired status)
        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewStatus)
        {
            VehicleInfo vehicleInfo = GetVehicleInfoByLicense(i_LicenseNumber);
            if (vehicleInfo != null)
            {
                vehicleInfo.Status = i_NewStatus;
            }
            else
            {
                throw new ArgumentException("License Doesn't Exist.");
            }
        }

        //4. Inflate tires to maximum(Prompting the user for the license number)
        public void PumpWheelsToMax(string i_LicenseNumber)
        {
            Vehicle vehicle = GetVehicleByLicense(i_LicenseNumber);

            if(vehicle != null)
            {
                foreach (Wheel wheel in vehicle.Wheels)
                {
                    wheel.PumpWheel(wheel.MaxAirPressure - wheel.CurrentAirPressure);
                }
            }
            else
            {
                throw new ArgumentException("Vehicle with given License Number doesn't Exist.");
            }
        }

        public Vehicle GetVehicleByLicense(string i_LicenseNumber)
        {
            Vehicle vehicleToReturn = null;

            foreach (Vehicle vehicle in GarageVehicles.Keys)
            {
                if (vehicle.LicenseNumber == i_LicenseNumber)
                {
                    vehicleToReturn = vehicle;
                }
            }

            return vehicleToReturn;
        }

        //5. Refuel a fuel-based vehicle (Prompting the user for the license number, fuel type and amount to fill)
        public void RefuelVehicle(string i_LicenseNumber, eEnergyType i_FuelType, float i_FuelToAdd)
        {
            FuelVehicle vehilceToRefuel = (FuelVehicle)GetVehicleByLicense(i_LicenseNumber);

            if (vehilceToRefuel.EnergyType == i_FuelType)
            {
                if (vehilceToRefuel.CurrentFuelLevel >= vehilceToRefuel.MaximalFuelLevel)
                {
                    throw new ValueOutOfRangeException(vehilceToRefuel.MaximalFuelLevel, 0, "Tank is already full");
                }

                float currentLevel = vehilceToRefuel.AddEnergy(vehilceToRefuel, vehilceToRefuel.MaximalFuelLevel, vehilceToRefuel.CurrentFuelLevel, i_FuelToAdd);
                vehilceToRefuel.CurrentFuelLevel = currentLevel;
            }
            else
            {
                throw new ArgumentException("Wrong Type of Fuel.");
            }
        }

        //6. Charge an electric-based vehicle (Prompting the user for the license number and number of minutes to charge)
        public void ChargeBattery(string i_LicenseNumber, float i_MinutesToAdd)
        {
            try
            {
                ElectricVehicle vehicleToCharge = (ElectricVehicle)GetVehicleByLicense(i_LicenseNumber);

                if (vehicleToCharge.EnergyType == eEnergyType.Electricity)
                {
                    if (vehicleToCharge.RemainingBatteryTime >= vehicleToCharge.MaximalBatteryTime)
                    {
                        throw new ValueOutOfRangeException(vehicleToCharge.MaximalBatteryTime, 0, "Battery Fully Charged");
                    }

                    float minutesToAdd = vehicleToCharge.AddEnergy(vehicleToCharge, vehicleToCharge.MaximalBatteryTime, vehicleToCharge.RemainingBatteryTime, (i_MinutesToAdd / 60));
                    vehicleToCharge.RemainingBatteryTime = minutesToAdd;
                }
                else
                {
                    throw new ArgumentException("Wrong Type of Energy");
                }
            }

            catch (InvalidCastException ice)
            {
                throw new InvalidCastException("Wrong Type of Energy");
            }
            
        }

        /*7. Display vehicle information (License number, Model name, Owner name, Status in
           garage, Tire specifications(manufacturer and air pressure), Fuel status + Fuel type
           Battery status, other relevant information based on vehicle type)*/
        public List<object> GetFullVehicleDetails(string i_LicenseNumber)
        {
            List<object> fullDetails = new List<object>();
            eVehicleType vehicleType = GetVehicleTypeByLicenseNumber(i_LicenseNumber);
            Vehicle currentVehicle = GetVehicleByLicense(i_LicenseNumber);
            VehicleInfo currentVehicleInfo = GetVehicleInfoByLicense(i_LicenseNumber);

            fullDetails.Add(vehicleType);
            fullDetails.Add(currentVehicle.LicenseNumber);
            fullDetails.Add(currentVehicle.ModelName);
            fullDetails.Add(currentVehicleInfo.OwnerName);
            fullDetails.Add(currentVehicleInfo.OwnerPhoneNumber);
            fullDetails.Add(currentVehicleInfo.Status);
            fullDetails.Add(currentVehicle.Wheels[0].MaxAirPressure);
            
            switch (vehicleType)
            {
                case eVehicleType.ElectricBike:
                    {
                        ElectricBike electricBike = (ElectricBike)currentVehicle;
                        fullDetails.Add(electricBike.LicenseType);
                        fullDetails.Add(electricBike.EngineCCVolume);
                        fullDetails.Add(electricBike.MaximalBatteryTime);
                        fullDetails.Add(electricBike.RemainingBatteryTime);
                        for(int i = 0; i < electricBike.Wheels.Count; i++)
                        {
                            fullDetails.Add(electricBike.Wheels[i].CurrentAirPressure);
                            fullDetails.Add(electricBike.Wheels[i].ManufacturerName);
                        }

                        break;
                    }

                case eVehicleType.ElectricCar:
                    {
                        ElectricCar electricCar = (ElectricCar)currentVehicle;
                        fullDetails.Add(electricCar.CarColor);
                        fullDetails.Add(electricCar.NumOfDoors);
                        fullDetails.Add(electricCar.MaximalBatteryTime);
                        fullDetails.Add(electricCar.RemainingBatteryTime);
                        for (int i = 0; i < electricCar.Wheels.Count; i++)
                        {
                            fullDetails.Add(electricCar.Wheels[i].CurrentAirPressure);
                            fullDetails.Add(electricCar.Wheels[i].ManufacturerName);
                        }

                        break;
                    }

                case eVehicleType.FuelBike:
                    {
                        FuelBike fuelBike = (FuelBike)currentVehicle;
                        fullDetails.Add(fuelBike.LicenseType);
                        fullDetails.Add(fuelBike.EngineCCVolume);
                        fullDetails.Add(fuelBike.EnergyType);
                        fullDetails.Add(fuelBike.MaximalFuelLevel);
                        fullDetails.Add(fuelBike.CurrentFuelLevel);
                        for (int i = 0; i < fuelBike.Wheels.Count; i++)
                        {
                            fullDetails.Add(fuelBike.Wheels[i].CurrentAirPressure);
                            fullDetails.Add(fuelBike.Wheels[i].ManufacturerName);
                        }

                        break;
                    }

                case eVehicleType.FuelCar:
                    {
                        FuelCar fuelCar = (FuelCar)currentVehicle;
                        fullDetails.Add(fuelCar.CarColor);
                        fullDetails.Add(fuelCar.NumOfDoors);
                        fullDetails.Add(fuelCar.EnergyType);
                        fullDetails.Add(fuelCar.MaximalFuelLevel);
                        fullDetails.Add(fuelCar.CurrentFuelLevel);
                        for (int i = 0; i < fuelCar.Wheels.Count; i++)
                        {
                            fullDetails.Add(fuelCar.Wheels[i].CurrentAirPressure);
                            fullDetails.Add(fuelCar.Wheels[i].ManufacturerName);
                        }
                        break;
                    }

                case eVehicleType.Truck:
                    {
                        Truck truck = (Truck)currentVehicle;
                        fullDetails.Add(truck.MaximalFuelLevel);
                        fullDetails.Add(truck.CurrentFuelLevel);
                        fullDetails.Add(truck.EnergyType);
                        fullDetails.Add(truck.IsCarryingHazards);
                        fullDetails.Add(truck.MaxLoadAllowed);
                        for (int i = 0; i < truck.Wheels.Count; i++)
                        {
                            fullDetails.Add(truck.Wheels[i].CurrentAirPressure);
                            fullDetails.Add(truck.Wheels[i].ManufacturerName);
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            return fullDetails;
        }

        public eVehicleType GetVehicleTypeByLicenseNumber(string i_LicenseNumber)
        {
            Vehicle vehicle = GetVehicleByLicense(i_LicenseNumber);

            if (vehicle is FuelBike)
            {
                return eVehicleType.FuelBike;
            }

            if (vehicle is ElectricBike)
            {
                return eVehicleType.ElectricBike;
            }

            if(vehicle is FuelCar)
            {
                return eVehicleType.FuelCar;
            }

            if(vehicle is ElectricCar)
            {
                return eVehicleType.ElectricCar;
            }

            return eVehicleType.Truck;
        }

        public eEnergyType GetEnergyTypeByLicense(string i_LicenseNumber)
        {
            eVehicleType vehicleType = GetVehicleTypeByLicenseNumber(i_LicenseNumber);
            switch (vehicleType)
            {
                case eVehicleType.ElectricBike:
                case eVehicleType.ElectricCar:
                    return eEnergyType.Electricity;
                case eVehicleType.FuelBike:
                    return eEnergyType.Octan95;
                case eVehicleType.FuelCar:
                    return eEnergyType.Octan98;
                default:
                    return eEnergyType.Octan96;
            }
        }
    }
}