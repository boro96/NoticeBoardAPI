using NoticeBoardAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Models
{
    public class AdvertDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
