using System.Collections.Generic;

namespace BB.DAL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Card> Cards { get; set; }
    }
}