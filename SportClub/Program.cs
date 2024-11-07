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
                    context.Database.EnsureCreated();
                    var memberService = new MemberService(context);

                    var memberInfos = memberService.SearchInformation("John");
                    foreach (var info in memberInfos)
                    {
                        Console.WriteLine($"Member: {info.FirstName} {info.LastName}, Email: {info.Email}");
                        Console.WriteLine($"Membership Type: {info.Membership.Type}, Sport: {info.Sport}");
                        foreach (var sub in info.Subscriptions)
                        {
                            Console.WriteLine($"Subscription: {sub.StartDate.ToShortDateString()} to {sub.EndDate.ToShortDateString()}");
                        }
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