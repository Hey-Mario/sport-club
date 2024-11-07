using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class MemberService : IService<Member>
    {
        private readonly AppDbContext _context;

        public MemberService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new member with user input
        public void Create()
        {
            var member = new Member();

            Console.WriteLine("Enter First Name:");
            member.FirstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name:");
            member.LastName = Console.ReadLine();

            Console.WriteLine("Enter Email:");
            member.Email = Console.ReadLine();

            Console.WriteLine("Enter Phone:");
            member.Phone = Console.ReadLine();

            _context.Member.Add(member);
            _context.SaveChanges();

            Console.WriteLine("Member added successfully!");
        }

        // Retrieve all members
        public List<Member> ReadAll()
        {
            return _context.Member.ToList();
        }

        // Retrieve a single member by ID
        public Member ReadById(int id)
        {
            return _context.Member.FirstOrDefault(m => m.Id == id);
        }

        // Update an existing member with user input
        public void Update()
        {
            Console.WriteLine("Enter Member ID to update:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var member = _context.Member.FirstOrDefault(m => m.Id == id);
            if (member == null)
            {
                Console.WriteLine("Member not found.");
                return;
            }

            Console.WriteLine("Enter First Name (current: " + member.FirstName + "):");
            var firstName = Console.ReadLine();
            if (!string.IsNullOrEmpty(firstName))
                member.FirstName = firstName;

            Console.WriteLine("Enter Last Name (current: " + member.LastName + "):");
            var lastName = Console.ReadLine();
            if (!string.IsNullOrEmpty(lastName))
                member.LastName = lastName;

            Console.WriteLine("Enter Email (current: " + member.Email + "):");
            var email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email))
                member.Email = email;

            Console.WriteLine("Enter Phone (current: " + member.Phone + "):");
            var phone = Console.ReadLine();
            if (!string.IsNullOrEmpty(phone))
                member.Phone = phone;

            _context.Member.Update(member);
            _context.SaveChanges();

            Console.WriteLine("Member updated successfully!");
        }

        // Delete a member
        public void Delete(int id)
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