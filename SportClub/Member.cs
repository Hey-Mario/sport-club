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
    }

    public class CreateMemberDto : Member
    {
        public int MemberShipId { get; set; }
    }
}
