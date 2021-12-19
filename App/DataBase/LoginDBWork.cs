using Microsoft.EntityFrameworkCore;
using TasksApi.Models;

namespace TasksApi.App.DataBase
{
    public class LoginDBWork
    {
        private DbContextOptions<tasks_dbContext> Options { get; set; }
        public LoginDBWork() =>
            Options = new DbContextOptionsBuilder<tasks_dbContext>().Options;
        
        public LoginDBWork(DbContextOptions<tasks_dbContext> Options)=>
            this.Options = Options;

        /// <summary>
        /// Добавить нового пользователя в базу данных
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="InvalidOperationException">В случае, если пользователь уже существует</exception>
        public void AddUser(Models.LoginModel user)
        {
            using (var context = new tasks_dbContext(Options))
            {
                try
                {
                    var login = context.Logins.First(l => l.Login1.Equals(user.Login));
                }
                catch (InvalidOperationException e)
                {
                    var login = new Login()
                    {
                        Login1 = user.Login,
                        Password = user.Password
                    };
                    context.Logins.Add(login);
                    context.SaveChanges();
                    return;
                }
                catch(ArgumentNullException e)
                {
                    var login = new Login()
                    {
                        Login1 = user.Login,
                        Password = user.Password
                    };
                    context.Logins.Add(login);
                    context.SaveChanges();
                    return;
                }
                throw new InvalidOperationException("User Exists");
            }
        }
        
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Вслучае, если пользователь не зарегестрирован</exception>
        public bool CheckLogin(Models.LoginModel user)
        {
            using (var context = new tasks_dbContext(Options))
            {
                try
                {
                    var login = context.Logins.First(l => l.Login1.Equals(user.Login));
                    return login.Password.Equals(user.Password);
                }
                catch
                {
                    throw new NullReferenceException("User Not Exists");
                }
            }
        }
        /// <summary>
        /// Возвращает User ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int GetID(Models.LoginModel user)
        {
            using (var context = new tasks_dbContext(Options))
            {
                return context.Logins.First(u => u.Login1.Equals(user.Login)).Id;
            }
        }
    }
}
