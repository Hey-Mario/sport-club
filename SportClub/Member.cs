using System;
using System.Collections.Generic;

namespace SportClub
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        public override string ToString()
        {
            return $"ID: {Id}, First Name: {FirstName}, Last Name: {LastName}, Email: {Email}, Phone: {Phone}";
        }
    }
}
