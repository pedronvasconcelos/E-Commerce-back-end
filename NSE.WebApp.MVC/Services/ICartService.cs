using System;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface ICartService
    {
        Task<CartViewModel> GetCart();
        Task<ResponseResult> AddItemCart(ItemProductViewModel product);
        Task<ResponseResult> UpdateItemCart(Guid productId, ItemProductViewModel product);
        Task<ResponseResult> RemoveItemCart(Guid productId);
    }
}