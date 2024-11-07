using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class SubscriptionService : IService<Subscription>
    {
        private readonly AppDbContext _context;

        public SubscriptionService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new subscription with user input
        public void Create()
        {
            var subscription = new Subscription();

            Console.WriteLine("Enter Member ID:");
            subscription.MemberId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Membership ID:");
            subscription.MembershipId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Start Date (yyyy-mm-dd):");
            subscription.StartDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter End Date (yyyy-mm-dd):");
            subscription.EndDate = DateTime.Parse(Console.ReadLine());

            _context.Subscription.Add(subscription);
            _context.SaveChanges();

            Console.WriteLine("Subscription added successfully!");
        }

        // Retrieve all subscriptions
        public List<Subscription> ReadAll()
        {
            return _context.Subscription.ToList();
        }

        // Retrieve a single subscription by ID
        public Subscription ReadById(int id)
        {
            return _context.Subscription.FirstOrDefault(s => s.Id == id);
        }

        // Update an existing subscription with user input
        public void Update()
        {
            Console.WriteLine("Enter Subscription ID to update:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var subscription = _context.Subscription.FirstOrDefault(s => s.Id == id);
            if (subscription == null)
            {
                Console.WriteLine("Subscription not found.");
                return;
            }

            Console.WriteLine("Enter Member ID (current: " + subscription.MemberId + "):");
            var memberId = Console.ReadLine();
            if (!string.IsNullOrEmpty(memberId))
                subscription.MemberId = int.Parse(memberId);

            Console.WriteLine("Enter Membership ID (current: " + subscription.MembershipId + "):");
            var membershipId = Console.ReadLine();
            if (!string.IsNullOrEmpty(membershipId))
                subscription.MembershipId = int.Parse(membershipId);

            Console.WriteLine("Enter Start Date (current: " + subscription.StartDate.ToString("yyyy-MM-dd") + "):");
            var startDate = Console.ReadLine();
            if (!string.IsNullOrEmpty(startDate))
                subscription.StartDate = DateTime.Parse(startDate);

            Console.WriteLine("Enter End Date (current: " + subscription.EndDate.ToString("yyyy-MM-dd") + "):");
            var endDate = Console.ReadLine();
            if (!string.IsNullOrEmpty(endDate))
                subscription.EndDate = DateTime.Parse(endDate);

            _context.Subscription.Update(subscription);
            _context.SaveChanges();

            Console.WriteLine("Subscription updated successfully!");
        }

        // Delete a subscription
        public void Delete(int id)
        {
            var subscription = _context.Subscription.Find(id);
            if (subscription != null)
            {
                _context.Subscription.Remove(subscription);
                _context.SaveChanges();
                Console.WriteLine("Subscription deleted successfully.");
            }
            else
            {
                Console.WriteLine("Subscription not found.");
            }
        }
    }
}