using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class SportService
    {
        private readonly AppDbContext _context;

        public SportService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new sport
        public void AddSport(Sport sport)
        {
            _context.Sport.Add(sport);
            _context.SaveChanges();
        }

        // Retrieve all sports
        public List<Sport> GetAllSports()
        {
            return _context.Sport.ToList();
        }

        // Retrieve a single sport by ID
        public Sport GetSportById(int id)
        {
            return _context.Sport.FirstOrDefault(s => s.Id == id);
        }

        // Update an existing sport
        public void UpdateSport(Sport sport)
        {
            _context.Sport.Update(sport);
            _context.SaveChanges();
        }

        // Delete a sport
        public void DeleteSport(int id)
        {
            var sport = _context.Sport.Find(id);
            if (sport != null)
            {
                _context.Sport.Remove(sport);
                _context.SaveChanges();
            }
        }
    }
}