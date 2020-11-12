using System.Collections.Generic;

namespace BB.DAL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public List<Card> Cards { get; set; }
    }
}