using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
// $G$ RUL-004 (-20) Wrong zip name format / folder name format
// $G$ DSN-001 (-5) Missing GarageUI class. Nothing but the entry point of the program should be implemented in Program class.
// $G$ DSN-001 (-5) Missing base energy provider class with concrete Electric/Gas derived classes.
namespace Ex03.ConsoleUI
{
    public class Program
    {
        private const int k_LengthOfPhoneNumber = 10;
        private const int k_LengthOfLicenseNumber = 7;
        private static GarageManager s_GarageManager;
        private static bool s_IsServiceEnded = false;

        public static void Main()
        {
            s_GarageManager = new GarageManager();
            startNewService();
        }

        private static void startNewService()
        {
            while(!s_IsServiceEnded)
            {
                Console.Clear();
                welcomePage();
                int lowestPossibleUserChoice = 1;
                int highestPossibleUserChoice = Enum.GetNames(typeof(eMainMenu)).Length;
                int userChoice = manageUserInput(lowestPossibleUserChoice, highestPossibleUserChoice);
                goToNextScreen(userChoice);
            }

            quitGarageManager();
        }

        private static bool isServiceEnded
        {
            get
            {
                return s_IsServiceEnded;
            }
            set
            {
                s_IsServiceEnded = value;
            }
        }

        private static void goToNextScreen(int i_UserInputChoice)
        {
            eMainMenu userEnumChoice = (eMainMenu)i_UserInputChoice;
            if(userEnumChoice == eMainMenu.Quit)
            {
                isServiceEnded = true;
            }

            switch(userEnumChoice)
            {
                case eMainMenu.AddNewVehicle:
                    {
                        printAddNewVehicleScreen();
                        break;
                    }
                case eMainMenu.PrintAllLicenseNumbers:
                    {
                        printAllLicenseNumbers();
                        break;
                    }
                case eMainMenu.ChangeVehicleStatus:
                    {
                        changeVehicleStatusScreen();
                        break;
                    }
                case eMainMenu.PumbWheelAirPressure:
                    {
                        pumpWheelScreen();
                        break;
                    }
                case eMainMenu.RefuelVehicle:
                    {
                        refuelScreen();
                        break;
                    }
                case eMainMenu.ChargeVehicle:
                    {
                        chargeScreen();
                        break;
                    }
                case eMainMenu.ShowFullDetails:
                    {
                        printAllDetailsScreen();
                        break;
                    }
            }
        }

        private static void printAllDetailsScreen()
        {
            List<object> fullDetails = new List<object>();
            Console.Clear();
            Console.WriteLine("Please Enter Vehicle's License Number that you want Full Details on:");
            string license = verifyStringContainsOnlyNumbers(k_LengthOfLicenseNumber);
            if(checkIfLicenseDoesntExist(license))
            {
                Console.WriteLine("Vehicle you wanted check doesn't exist in the system.");
            }
            else
            {
                fullDetails = s_GarageManager.GetFullVehicleDetails(license);
                eVehicleType vehicleType = (eVehicleType)fullDetails[0];
                switch(vehicleType)
                {
                    case eVehicleType.ElectricBike:
                        printToScreenElectricBikeInfo(fullDetails);
                        break;
                    case eVehicleType.ElectricCar:
                        printToScreenElectricCarInfo(fullDetails);
                        break;
                    case eVehicleType.FuelBike:
                        printToScreenFuelBikeInfo(fullDetails);
                        break;
                    case eVehicleType.FuelCar:
                        printToScreenFuelCarInfo(fullDetails);
                        break;
                    case eVehicleType.Truck:
                        printToScreenTruckInfo(fullDetails);
                        break;
                }
            }
            Console.WriteLine("Press Enter to return to the Main Menu...");
            Console.ReadLine();
        }

