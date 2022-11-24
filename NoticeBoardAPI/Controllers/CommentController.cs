using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    [Route("api/adverts/{advertId}/comments")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        public ActionResult CreateComment([FromRoute] int advertId, [FromBody] CreateCommentDto dto)
        {
            var commentId = _commentService.Create(advertId, dto);

            return Created($"/api/adverts/{advertId}/comments/{commentId}", null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<CommentDto>> GetAll([FromRoute] int advertId)
        {
            var commentsDto = _commentService.GetAll(advertId);

            return Ok(commentsDto);
        }
        [HttpGet("{commentId}")]
        public ActionResult<CommentDto> Get([FromRoute] int advertId, [FromRoute] int commentId)
        {
            var commentDto = _commentService.GetById(advertId, commentId);

            return Ok(commentDto);
        }
        [HttpDelete("{commentId}")]
        public ActionResult DeleteComment([FromRoute] int advertId, [FromRoute] int commentId)
        {
            _commentService.Delete(advertId, commentId);

            return NoContent();
        }
    }
}
