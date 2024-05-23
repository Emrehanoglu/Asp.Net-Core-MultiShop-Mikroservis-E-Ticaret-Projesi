using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
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
        var values = _commentContext.UserComments.ToList();
        return Ok(values);
    }
    [HttpGet("{id}")]
    public IActionResult GetByIdComment(int id)
    {
        var value = _commentContext.UserComments.Find(id);
        return Ok(value);
    }
    [HttpGet("CommentListByProductId/{id}")]
    public IActionResult CommentListByProductId(string id)
    {
        var value = _commentContext.UserComments.Where(x=>x.ProductId==id).ToList();
        return Ok(value);
    }
    [HttpPost]
    public IActionResult CreateComment(UserComment userComment)
    {
        _commentContext.UserComments.Add(userComment);
        _commentContext.SaveChanges();
        return Ok("Comment başarıyla eklendi");
    }
    [HttpPut]
    public IActionResult UpdateComment(UserComment userComment)
    {
        _commentContext.UserComments.Update(userComment);
        _commentContext.SaveChanges();
        return Ok("Comment başarıyla güncellendi");
    }
    [HttpDelete]
    public IActionResult DeleteComment(int id)
    {
        var value = _commentContext.UserComments.Find(id);
        _commentContext.UserComments.Remove(value);
        _commentContext.SaveChanges();
        return Ok("Comment başarıyla silindi");
    }
}
