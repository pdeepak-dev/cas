using CasSys.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CasSys.WebApi.Infrastructure.Results
{
    public class ValidationFailedResult : ObjectResult
    {
        public bool Succedded { get; set; }

        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            Succedded = false;
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}