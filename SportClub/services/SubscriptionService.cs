using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class SubscriptionService
    {
        private readonly AppDbContext _context;

        public SubscriptionService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new subscription
        public void AddSubscription(Subscription subscription)
        {
            _context.Subscription.Add(subscription);
            _context.SaveChanges();
        }

        // Retrieve all subscriptions
        public List<Subscription> GetAllSubscriptions()
        {
            return _context.Subscription.ToList();
        }

        // Retrieve a single subscription by ID
        public Subscription GetSubscriptionById(int id)
        {
            return _context.Subscription.FirstOrDefault(s => s.Id == id);
        }

        // Update an existing subscription
        public void UpdateSubscription(Subscription subscription)
        {
            _context.Subscription.Update(subscription);
            _context.SaveChanges();
        }

        // Delete a subscription
        public void DeleteSubscription(int id)
        {
            var subscription = _context.Subscription.Find(id);
            if (subscription != null)
            {
                _context.Subscription.Remove(subscription);
                _context.SaveChanges();
            }
        }
    }
}