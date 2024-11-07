using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class MembershipService : IService<Membership>
    {
        private readonly AppDbContext _context;

        public MembershipService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new membership with user input
        public void Create()
        {
            var membership = new Membership();

            Console.WriteLine("Enter Membership Type:");
            membership.Type = Console.ReadLine();

            Console.WriteLine("Enter Price:");
            membership.Price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter Duration in Months:");
            membership.DurationMonths = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Sport ID:");
            membership.SportId = int.Parse(Console.ReadLine());

            _context.Membership.Add(membership);
            _context.SaveChanges();

            Console.WriteLine("Membership added successfully!");
        }

        // Retrieve all memberships
        public List<Membership> ReadAll()
        {
            return _context.Membership.ToList();
        }

        // Retrieve a single membership by ID
        public Membership ReadById(int id)
        {
            return _context.Membership.FirstOrDefault(m => m.Id == id);
        }

        // Update an existing membership with user input
        public void Update()
        {
            Console.WriteLine("Enter Membership ID to update:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var membership = _context.Membership.FirstOrDefault(m => m.Id == id);
            if (membership == null)
            {
                Console.WriteLine("Membership not found.");
                return;
            }

            Console.WriteLine("Enter Membership Type (current: " + membership.Type + "):");
            var type = Console.ReadLine();
            if (!string.IsNullOrEmpty(type))
                membership.Type = type;

            Console.WriteLine("Enter Price (current: " + membership.Price + "):");
            var price = Console.ReadLine();
            if (!string.IsNullOrEmpty(price))
                membership.Price = decimal.Parse(price);

            Console.WriteLine("Enter Duration in Months (current: " + membership.DurationMonths + "):");
            var durationMonths = Console.ReadLine();
            if (!string.IsNullOrEmpty(durationMonths))
                membership.DurationMonths = int.Parse(durationMonths);

            Console.WriteLine("Enter Sport ID (current: " + membership.SportId + "):");
            var sportId = Console.ReadLine();
            if (!string.IsNullOrEmpty(sportId))
                membership.SportId = int.Parse(sportId);

            _context.Membership.Update(membership);
            _context.SaveChanges();

            Console.WriteLine("Membership updated successfully!");
        }

        // Delete a membership
        public void Delete(int id)
        {
            var membership = _context.Membership.Find(id);
            if (membership != null)
            {
                _context.Membership.Remove(membership);
                _context.SaveChanges();
                Console.WriteLine("Membership deleted successfully.");
            }
            else
            {
                Console.WriteLine("Membership not found.");
            }
        }
    }
}