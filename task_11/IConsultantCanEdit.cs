namespace task_11
{
    interface IConsultantCanEdit
    {
        /// <summary>
        /// Редактирует номер телефона клиента.
        /// При успешном редактировании возвращает true
        /// </summary>
        /// <param name="userToEdit"></param>
        /// <param name="newPhoneNumber"></param>
        /// <returns></returns>
        bool ChangePhoneNumber(User userToEdit, string newPhoneNumber);
    }
}
