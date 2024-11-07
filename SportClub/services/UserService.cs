using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new user
        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        // Retrieve all users
        public List<User> GetAllUsers()
        {
            return _context.User.ToList();
        }

        // Retrieve a single user by ID
        public User GetUserById(int id)
        {
            return _context.User.FirstOrDefault(u => u.Id == id);
        }

        // Update an existing user
        public void UpdateUser(User user)
        {
            _context.User.Update(user);
            _context.SaveChanges();
        }

        // Delete a user
        public void DeleteUser(int id)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }
        }

        // Login user
        public bool Login(string email, string password)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user != null;
        }
    }
} 