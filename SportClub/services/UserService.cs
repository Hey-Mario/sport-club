using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class UserService : IService<User>
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        
        public void Create()
        {
            var user = new User();

            Console.WriteLine("Enter Email:");
            user.Email = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            user.Password = Console.ReadLine();  

            _context.User.Add(user);
            _context.SaveChanges();

            Console.WriteLine("User added successfully!");
        }

        
        public List<User> ReadAll()
        {
            return _context.User.ToList();
        }

        
        public User ReadById(int id)
        {
            return _context.User.FirstOrDefault(u => u.Id == id);
        }

        
        public void Update()
        {
            Console.WriteLine("Enter User ID to update:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var user = _context.User.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("Enter Email (current: " + user.Email + "):");
            var email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email))
                user.Email = email;

            Console.WriteLine("Enter Password:");
            user.Password = Console.ReadLine();  

            _context.User.Update(user);
            _context.SaveChanges();

            Console.WriteLine("User updated successfully!");
        }

        
        public void Delete(int id)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        
        public bool Login()
        {
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();  

            var user = _context.User.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                Console.WriteLine("Login successful.");
                return true;
            }
            else
            {
                Console.WriteLine("Login failed.");
                return false;
            }
        }
    }
}