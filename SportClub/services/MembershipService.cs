using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class MembershipService
    {
        private readonly AppDbContext _context;

        public MembershipService(AppDbContext context)
        {
            _context = context;
        }

        
        public void AddMembership(Membership membership)
        {
            _context.Membership.Add(membership);
            _context.SaveChanges();
        }

        
        public List<Membership> GetAllMemberships()
        {
            return _context.Membership.ToList();
        }

        
        public Membership GetMembershipById(int id)
        {
            return _context.Membership.FirstOrDefault(m => m.Id == id);
        }

        
        public void UpdateMembership(Membership membership)
        {
            _context.Membership.Update(membership);
            _context.SaveChanges();
        }

        
        public void DeleteMembership(int id)
        {
            var membership = _context.Membership.Find(id);
            if (membership != null)
            {
                _context.Membership.Remove(membership);
                _context.SaveChanges();
            }
        }
    }
}