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
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        public AdvertController(IAdvertService advertService)
        {
            _advertService = advertService;
        }
        [HttpGet]
        [Authorize(Policy = "HasNationality")]
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
            var advertId = _advertService.Create(dto, userId);

            return Created($"/api/adverts/{advertId}", null);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateAdvert([FromBody] UpdateAdvertDto dto, [FromRoute] int id)
        {
            _advertService.Update(dto, id);

            return Ok();
        }
        [HttpDelete]
        public ActionResult DeleteAdvert([FromRoute] int id)
        {
            _advertService.Delete(id);

            return NoContent();
        }
    }
}
