using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_161_MILESTONE_1
{
    //Enum for the menu 
    enum Menu
    {
        CaptureBookingRequests = 1,
        EvaluateBookingEligibility,
        DisplayBookingStatistics,
        Exit
    }

    class Booking
    {
        public string FullName { get; set; }
        public string StudentNumber { get; set; }
        public int YearOfStudy { get; set; }
        public string EquipmentType { get; set; }
        public double BookingDuration { get; set; }
        public bool TrainingCompleted { get; set; }
        public int ActiveBookings { get; set; }
        public string Status { get; set; } // "Approved", "Conditional", "Rejected"
    }

    internal class Program
    {
        static List<Booking> allBookings = new List<Booking>();
        static List<Booking> approvedBookings = new List<Booking>();
        static int rejectedCount = 0;
        static void Main(string[] args)
        {
            Menu choice;
            do
            {
                Console.WriteLine("\n=============================");
                Console.WriteLine("   TECHNOLAB BOOKING SYSTEM  ");
                Console.WriteLine("=============================");
                Console.WriteLine("1. Capture Booking Requests");
                Console.WriteLine("2. Evaluate Booking Eligibility");
                Console.WriteLine("3. Display Booking Statistics");
                Console.WriteLine("4. Exit");
                Console.WriteLine("=============================");
                Console.Write("Enter your choice: ");

                int input;
                while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please enter a number between 1 and 4.");
                    Console.ResetColor();
                    Console.Write("Enter your choice: ");
                }

                choice = (Menu)input;

                switch (choice)
                {
                    case Menu.CaptureBookingRequests:
                        allBookings = CaptureBookings();
                        break;
                    case Menu.EvaluateBookingEligibility:
                        EvaluateBookings();
                        break;
                    case Menu.DisplayBookingStatistics:
                        DisplayStatistics();
                        break;
                    case Menu.Exit:
                        Console.WriteLine("\nThank you for using TechnoLab. Goodbye!");
                        break;
                }

            } while (choice != Menu.Exit);
            //=============================================================
            // OPTION 1: Capture Booking Requests
            //=============================================================
        }
            static List<Booking> CaptureBookings()
            {
                string continueCapture;

                do
                {
                    Booking booking = new Booking();

                    Console.WriteLine("\n--- New Booking Entry ---");

                    // Full Name
                    Console.Write("Enter student full name: ");
                    booking.FullName = ToTitleCase(Console.ReadLine());

                    // Student Number
                    Console.Write("Enter student number: ");
                    booking.StudentNumber = Console.ReadLine().Trim().ToUpper();

                    // Year of Study
                    Console.Write("Enter year of study: ");
                    int year;
                    while (!int.TryParse(Console.ReadLine(), out year) || year < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid year of study.");
                        Console.ResetColor();
                        Console.Write("Enter year of study: ");
                    }
                    booking.YearOfStudy = year;

                    // Equipment Type
                    Console.WriteLine("Equipment options: Drone | VR Headset | Microcontroller | 3D Printer");
                    Console.Write("Enter equipment type: ");
                    booking.EquipmentType = ToTitleCase(Console.ReadLine());

                    // Booking Duration
                    Console.Write("Enter booking duration (in hours): ");
                    double duration;
                    while (!double.TryParse(Console.ReadLine(), out duration) || duration <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid duration.");
                        Console.ResetColor();
                        Console.Write("Enter booking duration (in hours): ");
                    }
                    booking.BookingDuration = duration;

                    // Training Completed
                    Console.Write("Has the student completed required training? (yes/no): ");
                    string trainingInput = Console.ReadLine().Trim().ToLower();
                    while (trainingInput != "yes" && trainingInput != "no")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter yes or no.");
                        Console.ResetColor();
                        Console.Write("Has the student completed required training? (yes/no): ");
                        trainingInput = Console.ReadLine().Trim().ToLower();
                    }
                    booking.TrainingCompleted = trainingInput == "yes";

                    // Active Bookings
                    Console.Write("Enter number of current active bookings: ");
                    int active;
                    while (!int.TryParse(Console.ReadLine(), out active) || active < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        Console.ResetColor();
                        Console.Write("Enter number of current active bookings: ");
                    }
                    booking.ActiveBookings = active;

                    // Add to list
                    allBookings.Add(booking);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nBooking captured successfully!");
                    Console.ResetColor();

                    // Ask if user wants to add another
                    Console.Write("\nWould you like to add another booking? (yes/no): ");
                    continueCapture = Console.ReadLine().Trim().ToLower();
                    while (continueCapture != "yes" && continueCapture != "no")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter yes or no.");
                        Console.ResetColor();
                        Console.Write("Would you like to add another booking? (yes/no): ");
                        continueCapture = Console.ReadLine().Trim().ToLower();
                    }

                } while (continueCapture == "yes");

                return allBookings;
            }

            //=============================================================
            // OPTION 2: Evaluate Booking Eligibility
            //=============================================================
            static void EvaluateBookings()
            {
                if (allBookings.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNo bookings to evaluate. Please capture bookings first.");
                    Console.ResetColor();
                    return;
                }

                // Reset before evaluating
                approvedBookings.Clear();
                rejectedCount = 0;

                foreach (Booking booking in allBookings)
                {
                    bool trainingDone = booking.TrainingCompleted;
                    bool withinActiveLimit = booking.ActiveBookings < 2;
                    bool durationApproved = booking.BookingDuration <= 4;
                    bool durationConditional = booking.BookingDuration == 5 || booking.BookingDuration == 6;
                    bool durationRejected = booking.BookingDuration > 6;
                    bool tooManyBookings = booking.ActiveBookings >= 3;

                    if (durationRejected || tooManyBookings)
                    {
                        // Hard rejection
                        booking.Status = "Rejected";
                        rejectedCount++;
                    }
                    else if (trainingDone && withinActiveLimit && durationApproved)
                    {
                        // Fully approved
                        booking.Status = "Approved";
                        approvedBookings.Add(booking);
                    }
                    else if (trainingDone && withinActiveLimit && durationConditional)
                    {
                        // Conditionally approved
                        booking.Status = "Conditional";
                        approvedBookings.Add(booking);
                    }
                    else
                    {
                        // Failed other conditions
                        booking.Status = "Rejected";
                        rejectedCount++;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nBookings evaluated successfully!");
                Console.ResetColor();
                Console.WriteLine($"Approved: {approvedBookings.Count} | Rejected: {rejectedCount}");
            }

            //=============================================================
            // OPTION 3: Display Booking Statistics
            //=============================================================
            static void DisplayStatistics()
            {
                if (allBookings.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNo bookings found. Please capture and evaluate bookings first.");
                    Console.ResetColor();
                    return;
                }

                Console.WriteLine("\n=============================");
                Console.WriteLine("      BOOKING STATISTICS     ");
                Console.WriteLine("=============================");
                Console.WriteLine($"Total Booking Requests : {allBookings.Count}");
                Console.WriteLine($"Approved Bookings      : {approvedBookings.Count}");
                Console.WriteLine($"Rejected Bookings      : {rejectedCount}");

                if (approvedBookings.Count > 0)
                {
                    // Sort by priority: Year of Study (descending), then Active Bookings (ascending), then Duration (ascending)
                    List<Booking> sortedBookings = approvedBookings
                        .OrderByDescending(b => b.YearOfStudy)
                        .ThenBy(b => b.ActiveBookings)
                        .ThenBy(b => b.BookingDuration)
                        .ToList();

                    Console.WriteLine("\n--- Approved Bookings (Priority Order) ---");
                    Console.WriteLine($"{"#",-4} {"Name",-25} {"Student No",-15} {"Year",-6} {"Equipment",-18} {"Duration",-10} {"Active",-8} {"Status",-15}");
                    Console.WriteLine(new string('-', 105));

                    int rank = 1;
                    foreach (Booking b in sortedBookings)
                    {
                        string statusDisplay = b.Status == "Conditional" ? "** CONDITIONAL **" : "Approved";

                        if (b.Status == "Conditional")
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        else
                            Console.ForegroundColor = ConsoleColor.Green;

                        Console.WriteLine($"{rank,-4} {b.FullName,-25} {b.StudentNumber,-15} {b.YearOfStudy,-6} {b.EquipmentType,-18} {b.BookingDuration + "h",-10} {b.ActiveBookings,-8} {statusDisplay,-15}");
                        Console.ResetColor();
                        rank++;
                    }

                    Console.WriteLine(new string('-', 105));
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("** CONDITIONAL = Management approval required");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("\nNo approved bookings to display.");
                }
            }

            //=============================================================
            // HELPER METHOD: Title Case formatter
            //=============================================================
            static string ToTitleCase(string input)
            {
                if (string.IsNullOrWhiteSpace(input)) return input;
                string[] words = input.Trim().ToLower().Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length > 0)
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
                return string.Join(" ", words);
            }
        
    

        
    }
}
