using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents;

public class _ProductDetailReviewComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICommentService _commentService;

    public _ProductDetailReviewComponentPartial(IHttpClientFactory httpClientFactory, ICommentService commentService)
    {
        _httpClientFactory = httpClientFactory;
        _commentService = commentService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string id)
    {
        var values = await _commentService.CommentListByProductId(id);
        return View(values);
    }
}
