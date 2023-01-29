namespace task_11
{
    interface IManagerCanEdit :
        IConsultantCanEdit
    {
        /// <summary>
        /// Редактирует фамилию клиента
        /// </summary>
        /// <param name="userToEdit"></param>
        /// <param name="newSurname"></param>
        void ChangeSurname(User userToEdit, string newSurname);

        /// <summary>
        /// Редактирует имя клиента
        /// </summary>
        /// <param name="userToEdit"></param>
        /// <param name="newName"></param>
        void ChangeName(User userToEdit, string newName);

        /// <summary>
        /// Редактирует отчество клиента
        /// </summary>
        /// <param name="userToEdit"></param>
        /// <param name="newPatronymic"></param>
        void ChangePatronimic(User userToEdit, string newPatronymic);

        /// <summary>
        /// Редактирует номер телефона клиента.
        /// При успешном редактировании возвращает true
        /// </summary>
        /// <param name="userToEdit"></param>
        /// <param name="newPhoneNumber"></param>
        /// <returns></returns>
        new bool ChangePhoneNumber(User userToEdit, string newPhoneNumber);

        /// <summary>
        /// Редактирует департамент клиента
        /// </summary>
        /// <param name="userToEdit"></param>
        /// <param name="newDepartmentName"></param>
        void ChangeDepartment(User userToEdit, string newDepartmentName);

        /// <summary>
        /// Редактирует серию паспорта клиента
        /// </summary>
        /// <param name="userToEdit"></param>
        /// <param name="newSeries"></param>
        void ChangeSeries(User userToEdit, string newSeries);

        /// <summary>
        /// Редактирует номер паспорта клиента
        /// </summary>
        /// <param name="userToEdit"></param>
        /// <param name="newNumber"></param>
        void ChangeNumber(User userToEdit, int newNumber);
    }
}
