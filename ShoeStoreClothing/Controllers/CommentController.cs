using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Hubs;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.Repository;
using ShoeStoreClothing.ViewModels;
using System.Net.WebSockets;

namespace ShoeStoreClothing.Controllers
{
    public class CommentController(ICommentRepository _commentRepository,IHubContext<myHub> _hubContext,ApplicationDbContext _context,UserManager<AppUser> _userManager) : Controller
    {

        public async Task<IActionResult> ManagerComment(string ?search,int page=1)
        {
            var user = await _userManager.GetUserAsync(User);
            string userId =user.Id;
            if (user == null)
            {
                return Redirect("/404");
            }
            var listComment = new ListCommentViewModel()
            {
                comments = _commentRepository.GetAllComment(userId,search, page),
                TotalPage = (int)Math.Ceiling(_commentRepository.GetCount(userId,search) / (double)MyHelper.PAGE),
                search = search,
                page = page
            };
            return View(listComment);
        }
        //public IActionResult GetAllCommentProduct(string id,int page=1)
        //{
        //    int productDetailId = int.Parse(id);
        //    var listProductComment = _commentRepository.GetAllCommentProduct(productDetailId,page);
        //    return Json(new {id=productDetailId ,product=listProductComment });
        //}
        [HttpGet]
        public async Task<IActionResult> GetAllCommentProduct(int productDetailId)
        {
            try
            {
                var comments = _commentRepository.GetAllCommentProduct(productDetailId);
                return Json(new { data = comments });
            }
            catch (Exception ex)
            {
                // Log the error (not shown)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            int productDetailId=int.Parse(id);
            var productDetail=await _context.ProductDetails.Include(x=>x.Product).Include(x=>x.Size).Include(x=>x.Color).FirstOrDefaultAsync(x=>x.ProductDetailID==productDetailId);
            var user = await _userManager.GetUserAsync(User);
            ViewBag.productDetailId=productDetailId;
            ViewBag.productName = productDetail?.Product.ProductName;
            ViewBag.image=productDetail?.Image;
            ViewBag.userId = user.Id;
            ViewBag.Color = productDetail.Color.ColorName;
            ViewBag.Size = productDetail.Size.SizeName ;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    UserId = commentViewModel.UserId,
                    ProductDetailId = commentViewModel.ProductDetailId,
                    CommentText = commentViewModel.CommentText,
                };
                _commentRepository.Add(comment);
                await _hubContext.Clients.All.SendAsync("LoadComment", comment.ProductDetailId);
                return RedirectToAction("Order","Home");
            }
            return View(commentViewModel);
        }
    }
}
