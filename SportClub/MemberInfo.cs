using System;

namespace SportClub
{
    public class MemberInfo
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Membership Membership { get; set; }
        public string Sport { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
