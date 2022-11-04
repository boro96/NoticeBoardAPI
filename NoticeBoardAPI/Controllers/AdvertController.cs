using Microsoft.AspNetCore.Mvc;
using NoticeBoardAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Controllers
{
    [Route("api/adverts")]
    public class AdvertController : ControllerBase
    {
        public AdvertController()
        {

        }
        [HttpGet]
        public ActionResult<IEnumerable<Advert>> GetAll()
        {
            var adverts = 
        }
    }
}
