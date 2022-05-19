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
           
            if (resposta != null && resposta.Errors.Messages.Any())
            {
                foreach (var mensagem in resposta.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }
    }
}