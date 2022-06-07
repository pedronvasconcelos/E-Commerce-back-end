using System;
using System.Threading.Tasks;
using NSE.Core.Comunication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface IOrdersBffService
    {
        Task<CartViewModel> GetCart();

        Task<int> GetQuantityCart();
        Task<ResponseResult> AddItemCart(ItemCartViewModel product);
        Task<ResponseResult> UpdateItemCart(Guid productId, ItemCartViewModel product);
        Task<ResponseResult> RemoveItemCart(Guid productId);
    }
}