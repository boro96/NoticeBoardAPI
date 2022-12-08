using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Models
{
    public class AdvertQuery
    {
        public string searchPhrase { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
