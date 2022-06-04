using NSE.BFF.Orders.Models;
using NSE.Core.Comunication;

namespace NSE.Bff.Orders.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetCart();
        
        Task<int> GetQuantityCart();
        Task<ResponseResult> AddItemCart(ItemCartDTO product);
        Task<ResponseResult> UpdateItemCart(Guid productId, ItemCartDTO product);
        Task<ResponseResult> RemoveItemCart(Guid productId);
    }
}