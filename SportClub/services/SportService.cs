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

        
        public void AddSport(Sport sport)
        {
            _context.Sport.Add(sport);
            _context.SaveChanges();
        }

        
        public List<Sport> GetAllSports()
        {
            return _context.Sport.ToList();
        }

        
        public Sport GetSportById(int id)
        {
            return _context.Sport.FirstOrDefault(s => s.Id == id);
        }

        
        public void UpdateSport(Sport sport)
        {
            _context.Sport.Update(sport);
            _context.SaveChanges();
        }

        
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