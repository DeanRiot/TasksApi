namespace TasksApi.App.Models
{
    public class SortedModel
    {
        public List<TaskModel> Today = new List<TaskModel>();
        public List<TaskModel> Ended = new List<TaskModel>();
        public List<TaskModel> InProcess = new List<TaskModel>();
        public List<TaskModel> Old = new List<TaskModel>();
    }
}
