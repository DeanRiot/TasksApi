namespace TasksApi.App.Models
{
    public class SortedModel
    {
        public List<TaskModel>? Today;
        public List<TaskModel>? Ended;
        public List<TaskModel>? InProcess;
        public List<TaskModel>? Old;
    }
}
