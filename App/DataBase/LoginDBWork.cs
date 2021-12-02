using TasksApi.Models;

namespace TasksApi.App.DataBase
{
    public class LoginDBWork
    {
        /// <summary>
        /// Добавить нового пользователя в базу данных
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="InvalidOperationException">В случае, если пользователь уже существует</exception>
        public void AddUser(Models.LoginModel user)
        {
            using (var context = new tasks_dbContext())
            {
                Login login = null;
                login = context.Logins.First(l => l.Login1.Equals(user.Login));
                if (login is null)
                {
                    login = new Login()
                    {
                        Login1 = user.Login,
                        Password = user.Password
                    };
                }  
                else 
                {
                    throw new InvalidOperationException("User Exists");
                }
                context.Logins.Add(login);
                context.SaveChanges();
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
            using (var context = new tasks_dbContext())
            {
                Login login = null;
                login = context.Logins.First(l => l.Login1.Equals(user.Login));
                if (login is null)
                {
                    throw new NullReferenceException("User Not Exists");
                }
                else
                {
                    login = new Login()
                    {
                        Login1 = user.Login,
                        Password = user.Password
                    };
                }
                return login.Password.Equals(user.Password);
            }
        }
    }
}