        private static void printToScreenTruckInfo(List<object> i_FullDetails)
        {
            Console.Clear();
            Console.WriteLine(
                string.Format(
@"Truck Info:

License Number: {0}
Model Name: {1}
Owner Name: {2}
Phone Number: {3}
Status in the Garage: {4}
Max Wheel Air Pressure: {5}
Maximum fuel level: {6}
Current fuel level: {7}
Fuel type: {8}
Carrying hazards: {9}
Maximum load allowed: {10}
Wheels Details:
Current Wheel #1 Air Pressure: {11}
Wheel #1 Manufacturer Name: {12}
Current Wheel #2 Air Pressure: {13}
Wheel #2 Manufacturer Name: {14}
Current Wheel #3 Air Pressure: {15}
Wheel #3 Manufacturer Name: {16}
Current Wheel #4 Air Pressure: {17}
Wheel #4 Manufacturer Name: {18}
Current Wheel #5 Air Pressure: {19}
Wheel #5 Manufacturer Name: {20}
Current Wheel #6 Air Pressure: {21}
Wheel #6 Manufacturer Name: {22}
Current Wheel #7 Air Pressure: {23}
Wheel #7 Manufacturer Name: {24}
Current Wheel #8 Air Pressure: {25}
Wheel #8 Manufacturer Name: {26}
Current Wheel #9 Air Pressure: {27}
Wheel #9 Manufacturer Name: {28}
Current Wheel #10 Air Pressure: {29}
Wheel #10 Manufacturer Name: {30}
Current Wheel #11 Air Pressure: {31}
Wheel #11 Manufacturer Name: {32}
Current Wheel #12 Air Pressure: {33}
Wheel #12 Manufacturer Name: {34}
",
i_FullDetails[1], i_FullDetails[2], i_FullDetails[3], i_FullDetails[4].ToString(),
i_FullDetails[5].ToString(), i_FullDetails[6].ToString(), i_FullDetails[7].ToString(),
i_FullDetails[8].ToString(), i_FullDetails[9].ToString(), i_FullDetails[10],
i_FullDetails[11], i_FullDetails[12], i_FullDetails[13], i_FullDetails[14],
i_FullDetails[15], i_FullDetails[16], i_FullDetails[17], i_FullDetails[18],
i_FullDetails[19], i_FullDetails[20], i_FullDetails[21], i_FullDetails[22],
i_FullDetails[23], i_FullDetails[24], i_FullDetails[25], i_FullDetails[25],
i_FullDetails[26], i_FullDetails[27], i_FullDetails[28], i_FullDetails[29],
i_FullDetails[30], i_FullDetails[31], i_FullDetails[32], i_FullDetails[33],
i_FullDetails[34], i_FullDetails[35]));
        }

        private static void printToScreenFuelCarInfo(List<object> i_FullDetails)
        {
            Console.Clear();
            Console.WriteLine(
                string.Format(
@"Fuel Car Info:

License Number: {0}
Model Name: {1}
Owner Name: {2}
Phone Number: {3}
Status in the Garage: {4}
Max Wheel Air Pressure: {5}
Color: {6}
Number of doors: {7}
fuel type: {8}
Maximum fuel level: {9}
Current fuel level: {10}
Wheels Details:
Current Wheel #1 Air Pressure: {11}
Wheel #1 Manufacturer Name: {12}
Current Wheel #2 Air Pressure: {13}
Wheel #2 Manufacturer Name: {14}
Current Wheel #3 Air Pressure: {15}
Wheel #3 Manufacturer Name: {16}
Current Wheel #4 Air Pressure: {17}
Wheel #4 Manufacturer Name: {18}
",
i_FullDetails[1], i_FullDetails[2], i_FullDetails[3], i_FullDetails[4].ToString(),
i_FullDetails[5].ToString(), i_FullDetails[6].ToString(), i_FullDetails[7].ToString(),
i_FullDetails[8].ToString(), i_FullDetails[9].ToString(), i_FullDetails[10],
i_FullDetails[11], i_FullDetails[12], i_FullDetails[13], i_FullDetails[14],
i_FullDetails[15], i_FullDetails[16], i_FullDetails[17], i_FullDetails[18], i_FullDetails[19]));
        }

