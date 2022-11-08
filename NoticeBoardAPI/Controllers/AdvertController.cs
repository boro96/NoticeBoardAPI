using Microsoft.AspNetCore.Mvc;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.Services;
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
        private readonly IAdvertService _advertService;

        public AdvertController(IAdvertService advertService)
        {
            _advertService = advertService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AdvertDto>> GetAll()
        {
            var advertDtos = _advertService.GetAll();

            return Ok(advertDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<AdvertDto> Get([FromRoute] int id)
        {
            var advert = _advertService.GetById(id);

            if(advert is null)
            {
                return NotFound();
            }
            return Ok(advert);
        }
    }
}
