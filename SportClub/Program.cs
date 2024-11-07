using System;

namespace SportClub
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Database.EnsureCreated();  // This creates the database if it does not exist

                    // List all tables statically
                    var tableNames = new List<string> { "Members", "Subscriptions", "Sports", "MemberShips" };

                    while (true)  // Loop to allow multiple operations
                    {
                        Console.WriteLine("Voici les tables disponibles:");
                        foreach (var tableName in tableNames)
                        {
                            Console.WriteLine("> " + tableName);
                        }

                        Console.WriteLine("\nEntrez le nom de la table que vous souhaitez manipuler:");
                        var selectedTable = Console.ReadLine();

                        Console.WriteLine("Select an action:");
                        Console.WriteLine("1. Add");
                        Console.WriteLine("2. List");
                        Console.WriteLine("3. Update");
                        Console.WriteLine("4. Create");
                        Console.WriteLine("5. Find by ID");
                        Console.WriteLine("6. Exit");
                        Console.WriteLine("Enter the number of the action you want to perform:");
                        var action = Console.ReadLine();

                        if (action == "6")
                        {
                            Console.WriteLine("Exiting program...");
                            break;  // Exit the loop and end the program
                        }

                        HandleTableAction(context, selectedTable, action);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void PerformActions<T>(IService<T> service, string action) where T : class
        {
            switch (action)
            {
                case "1":
                    Console.WriteLine("Adding new entry...");
                    service.Create();
                    break;
                case "2":
                    Console.WriteLine("Listing all entries...");
                    var entries = service.ReadAll();
                    foreach (var entry in entries)
                    {
                        Console.WriteLine(entry.ToString());  // Ensure your classes override ToString() or adjust this line accordingly
                    }
                    break;
                case "3":
                    Console.WriteLine("Updating an entry...");
                    service.Update();
                    break;
                case "4":
                    Console.WriteLine("Creating a new entry...");
                    service.Create();
                    break;
                case "5":
                    Console.WriteLine("Finding by ID...");
                    Console.WriteLine("Enter ID:");
                    int id;
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        var item = service.ReadById(id);
                        if (item != null)
                        {
                            Console.WriteLine("Found: " + item.ToString());  // Ensure your classes override ToString() or adjust this line accordingly
                        }
                        else
                        {
                            Console.WriteLine("No entry found with that ID.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format.");
                    }
                    break;
                default:
                    Console.WriteLine("Action not recognized.");
                    break;
            }
        }

        static void HandleTableAction(AppDbContext context, string tableName, string action)
        {
            switch (tableName.ToLower())
            {
                case "memberships":
                    var membershipService = new MembershipService(context);
                    PerformActions(membershipService, action);
                    break;
                case "members":
                    var memberService = new MemberService(context);
                    PerformActions(memberService, action);
                    break;
                case "sports":
                    var sportService = new SportService(context);
                    PerformActions(sportService, action);
                    break;
                case "subscriptions":
                    var subscriptionService = new SubscriptionService(context);
                    PerformActions(subscriptionService, action);
                    break;
                default:
                    Console.WriteLine("Table not recognized.");
                    break;
            }
        }
    }
}