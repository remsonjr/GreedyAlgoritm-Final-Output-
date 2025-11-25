using System;
using System.Collections.Generic;
using System.Linq;

namespace ActivitySelectionGreedy
{
    // Activity class
    public class Activity
    {
        public int Id { get; set; }
        public int Start { get; set; }
        public int Finish { get; set; }

        public Activity(int id, int start, int finish)
        {
            Id = id;
            Start = start;
            Finish = finish;
        }
    }

    class Program
    {
        // Greedy Activity Selection Algorithm
        public static List<Activity> SelectActivities(List<Activity> activities)
        {
            // Sort by finish time
            var sorted = activities.OrderBy(a => a.Finish).ToList();

            List<Activity> selected = new List<Activity>();

            // First activity is always selected
            selected.Add(sorted[0]);
            int lastFinish = sorted[0].Finish;

            // Traverse remaining activities
            for (int i = 1; i < sorted.Count; i++)
            {
                if (sorted[i].Start >= lastFinish)
                {
                    selected.Add(sorted[i]);
                    lastFinish = sorted[i].Finish;
                }
            }

            return selected;
        }

        // Display activities
        public static void DisplayActivities(List<Activity> activities, string title)
        {
            Console.WriteLine("\n" + title);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"{"ID",4} {"Start",8} {"Finish",8}");
            Console.WriteLine("----------------------------------------------");

            foreach (var act in activities)
            {
                Console.WriteLine($"{act.Id,4} {act.Start,8} {act.Finish,8}");
            }

            Console.WriteLine("----------------------------------------------");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== GREEDY ALGORITHM: ACTIVITY SELECTION ===\n");

            // Predefined activities
            List<Activity> activities = new List<Activity>
            {
                new Activity(1, 1, 4),
                new Activity(2, 3, 5),
                new Activity(3, 0, 6),
                new Activity(4, 5, 7),
                new Activity(5, 3, 9),
                new Activity(6, 5, 9),
                new Activity(7, 6, 10),
                new Activity(8, 8, 11),
                new Activity(9, 8, 12),
                new Activity(10, 2, 14),
                new Activity(11, 12, 16)
            };

            // Display data before sorting
            DisplayActivities(activities, "ALL ACTIVITIES (Unsorted)");

            // Sort for display
            var sorted = activities.OrderBy(a => a.Finish).ToList();
            DisplayActivities(sorted, "ALL ACTIVITIES (Sorted by Finish Time)");

            // Get optimal set
            var selected = SelectActivities(activities);

            // Display selected activities
            DisplayActivities(selected, "SELECTED ACTIVITIES (Optimal Solution)");

            Console.WriteLine($"\nTotal Activities: {activities.Count}");
            Console.WriteLine($"Maximum Selected: {selected.Count}");

            Console.Write("Selected Order: ");
            Console.WriteLine(string.Join(" -> ", selected.Select(a => a.Id)));

            // User input option
            Console.Write("\nWould you like to enter custom activities? (y/n): ");
            char choice = Console.ReadKey().KeyChar;

            if (choice == 'y' || choice == 'Y')
            {
                Console.WriteLine();
                List<Activity> custom = new List<Activity>();

                Console.Write("\nEnter number of activities: ");
                int n = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < n; i++)
                {
                    Console.Write($"Activity {i + 1} Start: ");
                    int start = Convert.ToInt32(Console.ReadLine());

                    Console.Write($"Activity {i + 1} Finish: ");
                    int finish = Convert.ToInt32(Console.ReadLine());

                    custom.Add(new Activity(i + 1, start, finish));
                }

                var customSelected = SelectActivities(custom);

                DisplayActivities(customSelected, "\nCUSTOM SELECTED ACTIVITIES");

                Console.Write("Custom Selection Order: ");
                Console.WriteLine(string.Join(" -> ", customSelected.Select(a => a.Id)));
            }

            Console.WriteLine("\n\nProgram Completed Successfully!");
            Console.ReadLine();
        }
    }
}
