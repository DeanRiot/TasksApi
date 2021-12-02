using System;
using System.Collections.Generic;

namespace TasksApi.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string Theader { get; set; }
        public string Ttext { get; set; }
        public DateTime Enddate { get; set; }
        public bool? Ended { get; set; }

        public virtual Login User { get; set; }
    }
}
