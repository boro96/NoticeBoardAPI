using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [Authorize(Policy ="HasNationality")]
        public ActionResult<IEnumerable<AdvertDto>> GetAll()
        {
            
            var advertDtos = _advertService.GetAll();

            return Ok(advertDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<AdvertDto> Get([FromRoute] int id)
        {
            var advert = _advertService.GetById(id);

            return Ok(advert);
        }

        [HttpPost]
        public ActionResult CreateAdvert([FromBody] CreateAdvertDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var advert = _advertService.Create(dto, userId);

            return Ok();
        }
    }
}
