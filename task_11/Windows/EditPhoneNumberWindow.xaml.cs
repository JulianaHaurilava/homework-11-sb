using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Shapes;

namespace task_11.Windows
{
    /// <summary>
    /// Interaction logic for EditPhoneNumberWindow.xaml
    /// </summary>
    public partial class EditPhoneNumberWindow : Window
    {
        ListView l;
        Repository r;

        public EditPhoneNumberWindow(ListView l, Repository r)
        {
            InitializeComponent();
            this.l = l;
            this.r = r;
            cbDepartment.ItemsSource = r.AllDepartments;

            User selectedUser = (User)l.SelectedItem;
            tbSurname.Text = selectedUser.Surname;
            tbName.Text = selectedUser.Name;
            tbPatronymic.Text = selectedUser.Patronymic;
            cbDepartment.Text = selectedUser.Department.Name;
            tbPhoneNumber.Text = selectedUser.PhoneNumber.ReturnSimpleNumber();
            tbSeries.Text = selectedUser.HiddenPassportSeries;
            tbNumber.Text = selectedUser.HiddenPassportNumber;
        }

        private void bSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            Consultant c = new Consultant(r);
            if (c.ChangePhoneNumber((User)l.SelectedItem, tbPhoneNumber.Text))
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

        private void tbPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            bSaveChanges.IsEnabled = tbPhoneNumber.Text.Length > 10;
        }
    }
}
