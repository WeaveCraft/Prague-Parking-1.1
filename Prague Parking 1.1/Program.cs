using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Prague_Parking
{
    class Program
    {
        static string[] Parking = new string[100];
        static int number, cantPark, lineSpace, indexSpace;
        static string temp;

        //--------------------------------------------------------------------------------Jag har uppdaterat "CheckUserLicence" för att säkra data från användaren. "CheckUserLicence" finns på kodrad 764--------------------------------------------------------------------------------

        static void MainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("[1] Add vehicle");
            Console.WriteLine("[2] Move vehicle");
            Console.WriteLine("[3] Remove vehicle");
            Console.WriteLine("[4] Look for vehicle");
            Console.WriteLine("[5] List of parked vehicles (1-100)");
            Console.WriteLine("[6] Exit Program");

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
                        PrintVehicles();
                        break;
                    case 6:
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
            string carLicence = CheckUserLicence(Console.ReadLine().ToUpper());

            if (SearchForVehicle(carLicence))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine($"{carLicence} is already parked. Maybe you wish to move or remove it?\nReturning to Main Menu...");
                Thread.Sleep(3000);
                MainMenu();
            }
            else if (true)
            {
                for (int i = 0; i < Parking.Length; i++)
                {
                    if (Parking[i] == null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        string removeWhiteSpace = Regex.Replace(carLicence, @"\s+", "");
                       
                        Console.WriteLine($"{removeWhiteSpace} is now parked.\nReturning to Main Menu...");

                        addSpace = "CARϴ" + removeWhiteSpace;
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
            
        }

        static void AddMC()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            string addSpace = string.Empty;
            int emptySpace = SpaceExist();

            Console.Write("Enter Licence Plate Number for MC: ");
            string mcLicence = CheckUserLicence(Console.ReadLine().ToUpper());

            
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
                            string removeWhiteSpace = Regex.Replace(mcLicence, @"\s+", "");

                            string id = "|MCϴ";
                            string contain = string.Join(id, Parking[j], removeWhiteSpace);
                            Parking[j] = contain;

                            PrintVehiclesShowCase();

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine($"{removeWhiteSpace} has been parked. Retruning to Main Menu.\nPlease Wait.");

                            Thread.Sleep(2000);
                            MainMenu();
                            break;
                        }

                    }
                    if (Parking[j] == null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        string removeWhiteSpace = Regex.Replace(mcLicence, @"\s+", "");

                        addSpace = "MCϴ" + removeWhiteSpace;
                        AddSpace(emptySpace, addSpace);

                        PrintVehiclesShowCase();

                        Console.WriteLine($"{removeWhiteSpace} has been parked. Returning to Main Menu.\nPlease Wait.");

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
            space = space - 1; //If user is trying to park at 50, then since first index is a 0, we need to minus by 1 to actually add space at 50, and not 49.
            Parking[space] = vehicle;
        }
        static int SpaceExist() //Look if there's still space in Parking[]
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
            var hasNumber = new Regex(@"[0-9]");
            var hasChar = new Regex(@"[a-zA-Z]"); //Tillåtna tecken som -
            var specialChar = new Regex("^[a-öA-Ö0-9]*$"); // Icke tillåtna tecken. Såsom !()
            for (int i = 0; i < Parking.Length; i++)
            {
                if (Parking[i] != null)
                {
                    if (Parking[i].Contains("MCϴ" + input) || Parking[i].Contains("CARϴ" + input))
                    {
                        number = i;
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
            PrintVehiclesShowCase();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Licence Number of Car or Motorcycle");
            string licenceSearch = Console.ReadLine().ToUpper();
            PrintVehiclesHighLight(licenceSearch);


            for (int i = 0; i < Parking.Length; i++)
            {
                if (Parking[i] != null)
                {
                    if (Parking[i].Contains(licenceSearch) || Parking[i].Contains(licenceSearch))
                    {
                        if (Parking[i].Contains("MCϴ"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n{licenceSearch} can be found at parking spot number: {i + 1}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to return to Main Menu.");
                            Console.ReadKey();
                            MainMenu();
                            break;
                        }
                        else if (Parking[i].Contains("CARϴ"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n{licenceSearch} can be found at parking spot number: {i + 1}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to return to Main Menu.");
                            Console.ReadKey();
                            MainMenu();
                            break;
                        }
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
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
            PrintVehiclesShowCase();
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine($"Sorry, vehicle couldn't be found. Maybe {licenceSearch} is stolen?\nReturning to Main Menu...");
            Thread.Sleep(2500);
            MainMenu();
        }
        static void PrintVehiclesHighLight(string input) //This exists purely for "Search" Method.
        {
            indexSpace = 6;
            lineSpace = 1;
            Console.Clear();
            for (int i = 0; i < Parking.Length; i++)
            {
                if (lineSpace >= indexSpace && lineSpace % indexSpace == 0)
                {
                    Console.WriteLine();
                    lineSpace = 1;
                }
                if (Parking[i] == null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;

                    Console.Write(i + 1 + ": Empty \t");
                    lineSpace++;
                }
                else if (Parking[i].Contains("|MCϴ"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
                else if (Parking[i].Contains("CARϴ"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
                else if (Parking[i].Contains(input))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
            }
            Console.WriteLine("\n__________________________________________________________________________");
        }
        static void PrintVehicles()
        {
            indexSpace = 6;
            lineSpace = 1;
            Console.Clear();
            for (int i = 0; i < Parking.Length; i++)
            {
                if (lineSpace >= indexSpace && lineSpace % indexSpace == 0)
                {
                    Console.WriteLine();
                    lineSpace = 1;
                }
                if (Parking[i] == null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;

                    Console.Write(i + 1 + ": Empty \t");
                    lineSpace++;
                }
                else if (Parking[i].Contains("|MCϴ"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
                else if (Parking[i].Contains("CARϴ"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
            }
            Console.WriteLine("\n__________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Blue = Empty");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Yellow = Half Full");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Dark Yellow = Full");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nPress any key to return to the menu...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }

        static void PrintVehiclesShowCase() //This purely exists to print out a menu to show where vehicles are parked for "Move & Remove" functions.
        {
            indexSpace = 6;
            lineSpace = 1;
            Console.Clear();
            for (int i = 0; i < Parking.Length; i++)
            {
                if (lineSpace >= indexSpace && lineSpace % indexSpace == 1)
                {
                    Console.WriteLine();
                    lineSpace = 1;
                }
                if (Parking[i] == null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;

                    Console.Write(i + 1 + ": Empty \t");
                    lineSpace++;
                }
                else if (Parking[i].Contains("|"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
                else if (Parking[i].Contains("CARϴ"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write(i + 1 + ": " + Parking[i] + "\t");
                    lineSpace++;
                }
            }
            Console.WriteLine("\n__________________________________________________________________________");


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
                    Console.WriteLine("Please choose 1 or 2");
                    break;
            }
        }

        static void MoveCar()
        {
            PrintVehiclesShowCase();
            Console.Write("Enter license plate number:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[CARϴ]");
            string trim = CheckUserLicence(Console.ReadLine().ToUpper());
            string carLicence = Regex.Replace(trim, @"\s+", "");

            if (SearchLicence(carLicence)) //To find if the Licence plate exists.
            {
                int index = FindIndex(carLicence); //To find what number the parking spot has.
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
                            PrintVehiclesShowCase();
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
            PrintVehiclesShowCase();
            Console.Write("Enter license plate number:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[MCϴ]");
            string trim = CheckUserLicence(Console.ReadLine().ToUpper());
            string mcLicence = Regex.Replace(trim, @"\s+", "");

            if (SearchLicence(mcLicence))
            {
                int index = FindIndex(mcLicence);
                if (Parking[index].Contains("MCϴ"))
                {
                    for (int i = 0; i < Parking.Length; i++)
                    {
                        Console.Write("Enter new parking spot:");
                        int parkingSpace = UserParsing(Console.ReadLine());
                        if (Parking[index].Contains("|"))
                        {
                            string[] mc = Parking[index].Split("|"); //Array to be able to seperate our two MC that are joined.
                            foreach (var vehicle in mc)
                            {
                                if (mc[0] == "MCϴ" + mcLicence) //First MC
                                {
                                    Parking[parkingSpace - 1] += "|" + mc[0];
                                    Parking[index] = mc[1]; //Keeps other motorcycle that isnt going to be moved.
                                    PrintVehiclesShowCase();
                                    Console.WriteLine($"Moving vehicles {mcLicence} to parking spot {parkingSpace}\nPress any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    MainMenu();
                                    break;
                                }
                                if (mc[1] == "MCϴ" + mcLicence) //Second MC
                                {
                                    Parking[parkingSpace - 1] += "|" + mc[1];
                                    Parking[index] = mc[0]; //Keeps other motorcycle that isnt going to be moved.

                                    PrintVehiclesShowCase();
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
                            Parking[index] = null;
                            PrintVehiclesShowCase();
                            Console.WriteLine($"Moving vehicles {mcLicence} to parking spot {parkingSpace}\nReturning to Main Menu...");
                            Thread.Sleep(2500);
                            MainMenu();
                            break;
                        }
                        else if (Parking[parkingSpace - 1].Contains("CARϴ"))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("A car is already parked here, please choose another spot");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (Parking[parkingSpace - 1] == Parking[parkingSpace - 1])
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Cant park here.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Try again");
                        }
                        else if (Parking[parkingSpace - 1].Contains("MCϴ"))
                        {
                            string seperator = "|MCϴ";
                            temp = string.Join(seperator, Parking[parkingSpace - 1], mcLicence);
                            Parking[parkingSpace - 1] = temp;
                            Parking[index] = null;
                            PrintVehiclesShowCase();
                            Console.WriteLine($"Moved vehicle {mcLicence} too parking spot {parkingSpace}\nReturning to Main Menu...");
                            Thread.Sleep(2500);
                            MainMenu();
                            break;
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Licence plate number: \"{Parking[index]}\" is for a car. You are trying to move a Mc.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu();
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Licence Plate couldnt be found.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPlease Choose:\n[1] to Park a Car.\n[2] To park a MC.\n[0] To go back to Main Menu.");
            Thread.Sleep(2000);
            UserCantParkCorrect();
        }
        static void RemoveVehicle()
        {
            Console.Clear();
            PrintVehiclesShowCase();
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

                    PrintVehiclesShowCase();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{wantedRemoval} has been removed.\nRedirecting you to Main Menu.");
                    Thread.Sleep(3000);
                    MainMenu();
                    break;
                }
                else if (Parking[i].Contains("|") && Parking[i].Contains("MCϴ" + wantedRemoval))
                {
                    string[] mc = Parking[i].Split("|"); //Create new array to find first mc in Parking[0] and second mc in Parking[1] to remove it. with split method we can see the string as an array.
                    foreach (var vehicle in mc)
                    {
                        if (mc[0] == "MCϴ" + wantedRemoval)
                        {
                            mc[0] = null;
                            Parking[i] = mc[1];

                            PrintVehiclesShowCase();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine();

                            Console.WriteLine($"{wantedRemoval} has been removed.\nRedirecting you to Main Menu.");
                            Thread.Sleep(3000);
                            MainMenu();
                            break;
                        }
                        else if (mc[1] == "MCϴ" + wantedRemoval)
                        {
                            mc[1] = null;
                            Parking[i] = mc[0];

                            PrintVehiclesShowCase();
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
            var hasNumber = new Regex(@"[0-9]");
            var hasChar = new Regex(@"[a-zA-Z-]");
            var regex = new Regex(@"[^a-zA-Z0-9\s]");
            while (true)
            {
                if(string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Licence Plate needs to be between 4-10 characters, no special characters allowed");
                    Console.WriteLine("Press any key to go back to park menu");
                    Console.ReadKey();
                    ParkMenu();
                }
                else if (regex.IsMatch(input) && input.Length <= 10 && input.Length >= 4 && hasNumber.IsMatch(input) && hasChar.IsMatch(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("Press any key to go back to park menu");
                    Console.ReadKey();
                    ParkMenu();
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
