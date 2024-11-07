using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class SportService : IService<Sport>
    {
        private readonly AppDbContext _context;

        public SportService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new sport with user input
        public void Create()
        {
            var sport = new Sport();

            Console.WriteLine("Enter Sport Name:");
            sport.Name = Console.ReadLine();

            Console.WriteLine("Enter Sport Description:");
            sport.Description = Console.ReadLine();

            _context.Sport.Add(sport);
            _context.SaveChanges();

            Console.WriteLine("Sport added successfully!");
        }

        // Retrieve all sports
        public List<Sport> ReadAll()
        {
            return _context.Sport.ToList();
        }

        // Retrieve a single sport by ID
        public Sport ReadById(int id)
        {
            return _context.Sport.FirstOrDefault(s => s.Id == id);
        }

        // Update an existing sport with user input
        public void Update()
        {
            Console.WriteLine("Enter Sport ID to update:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var sport = _context.Sport.FirstOrDefault(s => s.Id == id);
            if (sport == null)
            {
                Console.WriteLine("Sport not found.");
                return;
            }

            Console.WriteLine("Enter Sport Name (current: " + sport.Name + "):");
            var name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
                sport.Name = name;

            Console.WriteLine("Enter Sport Description (current: " + sport.Description + "):");
            var description = Console.ReadLine();
            if (!string.IsNullOrEmpty(description))
                sport.Description = description;

            _context.Sport.Update(sport);
            _context.SaveChanges();

            Console.WriteLine("Sport updated successfully!");
        }

        // Delete a sport
        public void Delete(int id)
        {
            var sport = _context.Sport.Find(id);
            if (sport != null)
            {
                _context.Sport.Remove(sport);
                _context.SaveChanges();
                Console.WriteLine("Sport deleted successfully.");
            }
            else
            {
                Console.WriteLine("Sport not found.");
            }
        }
    }
}