        private static void printToScreenElectricCarInfo(List<object> i_FullDetails)
        {
            Console.Clear();
            Console.WriteLine(
                string.Format(
@"Electric Car Info:

License Number: {0}
Model Name: {1}
Owner Name: {2}
Phone Number: {3}
Status in the Garage: {4}
Max Wheel Air Pressure: {5}
Color: {6}
Number of doors: {7}
Max Battery Time: {8}
Remaining Battery Time: {9}
Wheels Details:
Current Wheel #1 Air Pressure: {10}
Wheel #1 Manufacturer Name: {11}
Current Wheel #2 Air Pressure: {12}
Wheel #2 Manufacturer Name: {13}
Current Wheel #3 Air Pressure: {14}
Wheel #3 Manufacturer Name: {15}
Current Wheel #4 Air Pressure: {16}
Wheel #4 Manufacturer Name: {17}
",
i_FullDetails[1], i_FullDetails[2], i_FullDetails[3],
i_FullDetails[4].ToString(), i_FullDetails[5].ToString(), i_FullDetails[6].ToString(),
i_FullDetails[7].ToString(), i_FullDetails[8].ToString(), i_FullDetails[9].ToString(),
i_FullDetails[10], i_FullDetails[11], i_FullDetails[12], i_FullDetails[13], i_FullDetails[14],
i_FullDetails[15], i_FullDetails[16], i_FullDetails[17], i_FullDetails[18]));
        }

        private static void printToScreenFuelBikeInfo(List<object> i_FullDetails)
        {
            Console.Clear();
            Console.WriteLine(
                string.Format(
@"Fuel Bike Info:

License Number: {0}
Model Name: {1}
Owner Name: {2}
Phone Number: {3}
Status in the Garage: {4}
Max Wheel Air Pressure: {5}
License Type: {6}
Engine CC Volume: {7}
Fuel type: {8}
Maximum fuel level: {9}
Current fuel level: {10}
Wheels Details:
Current Wheel #1 Air Pressure: {11}
Wheel #1 Manufacturer Name: {12}
Current Wheel #2 Air Pressure: {13}
Wheel #2 Manufacturer Name: {14}
",
i_FullDetails[1], i_FullDetails[2], i_FullDetails[3], i_FullDetails[4].ToString(),
i_FullDetails[5].ToString(), i_FullDetails[6].ToString(), i_FullDetails[7].ToString(),
i_FullDetails[8].ToString(), i_FullDetails[9].ToString(), i_FullDetails[10].ToString(),
i_FullDetails[11].ToString(), i_FullDetails[12], i_FullDetails[13].ToString(), i_FullDetails[14], i_FullDetails[15]));
        }

        private static void printToScreenElectricBikeInfo(List<object> i_FullDetails)
        {
            Console.Clear();
            Console.WriteLine(
                string.Format(
@"Electric Bike Info:

License Number: {0}
Model Name: {1}
Owner Name: {2}
Phone Number: {3}
Status in the Garage: {4}
Max Wheel Air Pressure: {5}
License Type: {6}
Engine CC Volume: {7}
Max Battery Time: {8}
Remaining Battery Time: {9}
Wheels Details:
Current Wheel #1 Air Pressure: {10}
Wheel #1 Manufacturer Name: {11}
Current Wheel #2 Air Pressure: {12}
Wheel #2 Manufacturer Name: {13}
",
i_FullDetails[1], i_FullDetails[2], i_FullDetails[3], i_FullDetails[4].ToString(), 
i_FullDetails[5].ToString(), i_FullDetails[6].ToString(), i_FullDetails[7].ToString(),
i_FullDetails[8].ToString(), i_FullDetails[9].ToString(), i_FullDetails[10].ToString(),
i_FullDetails[11].ToString(), i_FullDetails[12], i_FullDetails[13].ToString(), i_FullDetails[14]));
        }

