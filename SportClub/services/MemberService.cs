using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class MemberService
    {
        private readonly AppDbContext _context;

        public MemberService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new member
        public void AddMember(Member member)
        {
            _context.Member.Add(member);
            _context.SaveChanges();
        }

        // Retrieve all members
        public List<Member> GetAllMembers()
        {
            return _context.Member.ToList();
        }

        // Retrieve a single member by ID
        public Member GetMemberById(int id)
        {
            return _context.Member.FirstOrDefault(m => m.Id == id);
        }

        // Update an existing member
        public void UpdateMember(Member member)
        {
            _context.Member.Update(member);
            _context.SaveChanges();
        }

        // Delete a member
        public void DeleteMember(int id)
        {
            var member = _context.Member.Find(id);
            if (member != null)
            {
                _context.Member.Remove(member);
                _context.SaveChanges();
            }
        }

        public List<MemberInfo> SearchInformation(string name)
        {
            var members = _context.Member
                .Where(m => m.FirstName.Contains(name) || m.LastName.Contains(name))
                .Select(m => new MemberInfo
                {
                    MemberId = m.Id,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Email = m.Email,
                    Phone = m.Phone,
                    Membership = m.Subscriptions.OrderByDescending(s => s.StartDate)
                                .Select(s => s.Membership).FirstOrDefault(),
                    Sport = m.Subscriptions.OrderByDescending(s => s.StartDate)
                                .Select(s => s.Membership.Sport.Name).FirstOrDefault(),
                    Subscriptions = m.Subscriptions.ToList()
                })
                .ToList();

            return members;
        }
    }
}