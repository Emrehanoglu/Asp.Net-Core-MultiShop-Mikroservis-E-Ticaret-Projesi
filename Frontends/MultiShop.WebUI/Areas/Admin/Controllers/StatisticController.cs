using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Statistic")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticService _catalogStatisticService;
        private readonly IUserStatisticService _userStatisticService;
        private readonly IDiscountStatisticService _discountStatisticService;
        private readonly ICommentStatisticService _commentStatisticService;
        private readonly IMessageStatisticService _messageStatisticService;

        public StatisticController(ICatalogStatisticService catalogStatisticService, IUserStatisticService userStatisticService, IDiscountStatisticService discountStatisticService, ICommentStatisticService commentStatisticService, IMessageStatisticService messageStatisticService)
        {
            _catalogStatisticService = catalogStatisticService;
            _userStatisticService = userStatisticService;
            _discountStatisticService = discountStatisticService;
            _commentStatisticService = commentStatisticService;
            _messageStatisticService = messageStatisticService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var getBrandCount = await _catalogStatisticService.GetBrandCount();
            var getProductCount = await _catalogStatisticService.GetProductCount();
            var getCategoryCount = await _catalogStatisticService.GetCategoryCount();
            var getMaxPriceProductName = await _catalogStatisticService.GetMaxPriceProductName();
            var getMinPriceProductName = await _catalogStatisticService.GetMinPriceProductName();

            var getUserCount = await _userStatisticService.GetUserCount();

            var getMessageTotalCount = await _messageStatisticService.GetTotalMessageCount();

            var getTotalCommentCount = await _commentStatisticService.GetTotalCommentCount();
            var getActiveCommentCount = await _commentStatisticService.GetActiveCommentCount();
            var getPassiveCommentCount = await _commentStatisticService.GetPassiveCommentCount();

            var getDiscountCouponCount = await _discountStatisticService.GetDiscountCouponCount();

            ViewBag.getBrandCount = getBrandCount;
            ViewBag.getProductCount = getProductCount;
            ViewBag.getCategoryCount = getCategoryCount;
            ViewBag.getMaxPriceProductName = getMaxPriceProductName;
            ViewBag.getMinPriceProductName = getMinPriceProductName;

            ViewBag.getUserCount = getUserCount;

            ViewBag.getMessageTotalCount = getMessageTotalCount;

            ViewBag.getTotalCommentCount = getTotalCommentCount;
            ViewBag.getActiveCommentCount = getActiveCommentCount;
            ViewBag.getPassiveCommentCount = getPassiveCommentCount;

            ViewBag.getDiscountCouponCount = getDiscountCouponCount;

            return View();
        }
    }
}
