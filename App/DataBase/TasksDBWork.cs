using TasksApi.Models;

namespace TasksApi.App.DataBase
{
    public class TasksDBWork
    {
        /// <summary>
        /// Возвращает список задач для заданного пользователя
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Models.TaskModel> GetAllTasks(int userID)
        {
            List<Models.TaskModel> tasks = new List<Models.TaskModel>();
            using (var context = new tasks_dbContext())
            {
                var usr = context.Logins.First(l => l.Id == userID);
                foreach (var task in context.Tasks)
                {
                    if (usr.Id == task.Userid)
                    {
                        tasks.Add(new Models.TaskModel()
                        {
                            ID = task.Id,
                            Header = task.Theader,
                            Text = task.Ttext,
                            Status = task.Ended,
                            EndDate = task.Enddate
                        });
                    }
                }
            }
            return tasks;
        }
        /// <summary>
        /// Добавляет задачу в базу данных
        /// </summary>
        /// <param name="userID">Пользователь для которого добавлена задача</param>
        /// <param name="task">Задача</param>
        public void AddTask(int userID, Models.TaskModel task)
        {
            using (var context = new tasks_dbContext())
            {
                context.Tasks.Add(new TasksApi.Models.Task()
                {
                    Theader = task.Header,
                    Ttext = task.Text,
                    Userid = userID,
                    Ended = false,
                    Enddate = task.EndDate
                });
            }
        }
        /// <summary>
        /// Возрвразает все задачи на текущую дату
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <returns></returns>
        public List<Models.TaskModel> GetTodayTasks(int userID)
        {
            var tasks = new List<Models.TaskModel>();
            using (var context = new tasks_dbContext())
            {
                foreach (var task in context.Tasks)
                {
                    if (task.Userid.Equals(userID) && task.Enddate.Equals(DateTime.Now))
                    {
                        tasks.Add(new Models.TaskModel()
                        {
                            ID = task.Id,
                            EndDate = task.Enddate,
                            Header = task.Theader,
                            Status = task.Ended,
                            Text = task.Ttext
                        });
                    }
                }
                return tasks;
            }
        }
        /// <summary>
        /// Возвращает завершенные задачи
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Models.TaskModel> GetEnded(int userId)
        {
            var tasks = new List<Models.TaskModel>();
            using (var context = new tasks_dbContext()){
                foreach(var task in context.Tasks){
                    if(task.Userid.Equals(userId) && task.Ended) {
                        tasks.Add(new Models.TaskModel()
                        {
                            ID = task.Id,
                            EndDate = task.Enddate,
                            Header = task.Theader,
                            Text = task.Ttext,
                            Status = task.Ended
                        });
                    }
                }
            }
            return tasks;
        }
        /// <summary>
        /// Возвращает все не завершенные задачи
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Models.TaskModel> GetInProcess(int userID)
        {
            List<Models.TaskModel> tasks = new List<Models.TaskModel>();
            using (var context = new tasks_dbContext())
            {
                foreach(var task in context.Tasks)
                {
                    if (task.Userid.Equals(userID) && !task.Ended)
                    {
                        tasks.Add(new App.Models.TaskModel()
                        {
                            ID = task.Id,
                            Header = task.Theader,
                            Text = task.Ttext,
                            EndDate = task.Enddate,
                            Status = task.Ended
                        });
                    }
                }
            }
            return tasks;
        }
        /// <summary>
        /// Возвращает просроченные не завершенные задачи
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Models.TaskModel> GetOldTasks(int userID)
        {
            List<Models.TaskModel> tasks = new List<Models.TaskModel>();
            using (var context = new tasks_dbContext())
            {
                foreach (var task in context.Tasks)
                {
                    if (task.User.Equals(userID) && task.Enddate<DateTime.Now && !task.Ended)
                    {
                        tasks.Add(new Models.TaskModel()
                        {
                            ID = task.Id,
                            Header = task.Theader,
                            Text = task.Ttext,
                            EndDate = task.Enddate,
                            Status = task.Ended
                        });
                    }
                }
            }
            return tasks;
        }
        /// <summary>
        /// Первые 5 элементов списка
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        private List<Models.TaskModel> GetFirstElements(List<Models.TaskModel> tasks)
        {
            if (tasks.Count <= 5)
            {
                return tasks;
            }
            var firstTasks = new List<Models.TaskModel>();
            for (int index = 0; index<=4; index++)
            {
                tasks.Add(firstTasks[index]);
            }
            return firstTasks;
        }
        /// <summary>
        /// Возвращает по 5 элементов на блок 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Models.SortedModel GetPreviewTasks(int userID)
        {
            var preview = new Models.SortedModel();
            var today = GetTodayTasks(userID);
            var ended = GetEnded(userID);
            var proc = GetInProcess(userID);
            var old = GetOldTasks(userID);
            preview.Today = GetFirstElements(today);
            preview.InProcess = GetFirstElements(proc);
            preview.Old = GetFirstElements(old);
            preview.Ended = GetFirstElements(ended);
            return preview;
        }
        /// <summary>
        /// Возвращает все данные по блокам
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Models.SortedModel GetSortedTasks(int userID)
        {
            return new Models.SortedModel()
            {
                Today = GetTodayTasks(userID),
                Old = GetOldTasks(userID),
                InProcess = GetInProcess(userID),
                Ended = GetEnded(userID)
            };
        }
        /// <summary>
        /// Устанавливает статус задачи
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="TaskID"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetStatus(int userID, int TaskID, bool status)
        {
            using (var context = new tasks_dbContext())
            {
                var task = context.Tasks.First(t => t.Userid == userID && t.Id == TaskID);
                task.Ended = status;
                context.Update(task);
                context.SaveChanges();
            }
        }
    }
}
