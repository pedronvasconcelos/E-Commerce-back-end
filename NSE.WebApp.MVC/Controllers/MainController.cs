using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult resposta)
        {
           
            if (resposta != null && resposta.Errors.Message != null)
            {
                foreach (var mensagem in resposta.Errors.Message)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }
    }
}