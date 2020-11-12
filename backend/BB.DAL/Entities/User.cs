using System.Collections.Generic;

namespace BB.DAL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        private List<Card> Cards { get; } = new List<Card>();
    }
}