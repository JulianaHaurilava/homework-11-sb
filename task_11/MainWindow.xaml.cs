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
            switch ((string)cbWorkerType.SelectedItem)
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
            lClientInfo.ItemsSource = r.AllUsers.Where(findClient);
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

        private void SortByFio(object sender, RoutedEventArgs e)
        {

        }

        private void ToAddNewClientMenu(object sender, RoutedEventArgs e)
        {
            AddNewUserWindow addNewUserWindow = new AddNewUserWindow(lClientInfo, r, r.AllDepartments);
            addNewUserWindow.Owner = this;
            addNewUserWindow.ShowDialog();
        }

        private void ToEditUserMenuC(object sender, RoutedEventArgs e)
        {
            EditPhoneNumberWindow addNewUserWindow = new EditPhoneNumberWindow(lClientInfo, r);
            addNewUserWindow.ShowDialog();
        }

        private void ToMenuM(object sender, RoutedEventArgs e)
        {
            lClientInfo.SelectedItem = null;
            windowSwitcher.ChangeState(spMenuM);
        }
        private void ToMenuC(object sender, RoutedEventArgs e)
        {
            lClientInfo.SelectedItem = null;
            windowSwitcher.ChangeState(spMenuC);
        }
    }
}
