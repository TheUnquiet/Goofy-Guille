namespace REST.Controller
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId)) throw new UnauthorizedAcessException("User ID is missing or invalid");
            return userId;
        }
    }
}