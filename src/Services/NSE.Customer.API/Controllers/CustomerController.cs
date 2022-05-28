using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Core.Mediator;
using NSE.Customers.API.Application.Commands;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Customers.API.Controllers
{ 
    public class CustomerController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomerController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }
        
        [HttpGet("customers")]
        public async Task<IActionResult> Index()
        {

          var result =  await  _mediatorHandler.SendCommand(
              new RegisterCustomerCommand(Guid.NewGuid(), "Fulano", "Fulano@fulano.com.br", "25554898009"));
                
            return CustomResponse(result);
        }            
        
    }
   
}