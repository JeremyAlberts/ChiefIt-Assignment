using Microsoft.AspNetCore.Mvc;
using YakShop.Core.Operation;

namespace YakShop.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected IActionResult ToActionResult<TResult>(OperationResult<TResult> result)
        {
            var errorMessage = result.ErrorMessage;

            return result.OperationStatus switch
            {
                OperationStatus.Ok => result.Data is null ? NoContent() : Ok(result.Data),
                OperationStatus.Error or OperationStatus.Exception => BadRequest(errorMessage),
                OperationStatus.None => NoContent(),
                OperationStatus.Created => CreatedAtAction("order", result.Data),
                OperationStatus.ResetContent => StatusCode(205),
                OperationStatus.Partial => StatusCode(206, result.Data),
                _ => StatusCode(500, errorMessage),
            };
        }
    }
}
