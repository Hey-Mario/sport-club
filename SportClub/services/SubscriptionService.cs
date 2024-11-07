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

        
        public void AddSubscription(Subscription subscription)
        {
            _context.Subscription.Add(subscription);
            _context.SaveChanges();
        }

        
        public List<Subscription> GetAllSubscriptions()
        {
            return _context.Subscription.ToList();
        }

        
        public Subscription GetSubscriptionById(int id)
        {
            return _context.Subscription.FirstOrDefault(s => s.Id == id);
        }

        
        public void UpdateSubscription(Subscription subscription)
        {
            _context.Subscription.Update(subscription);
            _context.SaveChanges();
        }

        
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