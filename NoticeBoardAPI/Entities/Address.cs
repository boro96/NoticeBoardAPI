using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ApartamentNumber { get; set; }
        public string PostalCode { get; set; }

        //Navigation Properties
        public List<User> Users { get; set; } = new List<User>();

    }
}
