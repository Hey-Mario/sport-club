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

        
        public void AddMember(CreateMemberDto member)
        {
            
            _context.Member.Add(member);
            _context.SaveChanges(); 

            
            var membership = _context.Membership.FirstOrDefault(m => m.Id == member.MemberShipId);
            if (membership == null)
            {
                throw new Exception("Membership not found.");
            }

            
            var endDate = DateTime.Now.AddMonths(membership.DurationMonths);

            
            var subscription = new Subscription
            {
                MemberId = member.Id,
                MembershipId = member.MemberShipId,
                StartDate = DateTime.Now,
                EndDate = endDate,
                Member = member,
                Membership = membership
            };

            
            _context.Subscription.Add(subscription);

            
            _context.SaveChanges();
        }

        
        public List<Member> GetAllMembers()
        {
            return _context.Member.ToList();
        }

        
        public Member GetMemberById(int id)
        {
            return _context.Member.FirstOrDefault(m => m.Id == id);
        }

        
        public void UpdateMember(Member member)
        {
            _context.Member.Update(member);
            _context.SaveChanges();
        }

        
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