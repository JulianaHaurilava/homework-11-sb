using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using task_11.Windows;

namespace task_11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Вложенный класс для переключения StackPanels
        /// </summary>
        class StateMachine
        {
            private StackPanel spCurrentPanel;

            public void ChangeState(StackPanel chosenMenu)
            {
                if (spCurrentPanel != null)
                {
                    spCurrentPanel.Visibility = Visibility.Hidden;
                }
                chosenMenu.Visibility = Visibility.Visible;
                spCurrentPanel = chosenMenu;
            }
        }

        string workerType;
        StateMachine windowSwitcher;
        public Repository r;
        public MainWindow()
        {
            InitializeComponent();

            r = new Repository("all_users.txt", "all_departments.txt");
            windowSwitcher = new StateMachine();
            string[] workerTypes = { "Консультант", "Менеджер" };
            cbWorkerType.ItemsSource = workerTypes;
            cbDepartment.ItemsSource = r.AllDepartments;
        }

        private bool findClient(User user)
        {
            return user.Department.Name == (cbDepartment.SelectedItem as Department).Name;
        }

        private void cbWorkerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbDepartment.SelectedItem = null;
            lClientInfo.ItemsSource = null;

            cbDepartment.IsEnabled = true;
            workerType = (string)cbWorkerType.SelectedItem;
            switch (workerType)
            {
                case "Консультант":
                    gvcPassportSeries.DisplayMemberBinding = new Binding("HiddenPassportSeries");
                    gvcPassportNumber.DisplayMemberBinding = new Binding("HiddenPassportNumber");
                    windowSwitcher.ChangeState(spMenuC);
                    break;
                case "Менеджер":
                    gvcPassportSeries.DisplayMemberBinding = new Binding("PassportSeries");
                    gvcPassportNumber.DisplayMemberBinding = new Binding("PassportNumber");
                    windowSwitcher.ChangeState(spMenuM);
                    break;
            }
        }
        private void cbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDepartment.SelectedItem != null)
            {
                lClientInfo.ItemsSource = r.AllUsers.Where(findClient);
            }
        }

        private void lClientInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((string)cbWorkerType.SelectedItem)
            {
                case "Консультант":
                    windowSwitcher.ChangeState(spUserMenuC);
                    break;
                case "Менеджер":
                    windowSwitcher.ChangeState(spUserMenuM);
                    break;
            }
        }


        /// <summary>
        /// Сортирует компоненты listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByFio(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lClientInfo.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Patronymic", ListSortDirection.Ascending));
        }

        /// <summary>
        /// Показывает окно для добавления нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToAddNewClientMenu(object sender, RoutedEventArgs e)
        {
            AddNewUserWindow addNewUserWindow = new AddNewUserWindow(lClientInfo, r, r.AllDepartments);
            addNewUserWindow.Owner = this;
            addNewUserWindow.ShowDialog();
            if (addNewUserWindow.DialogResult == true)
            {
                lClientInfo.ItemsSource = r.AllUsers.Where(findClient);
            }
        }

        /// <summary>
        /// Показывает окно для редактирования клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToEditUserMenu(object sender, RoutedEventArgs e)
        {
            switch (workerType)
            {
                case "Консультант":
                    EditPhoneNumberWindow editPhoneNumberWindow = new EditPhoneNumberWindow(lClientInfo, r);
                    editPhoneNumberWindow.ShowDialog();
                    break;
                case "Менеджер":
                    EditUserInfoWindow editUserInfoWindow = new EditUserInfoWindow(lClientInfo, r);
                    editUserInfoWindow.ShowDialog();
                    break;
            }
        }

        /// <summary>
        /// Осуществляет переход из меню работы с клиентом в главное меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnToMenu(object sender, RoutedEventArgs e)
        {
            lClientInfo.SelectedItem = null;
            switch (workerType)
            {
                case "Консультант":
                    windowSwitcher.ChangeState(spMenuC);
                    break;
                case "Менеджер":
                    windowSwitcher.ChangeState(spMenuM);
                    break;
            }
            
        }

        /// <summary>
        /// Осуществляет удаление клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранного клиента?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Manager m = new Manager(r);
                m.DeleteUser((User)lClientInfo.SelectedItem);
                lClientInfo.ItemsSource = r.AllUsers.Where(findClient);

                lClientInfo.SelectedItem = null;
                windowSwitcher.ChangeState(spMenuM);
            }
        }
    }
}
