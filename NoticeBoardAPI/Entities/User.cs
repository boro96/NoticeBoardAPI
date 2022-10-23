using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; } 
        public string PasswordHash { get; set; }
        public string Nationality { get; set; }

        //Navigation Properties
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public List<Advert> Adverts { get; set; } = new List<Advert>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
