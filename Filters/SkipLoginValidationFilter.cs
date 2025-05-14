using Microsoft.AspNetCore.Mvc.Filters;
using Southwest_Airlines.Models.SharedViewModels;

namespace Southwest_Airlines.Filters
{
    public class SkipLoginValidationFilter : IActionFilter
    {
        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    // Iterate through all action arguments
        //    foreach (var argument in context.ActionArguments.Values)
        //    {
        //        // Check if the argument is a BaseViewModel or derived from it
        //        if (argument is BaseViewModel baseViewModel)
        //        {
        //            // If the model is not BaseViewModel itself, skip Login validation
        //            if (baseViewModel.GetType() != typeof(BaseViewModel))
        //            {
        //                context.ModelState.Remove(nameof(baseViewModel.Login));

        //            }
        //        }
        //    }
        //}

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is BaseViewModel)
                {
                    // Remove all ModelState errors for Login and its children
                    var keysToRemove = context.ModelState.Keys
                        .Where(k => k == "Login" || k.StartsWith("Login."))
                        .ToList();
                    foreach (var key in keysToRemove)
                    {
                        context.ModelState.Remove(key);
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No action needed after execution
        }
    }
}
