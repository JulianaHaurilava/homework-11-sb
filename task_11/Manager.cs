using System;

namespace task_11
{
    class Manager :
        Consultant, IManagerCanEdit
    {
        public Manager(Repository r) :
            base(r)
        {

        }
        public void ChangeSurname(User userToEdit, string newSurname)
        {
            if (userToEdit.Surname != newSurname)
            {
                userToEdit.Surname = newSurname;
                Change lastChange = new Change(InfoToChange.Surname,
                    TypeOfChange.Editing, WorkerType.Manager);
                lastChange.WriteLastChangeInFile();
                r.AllInFile();
            }
        }
        public void ChangeName(User userToEdit, string newName)
        {
            if (userToEdit.Name != newName)
            {
                userToEdit.Name = newName;
                Change lastChange = new Change(InfoToChange.Name,
                    TypeOfChange.Editing, WorkerType.Manager);
                lastChange.WriteLastChangeInFile();
                r.AllInFile();
            }
        }
        public void ChangePatronimic(User userToEdit, string newPatronymic)
        {
            if (userToEdit.Patronymic != newPatronymic)
            {
                userToEdit.Patronymic = newPatronymic;
                Change lastChange = new Change(InfoToChange.Patronymic,
                    TypeOfChange.Editing, WorkerType.Manager);
                lastChange.WriteLastChangeInFile();
                r.AllInFile();
            }
        }
        public new bool ChangePhoneNumber(User userToEdit, string newPhoneNumber)
        {
            PhoneNumber phoneNumber = new PhoneNumber(newPhoneNumber);
            User owner = r[phoneNumber];
            if (owner == null)
            {
                userToEdit.PhoneNumber = phoneNumber;
                Change lastChange = new Change(InfoToChange.PhoneNumber,
                    TypeOfChange.Editing, WorkerType.Manager);
                lastChange.WriteLastChangeInFile();
                r.AllInFile();
                return true;
            }
            else if (owner.PhoneNumber == userToEdit.PhoneNumber)
            {
                return true;
            }
            return false;
        }
        public void ChangeDepartment(User userToEdit, string newDepartmentName)
        {
            if (userToEdit.Department.Name != newDepartmentName)
            {
                userToEdit.Department.Name = newDepartmentName;
                Change lastChange = new Change(InfoToChange.PassportSeriesNumber,
                    TypeOfChange.Editing, WorkerType.Consultant);
                lastChange.WriteLastChangeInFile();
                r.AllInFile();
            }
        }
        public void ChangeSeries(User userToEdit, string newSeries)
        {
            if (userToEdit.PassportSeries != newSeries)
            {
                userToEdit.PassportSeries = newSeries;
                Change lastChange = new Change(InfoToChange.PassportSeriesNumber,
                    TypeOfChange.Editing, WorkerType.Consultant);
                lastChange.WriteLastChangeInFile();
                r.AllInFile();
            }
        }
        public void ChangeNumber(User userToEdit, int newNumber)
        {
            if (userToEdit.PassportNumber != newNumber)
            {
                userToEdit.PassportNumber = newNumber;
                Change lastChange = new Change(InfoToChange.PassportSeriesNumber, TypeOfChange.Editing, WorkerType.Consultant);
                lastChange.WriteLastChangeInFile();
                r.AllInFile();
            }
        }

        /// <summary>
        /// Добавляет нового клиента в коллекцию репозитория
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public bool AddNewUser(User newUser)
        {
            if (r[newUser.PhoneNumber] == null)
            {
                r.AddUser(newUser);
                Change lastChange = new Change(InfoToChange.AllAccount, TypeOfChange.Adding, WorkerType.Manager);
                lastChange.WriteLastChangeInFile();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет выбранного клиента
        /// </summary>
        /// <param name="userToDelete"></param>
        public void DeleteUser(User userToDelete)
        {
            r.AllUsers.Remove(userToDelete);
            Change lastChange = new Change(InfoToChange.AllAccount, TypeOfChange.Deleting, WorkerType.Manager);
            lastChange.WriteLastChangeInFile();
            r.AllInFile();
        }
    }
}