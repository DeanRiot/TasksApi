using Microsoft.AspNetCore.Mvc;

namespace TasksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly App.DataBase.LoginDBWork AuthDB = new App.DataBase.LoginDBWork();
        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;

        }

        [HttpPut("LogIn")]
        public void LogIn([FromForm] App.Models.LoginModel login)
        {
            try
            {
                AuthDB.CheckLogin(login);
                Response.StatusCode = 200;
            }
            catch
            {
                Response.StatusCode = 401;
            }
            
        }

        [HttpPost("Registration")]
        public void Registrate([FromForm] App.Models.LoginModel login)
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
