using System.Windows;
using System.Windows.Controls;


namespace task_11.Windows
{
    /// <summary>
    /// Interaction logic for EditUserInfoWindow.xaml
    /// </summary>
    public partial class EditUserInfoWindow : Window
    {
        ListView l;
        Repository r;
        ButtonIsActive buttonIsActive;

        public EditUserInfoWindow(ListView l, Repository r)
        {
            InitializeComponent();
            this.l = l;
            this.r = r;
            buttonIsActive = new ButtonIsActive(true);
            cbDepartment.ItemsSource = r.AllDepartments;

            User selectedUser = (User)l.SelectedItem;

            tbSurname.Text = selectedUser.Surname;
            tbName.Text = selectedUser.Name;
            tbPatronymic.Text = selectedUser.Patronymic;
            cbDepartment.Text = selectedUser.Department.Name;
            tbPhoneNumber.Text = selectedUser.PhoneNumber.ReturnSimpleNumber();
            tbSeries.Text = selectedUser.PassportSeries;
            tbNumber.Text = selectedUser.PassportNumber.ToString();
        }

        /// <summary>
        /// Осуществляет редактирование клиента.
        /// Возвращает true, если редактирование прошло успешно
        /// </summary>
        /// <param name="previousUser"></param>
        /// <returns></returns>
        private bool CommitChanges(User previousUser)
        {
            Manager m = new Manager(r);

            m.ChangeSurname((User)l.SelectedItem, tbSurname.Text);

            m.ChangeName((User)l.SelectedItem, tbName.Text);

            m.ChangePatronimic((User)l.SelectedItem, tbPatronymic.Text);

            m.ChangeDepartment((User)l.SelectedItem, cbDepartment.Text);

            m.ChangeSeries((User)l.SelectedItem, tbSeries.Text);

            m.ChangeNumber((User)l.SelectedItem, int.Parse(tbNumber.Text));

            return m.ChangePhoneNumber((User)l.SelectedItem, tbPhoneNumber.Text);
        }

        private void bSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            Manager m = new Manager(r);
            User previousUser = (User)l.SelectedItem;

            if (CommitChanges(previousUser))
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

        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.surnameNotEmpty = tbSurname.Text.Length != 0;
            bSaveChanges.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.nameNotEmpty = tbName.Text.Length != 0;
            bSaveChanges.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.patronymicNotEmpty = tbPatronymic.Text.Length != 0;
            bSaveChanges.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.phoneNumberNotEmpty = tbPhoneNumber.Text.Length > 10;
            bSaveChanges.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.seriesNotEmpty = tbSeries.Text.Length == 2;
            bSaveChanges.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.numberNotEmpty = tbNumber.Text.Length == 7;
            bSaveChanges.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void cbChooseDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonIsActive.departmentNotEmpty = cbDepartment.Text.Length != 0; ;
            bSaveChanges.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }
    }
}

