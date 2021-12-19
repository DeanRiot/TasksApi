using Microsoft.AspNetCore.Mvc;

namespace TasksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly App.DataBase.LoginDBWork AuthDB = new App.DataBase.LoginDBWork();
        public AuthController(ILogger<AuthController> logger) => _logger = logger;

        [HttpPut("LogIn")]
        public void LogIn(App.Models.LoginModel login)
        {
            try
            {
                var _hasUser = AuthDB.CheckLogin(login);
                if (_hasUser)
                {
                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = System.DateTimeOffset.Now.AddDays(7);
                    cookie.SameSite = SameSiteMode.None;
                    cookie.Secure = true;
                    Response.Cookies.Append("sessionID", AuthDB.GetID(login).ToString(),cookie);
                    Response.Headers["Access-Control-Allow-Origin"] = Request.Headers["Origin"];
                    Response.Headers["Access-Control-Allow-Credentials"] = "true";
                    Response.StatusCode = 200;
                }
                else
                    Response.StatusCode = 401;

            }
            catch
            {
                Response.StatusCode = 401;
            }
        }

        [HttpPost("Registration")]
        public void Registrate(App.Models.LoginModel login)
        {
            try
            {
                AuthDB.AddUser(login);
                Response.StatusCode = StatusCodes.Status201Created;
            }
            catch
            {
                Response.StatusCode = StatusCodes.Status409Conflict;
            }

        }
    }
}
