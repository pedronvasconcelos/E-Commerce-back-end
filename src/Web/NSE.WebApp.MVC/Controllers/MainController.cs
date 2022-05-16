using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Message.Any())
            {
                foreach (var message in response.Errors.Message)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

                return true;
            }

            return false;
        }
    }
}