        private static void chargeScreen()
        {
            Console.Clear();
            Console.WriteLine("Please Enter Vehicle License Number that you want to Charge: ");
            string license = verifyStringContainsOnlyNumbers(k_LengthOfLicenseNumber);
            if(checkIfLicenseDoesntExist(license))
            {
                Console.WriteLine(
                    string.Format(
@"Vehicle you wanted to Charge doesnt exist is the system. 
Press Enter to go back to Main Menu..."));
                Console.ReadLine();
            }
            else
            {
                bool isValidMinutes = false;
                while(!isValidMinutes)
                {
                    Console.WriteLine("Please Enter number of Minutes you want to Charge:");
                    try
                    {
                        float minutesToAdd = verifyInputIsANumber();
                        s_GarageManager.ChargeBattery(license, minutesToAdd);
                        isValidMinutes = true;
                        Console.Clear();
                        Console.WriteLine(
                            string.Format(
@"Vehicle was Charged Successfully!
Press Enter to return to the main menu"));
                        Console.ReadLine();
                    }
                    catch(ValueOutOfRangeException vore)
                    {
                        if(vore.Message.Equals("Battery Fully Charged"))
                        {
                            isValidMinutes = true;
                            Console.WriteLine(
                                "Battery Already Fully Charged! Press Enter to return to the main menu...");
                            Console.ReadLine();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You exceeded the amount of time you can charge the Battery. Try again:");
                            continue;
                        }
                    }

                    catch(InvalidCastException ice)
                    {
                        Console.WriteLine(
                            string.Format(
@"You tried to Charge Fuel-Based Vehicle!
Press Enter to return to main menu... "));
                        Console.ReadLine();
                        break;
                    }

                    catch(FormatException fm)
                    {
                        Console.WriteLine("Not a valid Number! Try Again:");
                        continue;
                    }

                    catch(ArgumentException ae)
                    {
                        Console.WriteLine("Wrong Type of Energy Type");
                        continue;
                    }
                }
            }
        }

        private static void refuelScreen()
        {
            Console.Clear();
            Console.WriteLine("Please Enter Vehicle License Number that you want to Refuel: ");
            string license = verifyStringContainsOnlyNumbers(k_LengthOfLicenseNumber);
            if(checkIfLicenseDoesntExist(license))
            {
                Console.WriteLine(
                    string.Format(
@"Vehicle you wanted to Refuel doesnt exist is the system. 
Press Enter to go back to Main Menu..."));
                Console.ReadLine();
            }
            else
            {
                bool isRightFuelType = false;

                while(!isRightFuelType)
                {
                    Console.Clear();
                    Console.WriteLine(
                        string.Format(
@"Please Enter Fuel Type you want to add: 
1. Soler
2. Octan95
3. Octan96
4. Octan98"));
                    const int k_LowestPossibleUserChoice = 1;
                    int highestPossibleUserChoice = Enum.GetNames(typeof(eEnergyType)).Length - 1;
                    int displayOption = manageUserInput(k_LowestPossibleUserChoice, highestPossibleUserChoice);
                    eEnergyType energyTypeChosen = (eEnergyType)displayOption;
                    bool isNumber = false;
                    while(!isNumber)
                    {
                        Console.WriteLine("Please Insert How many Litters You Want to Add: ");
                        try
                        {
                            float littersToAdd = verifyInputIsANumber();
                            s_GarageManager.RefuelVehicle(license, energyTypeChosen, littersToAdd);
                            isRightFuelType = true;
                            isNumber = true;
                            Console.Clear();
                            Console.WriteLine(
                                string.Format(
                                    @"Vehicle was Refueled Successfully!
Press Enter to return to the main menu"));
                            Console.ReadLine();
                        }
                        catch(ArgumentException ae)
                        {
                            Console.WriteLine("Wrong type of Fuel. Press Enter to Try Again:");
                            Console.ReadLine();
                            break;
                        }
                        catch(ValueOutOfRangeException vore)
                        {
                            if(vore.Message.Equals("Tank is already full"))
                            {
                                isRightFuelType = true;
                                isNumber = true;
                                Console.WriteLine("Tank is already Full! Press Enter to return to the main menu...");
                                Console.ReadLine();
                                break;
                            }
                            else
                            {
                                Console.WriteLine(
                                    "You exceeded the amount of fuel you can fill in the tank. Try again:");
                                continue;
                            }
                        }
                        catch(FormatException fm)
                        {
                            Console.WriteLine("Not a valid Number! Try Again:");
                            continue;
                        }

                        catch (InvalidCastException ice)
                        {
                            Console.WriteLine("Can't refuel an electric vehicle! Press enter to go to main menu:");
                            Console.ReadLine();
                            isRightFuelType = true;
                            isNumber = true;
                            continue;
                        }
                    }
                }
            }
        }

        private static void pumpWheelScreen()
        {
            Console.Clear();
            Console.WriteLine("Please Enter Vehicle License Number that you want to pump wheel air to max: ");
            string license = verifyStringContainsOnlyNumbers(k_LengthOfLicenseNumber);
            if(checkIfLicenseDoesntExist(license))
            {
                Console.WriteLine(
                    string.Format(
@"Vehicle you wanted to pump wheel's air for doesnt exist is the system. 
Press Enter to go back to Main Menu..."));
                Console.ReadLine();
            }
            else
            {
                s_GarageManager.PumpWheelsToMax(license);
                Console.Clear();
                Console.WriteLine(
                    string.Format(
@"Wheel Air Pressure for Vehicle {0} is now at Maximum!
Press Enter to go back to Main Menu...",
                        license));
                Console.ReadLine();
            }
        }

        private static void changeVehicleStatusScreen()
        {
            Console.Clear();
            Console.WriteLine("Please Enter Vehicle License Number that you want to change: ");
            bool isNumber = false;
            string license = string.Empty;
            while(!isNumber)
            {
                try
                {
                    license = verifyStringContainsOnlyNumbers(k_LengthOfLicenseNumber);
                    isNumber = true;
                }
                catch(FormatException fm)
                {
                    Console.WriteLine("Not a valid Number! Try Again:");
                    continue;
                }
            }

            if(checkIfLicenseDoesntExist(license))
            {
                Console.WriteLine(
                    string.Format(
@"Vehicle you wanted to change status for doesnt exist is the system. 
Press Enter to go back to Main Menu..."));
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(
                    string.Format(
@"Please Enter the new status of the vehicle: 
1. In Progress
2. Fixed
3. Payed"));
                int lowestPossibleUserChoice = 1;
                int highestPossibleUserChoice = Enum.GetNames(typeof(eVehicleStatus)).Length - 1;
                int displayOption = manageUserInput(lowestPossibleUserChoice, highestPossibleUserChoice);
                eVehicleStatus status = (eVehicleStatus)displayOption;
                s_GarageManager.ChangeVehicleStatus(license, status);
                Console.WriteLine(
                    string.Format(
@"
Vehicle Status has been Changed!
Press Enter to return to the Main Menu"));
                Console.ReadLine();
            }
        }

        private static void printAllLicenseNumbers()
        {
            Console.Clear();
            Console.WriteLine(
                string.Format(
@"Choose Display Option: 
1. All In Progress vehicles
2. All Fixed Vehicles
3. All Payed Vehicles
4. All License Numbers"));
            const int k_LowestPossibleUserChoice = 1;
            int highestPossibleUserChoice = Enum.GetNames(typeof(eVehicleStatus)).Length;
            int displayOption = manageUserInput(k_LowestPossibleUserChoice, highestPossibleUserChoice);
            eVehicleStatus status = (eVehicleStatus)displayOption;
            displayLicenseNumbers(status);
            Console.WriteLine(
                string.Format(
@"
Press Enter to return to the Main Menu"));
            Console.ReadLine();
        }

        private static void displayLicenseNumbers(eVehicleStatus i_Status)
        {
            List<string> licenseNumbers = new List<string>();

            Console.Clear();
            if(i_Status == eVehicleStatus.Other)
            {
                licenseNumbers = s_GarageManager.GetLicenseNumbers();
            }
            else
            {
                licenseNumbers = s_GarageManager.GetLicenseByStatus(i_Status);
            }

            foreach(string license in licenseNumbers)
            {
                Console.WriteLine(license);
            }
        }

        private static void printAddNewVehicleScreen()
        {
            List<object> listOfDetailsForVehicle = new List<object>();
            Console.Clear();
            Console.WriteLine(
                // $G$ DSN-001 (-20) The UI must not know specific types and their properties concretely! It means that when adding a new type you'll have to change the code here too!
                string.Format(
@"Please choose vehicle type (Choose a number between 1-5): 
1. Fuel Bike
2. Electric Bike
3. Fuel Car
4. Electric Car
5. Truck"));
            int lowestPossibleUserChoice = 1;
            int highestPossibleUserChoice = Enum.GetNames(typeof(eVehicleType)).Length;
            int vehicleType = manageUserInput(lowestPossibleUserChoice, highestPossibleUserChoice);
            eVehicleType vehicleTypeEnum = (eVehicleType)vehicleType;

            try
            {
                listOfDetailsForVehicle = setDetailsAccordingToVehicleType(listOfDetailsForVehicle, vehicleTypeEnum);
                s_GarageManager.AddNewVehicle(vehicleTypeEnum, listOfDetailsForVehicle);
                Console.Clear();
                Console.WriteLine(
                    string.Format(
@"Vehicle Added successfully to the system!
Press Enter to return to the Main Menu"));
                Console.ReadLine();
            }
            catch(ArgumentException ex)
            {
                if(ex.Message.Equals("Different Type of Car"))
                {
                    Console.WriteLine(
                        string.Format(
@"The Vehicle already exists in the garage, but is registered with a Different Vehicle Type! 
Press Enter to return to the main menu..."));
                    Console.ReadLine();
                }

                if(ex.Message.Equals("License Exists in Garage"))
                {
                    Console.WriteLine(
                        string.Format(
@"The Vehicle already exists in the garage!
Press Enter to return to the main menu..."));
                    Console.ReadLine();
                }
            }
        }

        private static List<object> setDetailsAccordingToVehicleType(
            List<object> io_ListOfDetailsForVehicle,
            eVehicleType i_VehicleType)
        {
            Console.Clear();
            Console.WriteLine("Please Enter Vehicle Model: ");
            string modelName = Console.ReadLine();
            io_ListOfDetailsForVehicle.Add(modelName);
            Console.WriteLine("Please Enter Vehicle License Number: ");
            string licenseNumber = verifyStringContainsOnlyNumbers(k_LengthOfLicenseNumber);
            if((checkIfLicenseDoesntExist(licenseNumber)))
            {
                io_ListOfDetailsForVehicle.Add(licenseNumber);
            }
            else
            {
                if(s_GarageManager.GetVehicleTypeByLicenseNumber(licenseNumber) == i_VehicleType)
                {
                    throw new ArgumentException("License Exists in Garage");
                }
                else
                {
                    throw new ArgumentException("Different Type of Car");
                }
            }
            Console.WriteLine("Please Enter the Vehicle Owner's Name: ");
            string ownerName = verifyValidOwnerName();
            io_ListOfDetailsForVehicle.Add(ownerName);
            Console.WriteLine("Please Enter the Owner's Phone Number: ");
            string ownerPhoneNumber = verifyStringContainsOnlyNumbers(k_LengthOfPhoneNumber);
            io_ListOfDetailsForVehicle.Add(ownerPhoneNumber);
            Console.WriteLine("Please Enter Wheels Manufacturer: ");
            string wheelManufacturer = Console.ReadLine();
            io_ListOfDetailsForVehicle.Add(wheelManufacturer);
            Console.WriteLine("Please Enter Current Wheel Pressure: ");
            float wheelPressure = verifyValidWheelPressure(i_VehicleType);
            io_ListOfDetailsForVehicle.Add(wheelPressure);

            switch(i_VehicleType)
            {
                case eVehicleType.FuelBike:
                case eVehicleType.ElectricBike:
                    {
                        io_ListOfDetailsForVehicle = setListOfDetailsForBike(io_ListOfDetailsForVehicle);
                        break;
                    }
                case eVehicleType.FuelCar:
                case eVehicleType.ElectricCar:
                    {
                        io_ListOfDetailsForVehicle = setListOfDetailsForCar(io_ListOfDetailsForVehicle);
                        break;
                    }
                case eVehicleType.Truck:
                    {
                        io_ListOfDetailsForVehicle = setListOfDetailsForTruck(io_ListOfDetailsForVehicle);
                        break;
                    }
            }

            return io_ListOfDetailsForVehicle;
        }

        private static string verifyValidOwnerName()
        {
            bool isValidName = false;
            string ownerName = string.Empty;

            while(!isValidName)
            {
                ownerName = Console.ReadLine();
                char[] ownerNameToCharArray = new char[ownerName.Length];
                ownerNameToCharArray = ownerName.ToCharArray();
                bool goToWhile = false;
                foreach(char c in ownerNameToCharArray)
                {
                    // $G$ CSS-999 (-2) You should have used constant
                    if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (char.IsWhiteSpace(c)))
                    {
                        continue;
                    }

                    goToWhile = true;
                }

                if(goToWhile == true)
                {
                    Console.WriteLine("That's Not a Name... Only Letters! try again: ");
                    continue;
                }
                else
                {
                    isValidName = true;
                }
            }

            return ownerName;
        }

        private static List<object> setListOfDetailsForTruck(List<object> io_ListOfDetailsForVehicle)
        {
            Console.WriteLine("The Truck Carrying Hazards? Y/N");
            bool carryingHazards = verifyCarryingHazardsInput();
            io_ListOfDetailsForVehicle.Add(carryingHazards);
            Console.WriteLine("What is the truck maximum load allowed? ");
            float maxLoad = 0;
            bool isNumber = false;
            while(!isNumber)
            {
                try
                {
                    maxLoad = verifyInputIsANumber();
                    isNumber = true;
                }
                catch(FormatException fm)
                {
                    Console.WriteLine("Not a valid Number! Try Again:");
                    continue;
                }
            }

            io_ListOfDetailsForVehicle.Add(maxLoad);

            return io_ListOfDetailsForVehicle;
        }

        private static bool verifyCarryingHazardsInput()
        {
            bool isValidInput = false;
            bool isCarryingHazards = true;
            while(!isValidInput)
            {
                string answer = Console.ReadLine();
                if(answer == "Y" || answer == "y")
                {
                    break;
                }
                else if(answer == "N" || answer == "n")
                {
                    isCarryingHazards = false;
                    break;
                }
                else
                {
                    Console.WriteLine("invalid input! try again: Y/N");
                }
            }

            return isCarryingHazards;
        }

        private static List<object> setListOfDetailsForCar(List<object> io_ListOfDetailsForVehicle)
        {
            Console.WriteLine("Please Enter the Car's Color: ");
            eCarColor carColor = verifyValidCarColor();
            io_ListOfDetailsForVehicle.Add(carColor);
            Console.WriteLine("Please Enter Number of Doors: ");
            eNumOfDoors numOfDoors = verifyNumberOfDoors();
            io_ListOfDetailsForVehicle.Add(numOfDoors);

            return io_ListOfDetailsForVehicle;
        }

        private static List<object> setListOfDetailsForBike(List<object> io_ListOfDetailsForVehicle)
        {
            Console.WriteLine("Please Enter Bike License Type: ");
            eLicenseType licenseType = verifyValidLicenseType();
            io_ListOfDetailsForVehicle.Add(licenseType);
            Console.WriteLine("Please Enter Bike Engine CC Volume");
            bool isNumber = false;
            int ccVolume = 0;
            while(!isNumber)
            {
                try
                {
                    ccVolume = (int)verifyInputIsANumber();
                    isNumber = true;
                }
                catch(FormatException fm)
                {
                    Console.WriteLine("Not a Valid Number! Try again: ");
                    continue;
                }
            }

            io_ListOfDetailsForVehicle.Add(ccVolume);

            return io_ListOfDetailsForVehicle;
        }

        private static eNumOfDoors verifyNumberOfDoors()
        {
            const int k_LowestPossibleInput = 1;
            int highestPossibleInput = Enum.GetNames(typeof(eNumOfDoors)).Length;
            Console.WriteLine(
                string.Format(
@"Choose number of doors (1-4):
1. Two
2. Three
3. Four
4. Five"));
            int userInput = manageUserInput(k_LowestPossibleInput, highestPossibleInput);
            eNumOfDoors eUserInput = (eNumOfDoors)userInput;

            return eUserInput;
        }

        private static eCarColor verifyValidCarColor()
        {
            int lowestPossibleInput = 1;
            int highestPossibleInput = Enum.GetNames(typeof(eCarColor)).Length;
            Console.WriteLine(
                string.Format(
@"Choose Relevant Car Color (1-4):
1. Yellow
2. Black
3. White
4. Blue"));
            int userInput = manageUserInput(lowestPossibleInput, highestPossibleInput);
            eCarColor eUserInput = (eCarColor)userInput;

            return eUserInput;
        }

        private static float verifyInputIsANumber()
        {
            float userInput = 0;
            bool isValidInput = false;

            while(!isValidInput)
            {
                string inputFromUser = Console.ReadLine();
                bool isNumber = float.TryParse(inputFromUser, out userInput);
                if(isNumber)
                {
                    isValidInput = true;
                    continue;
                }
                else
                {
                    throw new FormatException("Not a Number or Number is too long.");
                }
            }

            return userInput;
        }

        private static eLicenseType verifyValidLicenseType()
        {
            int lowestPossibleInput = 1;
            int highestPossibleInput = Enum.GetNames(typeof(eLicenseType)).Length;
            Console.WriteLine(
                string.Format(
@"Choose Relevant Bike License Type (1-4):
1. A
2. AB
3. A2
4. B1"));
            int userInput = manageUserInput(lowestPossibleInput, highestPossibleInput);
            eLicenseType eUserInput = (eLicenseType)userInput;

            return eUserInput;
        }

        private static int manageUserInput(int i_LowestPossibleUserChoice, int i_HighestPossibleUserChoice)
        {
            bool isValidInput = false;
            int userChoice = 0;

            while(!isValidInput)
            {
                string userInput = Console.ReadLine();
                bool isInputNumber = int.TryParse(userInput, out userChoice);
                if(!isInputNumber)
                {
                    Console.WriteLine("your input must be a number! try again: ");
                    continue;
                }
                else
                {
                    if(userChoice < i_LowestPossibleUserChoice || userChoice > i_HighestPossibleUserChoice)
                    {
                        Console.WriteLine(
                            string.Format(
                                @"The number you inserted is not one of the options! 
number should be between {0} and {1}.
Please Try again: ",
                                i_LowestPossibleUserChoice,
                                i_HighestPossibleUserChoice));
                        continue;
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
            }

            return userChoice;
        }

        private static bool checkIfLicenseDoesntExist(string i_LicenseNumber)
        {
            bool doesntExist;

            if(s_GarageManager.GetVehicleByLicense(i_LicenseNumber) == null)
            {
                doesntExist = true;
            }
            else
            {
                doesntExist = false;
            }

            return doesntExist;
        }

        private static void welcomePage()
        {
            Console.WriteLine(
                string.Format(
                    @"Welcome! You've entered our Garage Manager System!
Please Select one of the options below (Choose 1-8): 
1. Add a new vehicle to the garage.
2. Display all license numbers of all vehicles in our garage.
3. Change vehicle's status.
4. Pump vehicle's wheels air pressure to maximum.
5. Refuel fuel-based vehicle.
6. Charge electric-based vehicle.
7. Display vehicle's full details.
8. Quit"));
        }

        private static string verifyStringContainsOnlyNumbers(int i_LengthOfInput)
        {
            bool isValidString = false;
            string validStringAsNumber = string.Empty;

            while(!isValidString)
            {
                validStringAsNumber = Console.ReadLine();
                if(validStringAsNumber != null && validStringAsNumber.Length != i_LengthOfInput)
                {
                    Console.WriteLine(string.Format(@"Input must contain {0} digits! try again: ", i_LengthOfInput));
                    continue;
                }
                else
                {
                    char[] licenseToCharArray = new char[k_LengthOfLicenseNumber];
                    licenseToCharArray = validStringAsNumber.ToCharArray();
                    bool goToWhile = false;
                    foreach(char c in licenseToCharArray)
                    {
                        if(c >= '0' && c <= '9')
                        {
                            continue;
                        }
                        else
                        {
                            goToWhile = true;
                        }
                    }

                    if(goToWhile)
                    {
                        Console.WriteLine("Not a valid number! should contain only digits.");
                        continue;
                    }

                    isValidString = true;
                }
            }

            return validStringAsNumber;
        }

        private static float verifyValidWheelPressure(eVehicleType i_VehicleType)
        {
            int maxAirPressureAccordingToVehicleType = 0;

            switch(i_VehicleType)
            {
                case eVehicleType.ElectricBike:
                case eVehicleType.FuelBike:
                    {
                        maxAirPressureAccordingToVehicleType = 33;
                        break;
                    }

                case eVehicleType.ElectricCar:
                case eVehicleType.FuelCar:
                    {
                        maxAirPressureAccordingToVehicleType = 30;
                        break;
                    }

                case eVehicleType.Truck:
                    {
                        maxAirPressureAccordingToVehicleType = 32;
                        break;
                    }
            }

            return (float)manageUserInput(0, maxAirPressureAccordingToVehicleType);
        }

        private static void quitGarageManager()
        {
            Console.Clear();
            Console.WriteLine("Thank you! See you next time.");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}