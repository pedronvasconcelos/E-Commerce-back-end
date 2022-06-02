using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSE.WebAPI.Core.Controllers
{   
    [ApiController]
    public abstract class MainController : Controller
    {
        public ICollection<string> Errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }


        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddNotification(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse (ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.ErrorMessage);
            }
            return CustomResponse();
        }
        protected bool OperationValid()
        {
            return !Errors.Any();
        }

        protected void AddNotification(string error)
        {
            Errors.Add(error);
        }

        protected void ClearProcessingError()
        {
            Errors.Clear();
        }
    }
}
