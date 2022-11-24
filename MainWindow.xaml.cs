using LinkToDo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinkToDo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        AddressbookPage addressbookPage;
        TodolistPage todolistPage;
        public MainWindow()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            addressbookPage=new AddressbookPage();
            todolistPage=new TodolistPage();
            PagesNavigation.Navigate(addressbookPage);
        }
        private void rdAddressbook_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(addressbookPage);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdTodolist_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(todolistPage);
        }
    }
}
