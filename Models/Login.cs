using System;
using System.Collections.Generic;

namespace TasksApi.Models
{
    public partial class Login
    {
        public Login()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Login1 { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
