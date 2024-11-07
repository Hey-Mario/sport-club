using System;

namespace SportClub
{
    public class Membership
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
        public int SportId { get; set; }

        public Sport Sport { get; set; }
    }
}