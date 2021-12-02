using Microsoft.AspNetCore.Mvc;
using TasksApi.App.Models;

namespace TasksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly TasksApi.App.DataBase.TasksDBWork _dataBase =new App.DataBase.TasksDBWork();
        public TasksController(ILogger<TasksController> logger)
        {
            _logger = logger;
        }

        [HttpGet("All")]
        public void GetAllTasks()
        {
            var data = _dataBase.GetAllTasks();
            Response.StatusCode = 200;
        }

        [HttpGet("Today")]
        public void TodayTasks()
        {
            Response.StatusCode = 200;
        }

        [HttpGet("Preview")]
        public void PreviewTasks()
        {
            Response.StatusCode = 200;
        }

        [HttpPut("New")]
        public void NewTask([FromForm] TaskModel task)
        {

        }

        [HttpPost("SetStatus")]
        public void SetTaskStatus(int id, bool status)
        {

        }
    }
}
