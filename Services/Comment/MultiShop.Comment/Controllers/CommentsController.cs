using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _commentContext;

        public CommentsController(CommentContext commentContext)
        {
            _commentContext = commentContext;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values=_commentContext.UserComments.ToList();
            return Ok(values);
        }

        [HttpGet("CommentListByProductId")]
        public IActionResult CommentListByProductId(string id)
        {
            var values = _commentContext.UserComments.Where(x=>x.ProductId == id).ToList(); 
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateComment(UserComment comment)
        {
            _commentContext.UserComments.Add(comment);
            _commentContext.SaveChanges();
            return Ok("Yorum eklendi");
        }


        [HttpPut]
        public IActionResult UpdateComment(UserComment comment)
        {
            _commentContext.UserComments.Update(comment);
            _commentContext.SaveChanges();
            return Ok("Yorum güncellendi");
        }

        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            var value=_commentContext.UserComments.Find(id);
            _commentContext.UserComments.Remove(value);
            _commentContext.SaveChanges();
            return Ok("Yorum silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var value = _commentContext.UserComments.Find(id);
            return Ok(value);
        }
    }
}
