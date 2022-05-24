﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : MainController
    {
        [Route("sistema-indisponivel")]
        public IActionResult SystemUnavailable()
        {
            var modelErro = new ErrorViewModel
            {
                Message= "O sistema está temporariamente indisponível, isto pode ocorrer em momentos de sobrecarga de usuários.",
                Title = "Sistema indisponível.",
                ErrorCode = 500
            };

            return View("Error", modelErro);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Title = "Ocorreu um erro!";
                modelErro.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelErro.Message =
                    "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Title = "Ops! Página não encontrada.";
                modelErro.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelErro.Message = "Você não tem permissão para fazer isto.";
                modelErro.Title = "Acesso Negado";
                modelErro.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);
        }
    }
}
