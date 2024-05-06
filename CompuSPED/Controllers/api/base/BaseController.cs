using CompuSPED.Models;
using System.Web.Http;

namespace CompuSPED.Controllers.api.Base
{
	public class BaseController : ApiController
	{
        protected IHttpActionResult GetApiErrorResponse(string message)
        {
            return Ok(new ApiResponse<object>
            {
                Message = message,
                HasError = true
            });
        }
    }
}