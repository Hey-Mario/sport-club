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

                    // Services instantiation
                    var sportService = new SportService(context);
                    var membershipService = new MembershipService(context);
                    var memberService = new MemberService(context);
                    var subscriptionService = new SubscriptionService(context);
                    var userService = new UserService(context);

                    // Adding a new sport
                    var newSport = new Sport { Name = "Tennis", Description = "A sport for two or four people" };
                    sportService.AddSport(newSport);

                    // Adding a new membership
                    var newMembership = new Membership { Type = "Annual", Price = 200, DurationMonths = 12, SportId = newSport.Id };
                    membershipService.AddMembership(newMembership);

                    // Adding a new member
                    var newMember = new Member { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "1234567890" };
                    memberService.AddMember(newMember);

                    // Adding a new subscription
                    var newSubscription = new Subscription { MemberId = newMember.Id, MembershipId = newMembership.Id, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1) };
                    subscriptionService.AddSubscription(newSubscription);

                    // Adding a new user
                    var newUser = new User { Email = "user@example.com", Password = "securepassword123" };
                    userService.AddUser(newUser);

                    // Displaying all sports
                    var sports = sportService.GetAllSports();
                    foreach (var sport in sports)
                    {
                        Console.WriteLine($"{sport.Name} - {sport.Description}");
                    }

                    // Displaying all memberships
                    var memberships = membershipService.GetAllMemberships();
                    foreach (var membership in memberships)
                    {
                        Console.WriteLine($"{membership.Type} - ${membership.Price}");
                    }

                    // Displaying all members
                    var members = memberService.GetAllMembers();
                    foreach (var member in members)
                    {
                        Console.WriteLine($"{member.FirstName} {member.LastName} - {member.Email}");
                    }

                    // Displaying all subscriptions
                    var subscriptions = subscriptionService.GetAllSubscriptions();
                    foreach (var subscription in subscriptions)
                    {
                        Console.WriteLine($"Subscription for Member ID: {subscription.MemberId} with Membership ID: {subscription.MembershipId} from {subscription.StartDate.ToShortDateString()} to {subscription.EndDate.ToShortDateString()}");
                    }

                    // Displaying all users
                    var users = userService.GetAllUsers();
                    foreach (var user in users)
                    {
                        Console.WriteLine($"{user.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}