﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace task_11.Windows
{
    /// <summary>
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    public partial class AddNewUserWindow : Window
    {
        ListView l;
        Repository r;

        ButtonIsActive buttonIsActive;
        public User userToAdd;
        public AddNewUserWindow(ListView l, Repository r, List<Department> allDepartments)
        {
            InitializeComponent();
            buttonIsActive = new ButtonIsActive(false);
            cbDepartment.ItemsSource = allDepartments;
            this.l = l;
            this.r = r;
        }

        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.surnameNotEmpty = tbSurname.Text.Length != 0;
            bAddNewClient.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.nameNotEmpty = tbName.Text.Length != 0;
            bAddNewClient.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.patronymicNotEmpty = tbPatronymic.Text.Length != 0;
            bAddNewClient.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.phoneNumberNotEmpty = tbPhoneNumber.Text.Length != 0;
            bAddNewClient.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.seriesNotEmpty = tbSeries.Text.Length != 0;
            bAddNewClient.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonIsActive.numberNotEmpty = tbNumber.Text.Length != 0;
            bAddNewClient.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void cbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonIsActive.departmentNotEmpty = cbDepartment.Text.Length != 0; ;
            bAddNewClient.IsEnabled = buttonIsActive.ButtonIsEnabled();
        }

        private void bAddNewClient_Click(object sender, RoutedEventArgs e)
        {
            userToAdd = new User(tbSurname.Text, tbName.Text, tbPatronymic.Text,
                tbPhoneNumber.Text, cbDepartment.Text, tbSeries.Text, int.Parse(tbNumber.Text));
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