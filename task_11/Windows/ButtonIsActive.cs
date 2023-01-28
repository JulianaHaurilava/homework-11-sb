namespace task_11.Windows
{
    class ButtonIsActive
    {
        public bool surnameNotEmpty;

        public bool nameNotEmpty;

        public bool patronymicNotEmpty;

        public bool phoneNumberNotEmpty;

        public bool departmentNotEmpty;

        public bool seriesNotEmpty;

        public bool numberNotEmpty;

        public ButtonIsActive(bool notEmpty)
        {
            surnameNotEmpty = notEmpty;
            nameNotEmpty = notEmpty;
            patronymicNotEmpty = notEmpty;
            departmentNotEmpty = notEmpty;
            phoneNumberNotEmpty = notEmpty;
            seriesNotEmpty = notEmpty;
            numberNotEmpty = notEmpty;
        }

        public bool ButtonIsEnabled()
        {
            return surnameNotEmpty && nameNotEmpty && patronymicNotEmpty &&
                phoneNumberNotEmpty && departmentNotEmpty && numberNotEmpty && seriesNotEmpty;
        }
    }
}
