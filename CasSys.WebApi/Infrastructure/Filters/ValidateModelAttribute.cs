using System;
using Microsoft.AspNetCore.Mvc.Filters;
using CasSys.WebApi.Infrastructure.Results;

namespace CasSys.WebApi.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private bool _skipModelValidation = false;

        public ValidateModelAttribute(bool skipModelValidation = false)
        {
            _skipModelValidation = skipModelValidation;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_skipModelValidation)
            {
                if (!context.ModelState.IsValid)
                {
                    context.Result = new ValidationFailedResult(context.ModelState);
                }
            }
        }
    }
}