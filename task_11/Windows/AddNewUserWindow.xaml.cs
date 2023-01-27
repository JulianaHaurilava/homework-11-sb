using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace task_11.Windows
{
    /// <summary>
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    public partial class AddNewUserWindow : Window
    {
        class AddNewUserButtonEnabled
        {
            private bool surnameNotEmpty;
            public bool SurnameNotEmpty
            {
                set => surnameNotEmpty = value;
            }

            private bool nameNotEmpty;
            public bool NameNotEmpty
            {
                set => nameNotEmpty = value;
            }

            private bool patronymicNotEmpty;
            public bool PatronymicNotEmpty
            {
                set => patronymicNotEmpty = value;
            }

            private bool phoneNumberNotEmpty;
            public bool PhoneNumberNotEmpty
            {
                set => phoneNumberNotEmpty = value;
            }

            private bool departmentNotEmpty;
            public bool DepartmentNotEmpty
            {
                set => departmentNotEmpty = value;
            }

            private bool seriesNotEmpty;
            public bool SeriesNotEmpty
            {
                set
                => seriesNotEmpty = value;
            }

            private bool numberNotEmpty;
            public bool NumberNotEmpty
            {
                set
                => numberNotEmpty = value;
            }

            public AddNewUserButtonEnabled()
            {
                surnameNotEmpty = false;
                nameNotEmpty = false;
                patronymicNotEmpty = false;
                phoneNumberNotEmpty = false;
                seriesNotEmpty = false;
                numberNotEmpty = false;
            }
            public bool ButtonIsEnabled()
            {
                return surnameNotEmpty && nameNotEmpty && patronymicNotEmpty &&
                    phoneNumberNotEmpty && numberNotEmpty && seriesNotEmpty;
            }
        }
        ListView l;
        Repository r;

        AddNewUserButtonEnabled anu;
        public User userToAdd;
        public AddNewUserWindow(ListView l, Repository r, List<Department> allDepartments)
        {
            InitializeComponent();
            anu = new AddNewUserButtonEnabled();
            cbChooseDepartment.ItemsSource = allDepartments;
            this.l = l;
            this.r = r;
        }

        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSurname.Text.Length != 0)
                anu.SurnameNotEmpty = true;
            else anu.SurnameNotEmpty = false;

            bAddNewClient.IsEnabled = anu.ButtonIsEnabled();
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbName.Text.Length != 0)
                anu.NameNotEmpty = true;
            else anu.NameNotEmpty = false;

            bAddNewClient.IsEnabled = anu.ButtonIsEnabled();
        }

        private void tbPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPatronymic.Text.Length != 0)
                anu.PatronymicNotEmpty = true;
            else anu.PatronymicNotEmpty = false;

            bAddNewClient.IsEnabled = anu.ButtonIsEnabled();
        }

        private void tbPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPhoneNumber.Text.Length != 0)
                anu.PhoneNumberNotEmpty = true;
            else anu.PhoneNumberNotEmpty = false;

            bAddNewClient.IsEnabled = anu.ButtonIsEnabled();
        }

        private void tbSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSeries.Text.Length != 0)
                anu.SeriesNotEmpty = true;
            else anu.SeriesNotEmpty = false;

            bAddNewClient.IsEnabled = anu.ButtonIsEnabled();
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbNumber.Text.Length != 0)
                anu.NumberNotEmpty = true;
            else anu.NumberNotEmpty = false;

            bAddNewClient.IsEnabled = anu.ButtonIsEnabled();
        }

        private void cbChooseDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            anu.DepartmentNotEmpty = true;
            bAddNewClient.IsEnabled = anu.ButtonIsEnabled();
        }

        private void bAddNewClient_Click(object sender, RoutedEventArgs e)
        {
            userToAdd = new User(tbSurname.Text, tbName.Text, tbPatronymic.Text,
                tbPhoneNumber.Text, cbChooseDepartment.Text, tbSeries.Text, int.Parse(tbNumber.Text));
            Manager m = new Manager(r);
            if (m.AddNewUser(userToAdd))
            {
                l.Items.Refresh();
                Close();
            }
            else
            {
                string errorString = "Клиент с введенным номером телефона уже зарегистрирован в системе!";
                MessageBox.Show(errorString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}