using Microsoft.AspNetCore.Mvc;
using TasksApi.App.Models;

namespace TasksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly TasksApi.App.DataBase.TasksDBWork _dataBase = new App.DataBase.TasksDBWork();
        public TaskController(ILogger<TaskController> logger) => _logger = logger;

        private int getID(HttpRequest req)
        {
            if (req.Cookies.Count != 0) return int.Parse(Request.Cookies["sessionID"]);
            else return -1;
        }
        private HttpResponse setHeaders(HttpResponse response)
        {
            response.Headers["Access-Control-Allow-Origin"] = Request.Headers["Origin"];
            response.Headers["Access-Control-Allow-Credentials"] = "true";
            response.Headers["x-content-type-options"] = "nosniff";
            return response;
        }

        [HttpGet("All")]
        public void AllTasks()
        {
            var _id = getID(Request);
            var resp = setHeaders(Response);
            if (_id != -1)
            {
                var data = _dataBase.GetAllTasks(_id);
                resp.WriteAsJsonAsync(data);
            }
            else resp.StatusCode = 401;
        }

        [HttpGet("Today")]
        public void TodayTasks()
        {
            var _id = getID(Request);
            var resp = setHeaders(Response);
            if (_id != -1)
            {
                var data = _dataBase.GetTodayTasks(_id);
                resp.WriteAsJsonAsync(data);
            }
            else resp.StatusCode = 401;
        }

        [HttpGet("Old")]
        public void OldTasks()
        {
            var _id = getID(Request);
            var resp = setHeaders(Response);
            if (_id != -1)
            {
                var data = _dataBase.GetOldTasks(_id);
                resp.WriteAsJsonAsync(data);
            }
            else resp.StatusCode = 401;
        }

        [HttpGet("Actual")]
        public void ActualTasks()
        {
            var _id = getID(Request);
            var resp = setHeaders(Response);
            if (_id != -1)
            {
                var data = _dataBase.GetInProcess(_id);
                resp.WriteAsJsonAsync(data);
            }
            else resp.StatusCode = 401;
        }

        [HttpGet("Ended")]
        public void EndedTasks()
        {
            var _id = getID(Request);
            var resp = setHeaders(Response);
            if (_id != -1)
            {
                var data = _dataBase.GetEnded(_id);
                resp.WriteAsJsonAsync(data);
            }
            else resp.StatusCode = 401;
        }

        [HttpGet("Preview")]
        public IActionResult PreviewTasks()
        {
            var _id = getID(Request);
            var resp = setHeaders(Response);
            if (_id != -1) return Ok(_dataBase.GetPreviewTasks(_id));
            else return Unauthorized();
        }

        [HttpPut("New")]
        public void NewTask(TaskModel task)
        {
            var _id = getID(Request);
            var resp = setHeaders(Response);
            if (_id != -1) _dataBase.AddTask(_id, task);
            else resp.StatusCode = 401;
        }

        [HttpPost("Status")]
        public void SetTaskStatus(int id, bool status)
        {
            var _id = getID(Request);
            var resp = setHeaders(Response);
            if (_id != -1) _dataBase.SetStatus(_id, id, status);
            else resp.StatusCode = 401;
        }

        [HttpDelete("{id}")]
        public void DelTask(int id)
        {
            var _id = getID(Request);
            var resp = setHeaders(Response);
            if (_id != -1) _dataBase.DelTask(_id, id);
            else resp.StatusCode = 401;
        }
    }
}
