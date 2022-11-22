using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Advert Advert { get; set; }
        public int AdvertId { get; set; }
        public string Message { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}
