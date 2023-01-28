using System;

namespace task_11
{
    class Consultant :
        IConsultantCanEdit
    {
        protected Repository r;

        public Consultant(Repository r)
        {
            this.r = r;
        }
        public bool ChangePhoneNumber(User userToEdit, string newPhoneNumber)
        {
            PhoneNumber phoneNumber = new PhoneNumber(newPhoneNumber);
            User owner = r[phoneNumber];
            if (owner == null)
            {
                userToEdit.PhoneNumber = phoneNumber;
                Change lastChange = new Change(InfoToChange.PhoneNumber,
                    TypeOfChange.Editing, WorkerType.Consultant);
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
    }
}
