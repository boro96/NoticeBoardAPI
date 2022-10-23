using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public List<Advert> Adverts { get; set; } = new List<Advert>();
    }
}
