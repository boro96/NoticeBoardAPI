using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
