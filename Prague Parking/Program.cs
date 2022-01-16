using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace Prague_Parking
{
    class Program
    {
        static string[] Parking = new string[100]; 
        static int number, Index, cantPark;
        
        static void MainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("[1] Add vehicle");
            Console.WriteLine("[2] Move vehicle");
            Console.WriteLine("[3] Remove vehicle");
            Console.WriteLine("[4] Look for vehicle");
            Console.WriteLine("[5] Exit Program");

            try
            {
                int userInput = int.Parse(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        ParkMenu();
                        break;
                    case 2:
                        Move();
                        break;
                    case 3:
                        RemoveVehicle();
                        break;
                    case 4:
                        Search();
                        break;
                    case 5:
                        Exit();
                        break;
                    default:
                        MainMenu();
                        break;
                }
            }
            catch (Exception)
            {
                MainMenu();
                throw;
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;

            Parking[1] = "CARϴFLX819"; //Test
            Parking[28] = "CARϴRET119"; //Test
            Parking[48] = "CARϴAAA818"; //Test
            Parking[51] = "MCϴOLP153"; //Test
            Parking[63] = "MCϴSAP553|MCϴSRR336"; //Test

            MainMenu();
        }

        static void ParkMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Do you wish to add a Car or MC"); 
            Console.WriteLine();
            Console.WriteLine("[1] Car");
            Console.WriteLine("[2] MC");
            Console.WriteLine("[0] Main Menu");
            Console.WriteLine();

            int userInput = int.Parse(Console.ReadLine());

            try
            {
                switch (userInput)
                {
                    case 1:
                        AddCar();
                        break;
                    case 2:
                        AddMC();
                        break;
                    case 0:
                        MainMenu();
                        break;
                }
            }
            catch (Exception)
            {
                MainMenu();
                throw;
            }
        }

        static void AddCar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            string addSpace = string.Empty;
            int emptySpace = SpaceExist();

            Console.Write("Enter Licence Plate Number for Car: ");
            string carLicence = Console.ReadLine().ToUpper();

            if (SearchForVehicle(carLicence))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine($"{carLicence} is already parked. Maybe you wish to move or remove it?\nReturning to Main Menu...");
                Thread.Sleep(3000);
                MainMenu();
            }
            else if (carLicence.Length >= 4 && carLicence.Length <= 10)
            {
                for (int i = 0; i < Parking.Length; i++)
                {
                    if (Parking[i] == null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;

                        Console.WriteLine($"{carLicence} is now parked.\nReturning to Main Menu...");

                        addSpace = "CARϴ" + carLicence;
                        AddSpace(emptySpace, addSpace);

                        Thread.Sleep(2000);
                        MainMenu();
                    }
                    else if (Parking[i] != null)
                    {
                        continue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                        Console.WriteLine("There are no more available parking spots. Please Remove a vehicle to empty parking spot.");
                        Console.ReadKey();
                        Console.Clear();
                        MainMenu();
                    }
                    
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine($"{carLicence} is either too long or too short.\nEnter any key to try again.");
                Console.ReadKey();
                Console.Clear();
                ParkMenu();
            }
        }

        static void AddMC()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            string addSpace = string.Empty;
            int emptySpace = SpaceExist();

            Console.Write("Enter Licence Plate Number for MC: ");
            string mcLicence = Console.ReadLine().ToUpper();

            for (int i = 0; i < Parking.Length; i++)
            {
                if (SearchForVehicle(mcLicence))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine($"{mcLicence} is already parked. Maybe you wish to move or remove it?\nRedirecting you to Main Menu.");
                    Thread.Sleep(2500);
                    MainMenu();
                }
            }
            if (mcLicence.Length >= 4 && mcLicence.Length <= 10)
            {
                for (int j = 0; j < Parking.Length; j++)
                {
                    if (Parking[j] != null)
                    {
                        if (Parking[j].Contains("|"))
                        {
                            continue;
                        }
                        if (Parking[j].Contains("MCϴ"))
                        {
                            string id = "|MCϴ";
                            string contain = string.Join(id, Parking[j], mcLicence);
                            Parking[j] = contain;

                            Console.ForegroundColor = ConsoleColor.DarkCyan;

                            Console.WriteLine($"{mcLicence} has been parked. Retruning to Main Menu.\nPlease Wait.");
                            Thread.Sleep(2000);
                            MainMenu();
                            break;
                        }
                        
                    }
                    if (Parking[j] == null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        
                        Console.WriteLine($"{mcLicence} has been parked. Returning to Main Menu.\nPlease Wait.");

                        addSpace = "MCϴ" + mcLicence;
                        AddSpace(emptySpace, addSpace);

                        Thread.Sleep(2000);
                        MainMenu();
                        break;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine($"{mcLicence} is either too long or too short.\nPress any key to try again.");
                Console.ReadKey();
                Console.Clear();
                AddMC();
            }
            ParkMenu();
        }
        static void AddSpace(int space, string vehicle) //Add vehicle to Array[].
        {
            space = space - 1;
            Parking[space] = vehicle;
        }
        static int SpaceExist()
        {
            int counter = 0;
            int emptySpace = 0;

            foreach (string parkingSpace in Parking)
            {
                counter = counter + 1;
                if (parkingSpace == null)
                {
                    emptySpace = counter;
                    return emptySpace;
                }
                else if (counter == 100)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("No available spaces left.\nReturning to Main Menu...");
                    Thread.Sleep(3000);
                    MainMenu();
                }
            }
            emptySpace = counter;
            return emptySpace;
        }
        static bool SearchForVehicle(string input)
        {
            for (int i = 0; i < Parking.Length; i++)
            {
                if (Parking[i] != null)
                {
                    if (Parking[i].Contains("MCϴ" + input) || Parking[i].Contains("CARϴ" + input))
                    {
                        Index = i;
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return false;
        }
        static void Search()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Licence Number of Car or Motorcycle");
            string licenceSearch = Console.ReadLine().ToUpper();

            for (int i = 0; i < Parking.Length; i++)
            {
                if (Parking[i] != null)
                {
                    if (Parking[i].Contains("MCϴ" + licenceSearch) || Parking[i].Contains("CARϴ" + licenceSearch))
                    {
                        if (Parking[i].Contains("MCϴ"))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"\n{licenceSearch} can be found at parking spot number: {i + 1}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to return to Main Menu.");
                            Console.ReadKey();
                            MainMenu();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"\n{licenceSearch} can be found at parking spot number: {i + 1}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to return to Main Menu.");
                            Console.ReadKey();
                            MainMenu();
                            break;
                        }
                    }
                }
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine($"Sorry, vehicle couldn't be found. Maybe {licenceSearch} is stolen?\nReturning to Main Menu...");
            Thread.Sleep(2500);
            MainMenu();
        }
        static void Move() 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Do you wish to move a [1. Car] or [2. Motorcycle]?");
            int userInput = int.Parse(Console.ReadLine());

            switch (userInput)
            {
                case 1:
                    MoveCar();
                    break;

                case 2:
                    MoveMC();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Please choose from 1-2");
                    break;
            }
        }

        static void MoveCar()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Car Licence Plate Number:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[CARϴ]");

            string carLicence = CheckUserLicence(Console.ReadLine().ToUpper());
            Console.ForegroundColor = ConsoleColor.White;

            if (SearchLicence(carLicence))
            {
                int index = FindIndex(carLicence);
                if (Parking[index].Contains("CARϴ"))
                {


                    for (int i = 0; i < Parking.Length; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\"{carLicence}\" is located at parking spot number: {index + 1}"); 
                        Console.Write("Enter Desired Parking Spot: ");
                        int newSpot = int.Parse(Console.ReadLine());
                        if (Parking[newSpot - 1] == Parking[index])
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Your vehicle is already parked here.\nTry again.");
                            Thread.Sleep(2000);
                            MoveCar();
                        }
                        else if (Parking[newSpot - 1] == null)
                        {
                            Parking[newSpot - 1] = "CARϴ" + carLicence;
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Parking[index] = null; //Make sure to not dublicate our existing car by making previous parking spot null.
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Clear();
                            Console.WriteLine($"{carLicence} has been moved to {newSpot}");
                            Thread.Sleep(2000);
                            MainMenu();
                            break;
                        }
                        else if (Parking[newSpot - 1].Contains("MCϴ") || Parking[newSpot - 1].Contains("CARϴ"))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Parking space is taken. Choose another.");
                            Thread.Sleep(2000);
                            MoveCar();
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\nLicence Plate Number \"{carLicence}\" belongs to a MC. You are trying to move a Car");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPlease Choose:\n[1] to Park a Car.\n[2] To park a MC.\n[0] To go back to Main Menu.");

                    UserCantParkCorrect();
                }
            }
            else
            {
                Console.WriteLine("Incorrect input, Please Choose:\n[1] To Park a Car.\n[2] To park a MC.\n[0] To go back to Main Menu.");
                UserCantParkCorrect();
            }
        }
        public static void MoveMC()
        {
            Console.Clear();
            Console.Write("Enter license plate number:");
            string mcLicence = Console.ReadLine().ToUpper();

            if (SearchLicence(mcLicence))
            {
                int index = FindIndex(mcLicence);
                if (Parking[index].Contains("MCϴ"))
                {
                    for (int i = 0; i < Parking.Length; i++)
                    {
                        Console.Write("Enter new parkingSpot:");
                        int parkingSpace = UserParsing(Console.ReadLine());
                        if (Parking[index].Contains("|"))
                        {
                            string[] mc = Parking[index].Split("|");
                            foreach (var vehicle in mc)
                            {
                                if (mc[0] == "MCϴ" + mcLicence)
                                {
                                    Parking[parkingSpace - 1] += "|" + mc[0];
                                    Parking[index] = mc[1];
                                    //mc[0] = null;
                                    Console.WriteLine($"Moving vehicles {mcLicence} to parking spot {parkingSpace}\nPress any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    MainMenu(); 
                                    break;
                                }
                                if (mc[1] == "MCϴ" + mcLicence)
                                {
                                    Parking[parkingSpace - 1] += "|" + mc[1];
                                    Parking[index] = mc[0]; //Keeps other motorcycle that isnt going to be moved.

                                    Console.WriteLine($"Moving vehicles {mcLicence} to parking spot {parkingSpace}\nPress any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    MainMenu();
                                    break;

                                }
                            }
                        }
                        else if (Parking[parkingSpace - 1] == null)
                        {
                            Parking[parkingSpace - 1] = "MCϴ" + mcLicence;
                            Parking[index] = "Empty";
                            Console.WriteLine($"Moving vehicles {mcLicence} to parking spot {parkingSpace}\nPress any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            MainMenu();
                            break;
                        }
                        else if (Parking[parkingSpace - 1].Contains("CARϴ"))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("This spot is allocated to a car, please choose another spot\nPress any key to continue back...");
                            Console.ReadKey();
                            Console.Clear();
                            MoveMC();
                        }
                        else if (Parking[parkingSpace - 1] == Parking[i])
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Your vehicle is already parked here.\nPress any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            MainMenu();

                        }
                        else if (Parking[parkingSpace - 1].Contains("MCϴ"))
                        {
                            string temp;
                            string seperator = "|MCϴ";
                            temp = string.Join(seperator, Parking[parkingSpace - 1], mcLicence);
                            Parking[parkingSpace - 1] = temp;
                            Parking[index] = null;
                            Console.WriteLine($"Moved vehicle {mcLicence} too parking spot {parkingSpace}\nPress any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            MainMenu();
                            break;
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("This license plate number is for car. You are trying to move a Mc.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu();
                }
            }
        }
        static void RemoveVehicle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\nChoose a vehicle to remove.\nYou dont need to type MCϴ for MC or CARϴ for Car, the system will take care of that for you." +
                "\nLicence Number To Remove: ");

            string wantedRemoval = Console.ReadLine().ToUpper();

            for (int i = 0; i < Parking.Length; i++)
            {
                if (Parking[i] == null)
                {
                    continue;
                }
                else if (Parking[i] == "CARϴ" + wantedRemoval || Parking[i] == "MCϴ" + wantedRemoval) 
                {
                    Parking[i] = null; 

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{wantedRemoval} has been removed.\nRedirecting you to Main Menu.");
                    Thread.Sleep(3000);
                    MainMenu();
                    break;
                }
                else if (Parking[i].Contains("|") && Parking[i].Contains("MCϴ" + wantedRemoval))
                {
                    string[] parkingSpace = Parking[i].Split("|");
                    foreach (var vehicle in parkingSpace)
                    {
                        if (parkingSpace[0] == "MCϴ" + wantedRemoval)
                        {
                            parkingSpace[0] = null;
                            Parking[i] = parkingSpace[1];

                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine();

                            Console.WriteLine($"{wantedRemoval} has been removed.\nRedirecting you to Main Menu.");
                            Thread.Sleep(3000);
                            MainMenu();
                            break;
                        }
                        else if (parkingSpace[1] == "MCϴ" + wantedRemoval)
                        {
                            parkingSpace[1] = null;
                            Parking[i] = parkingSpace[0];

                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"{wantedRemoval} has been removed.\nRedirecting you to Main Menu.");
                            Thread.Sleep(3000);
                            MainMenu();
                            break;
                        }
                    }
                }
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{wantedRemoval} isnt found in the system. Maybe it's already removed?\nReturning to Main Menu.");
            Thread.Sleep(3000);
            MainMenu();
        }

        static string CheckUserLicence(string input) //Looks for correct input for searched licence
        {
            while (true)
            {
                if (input == null)
                {
                    Console.WriteLine("A licence plate can't be empty.");
                    input = Console.ReadLine();
                }

                if (input.Length <= 10 && input.Length >= 4)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid license plate input. (4-10 characters)");
                    Console.Write("Enter license plate nr: ");
                    input = Console.ReadLine();
                }
            }
        }

        static bool SearchLicence(string userInput) //true or false value wether or not the licence can be found in Array[].
        {
            userInput.ToUpper();

            for (int i = 0; i < Parking.Length; i++)
            {
                if (Parking[i] != null && Parking[i].Contains(userInput))
                {
                    return true;
                }
            }
            return false;
        }
        static int FindIndex(string userInput) //Look directly at the index where Licence Plate nr should be within Array[].
        {
            for (int i = 0; i < Parking.Length; i++)
            {
                if (Parking[i] != null && Parking[i].Contains(userInput))
                {
                    int index = i;
                    return index;
                }
            }
            return 0;
        }
        static int UserParsing(string input) //Makes sure user enters 1-100 when choosing desired parking.
        {

            while (true)
            {
                bool validInput = Int32.TryParse(input, out number);

                if (validInput && number <= 100 && number > 0)
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Please choose from 1-100.");
                    input = Console.ReadLine();
                }
            }
           
        }
        static int SmallUserParsing(string input) //This is only used in UserCantParkCorrect Method. Exception handling for swith-case.
        {
            while (true)
            {
                bool validInput = Int32.TryParse(input, out number);

                if (validInput && number <= 2 && number > -1)
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Choose from the options given.\nPlease and thank you.");
                    input = Console.ReadLine();
                }
            }
        }
        static void UserCantParkCorrect() //For whenever user makes an error choosing a parking space from "Move" method.
        {
            cantPark = SmallUserParsing(Console.ReadLine());
            switch (cantPark)
            {
                case 1:
                    MoveCar();
                    break;

                case 2:
                    MoveMC();
                    break;

                case 0:
                    MainMenu();
                    break;
            }
        }

        static void Exit()
        {
            Console.WriteLine();
        }
    }
}
