using System.Collections.Generic;
using System.IO;

namespace task_11
{
    public class Repository
    {

        private string usersFileName;
        private string departmentsFileName;

        public List<User> AllUsers { get; set; } // Коллекция клиентов банка
        public List<Department> AllDepartments { get; set; } // Коллекция всех департаментов банка
        public Repository(string usersFileName, string departmentsFileName)
        {
            this.usersFileName = usersFileName;
            this.departmentsFileName = departmentsFileName;
            AllUsers = new List<User>();
            AllDepartments = new List<Department>();
            ReadFile();
        }

        /// <summary>
        /// Индексатор, находящий клиента по номеру телефона
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public User this[PhoneNumber phoneNumber]
        {
            get
            {
                foreach (User user in AllUsers)
                {
                    if (user.PhoneNumber == phoneNumber)
                        return user;
                }
                return null;
            }
        }

        /// <summary>
        /// Считывает данные из файла
        /// </summary>
        private void ReadFile()
        {
            if (File.Exists(usersFileName))
            {
                using (StreamReader stream = new StreamReader(usersFileName, true))
                {
                    while (!stream.EndOfStream)
                    {
                        AllUsers.Add(new User(stream.ReadLine()));
                    }
                }
            }
            if (File.Exists(departmentsFileName))
            {
                using (StreamReader stream = new StreamReader(departmentsFileName, true))
                {
                    while (!stream.EndOfStream)
                    {
                        AllDepartments.Add(new Department(stream.ReadLine()));
                    }
                }
            }
        }

        /// <summary>
        /// Перезаписывает информацию в файле
        /// </summary>
        public void AllInFile()
        {
            using (StreamWriter stream = new StreamWriter(usersFileName))
            {
                foreach (User user in AllUsers)
                {
                    stream.WriteLine(user);
                }
            }
        }

        /// <summary>
        /// Добавляет нового клиента в репозиторий
        /// </summary>
        /// <param name="newUser"></param>
        public void AddUser(User newUser)
        {
            AllUsers.Add(newUser);
            WriteUserInFile(newUser);
        }

        /// <summary>
        /// Записывает пользователя в конец файла
        /// </summary>
        /// <param name="newUser"></param>
        private void WriteUserInFile(User newUser)
        {
            using (StreamWriter stream = new StreamWriter(usersFileName, true))
            {
                stream.WriteLine(newUser);
            }
        }

    }
}
