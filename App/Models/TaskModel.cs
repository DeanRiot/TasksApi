namespace TasksApi.App.Models
{
    public class TaskModel
    {
        public int? ID { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public bool? Status { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
