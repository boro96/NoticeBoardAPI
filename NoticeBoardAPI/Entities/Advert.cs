using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Entities
{
    public class Advert
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }

        //Navigation Properties
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
