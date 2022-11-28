using LinkToDo.Components;
using LinkToDo.Myscripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace LinkToDo.Pages
{
    /// <summary>
    /// AddressbookPage.xaml 的交互逻辑
    /// </summary>
    public partial class AddressbookPage : Page
    {
        public bool isLoaded { get; set; } = false;
        public AddressbookPage()
        {
            InitializeComponent();
        }
        public void Refresh()
        {
            UserDataControl userDataControl = new UserDataControl();
            DataTable dt = userDataControl.queryUserInfo();
            Dispatcher.BeginInvoke(new Action(delegate
            {
                wrapPanel.Children.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    UserInfo userInfo = new UserInfo(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString());
                    AddressUnit addressUnit = new AddressUnit(userInfo);
                    wrapPanel.Children.Add(addressUnit);
                }
            }));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInAnimation(sender);
            if (!isLoaded)
            {
                isLoaded = true;
                await Task.Run(Refresh);
            }
        }
        private void LoadInAnimation(object sender)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 0.4,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.6),
                DecelerationRatio = 0.6
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                From = 50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.8),
                DecelerationRatio = 0.6
            };
            Storyboard.SetTarget(doubleAnimation, (Page)sender);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, (Page)sender);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            storyboard.Children.Add(doubleAnimation2);
            storyboard.Begin();
        }

        private void insertpersonBtn_Click(object sender, RoutedEventArgs e)
        {
            UserInfo userInfo = new UserInfo(UserInfo.genUUID(), "", "", "", "default.jpg");
            AddressUnitEdit addressUnitEdit = new AddressUnitEdit(this, userInfo);
            NavigationService.GetNavigationService(this).Navigate(addressUnitEdit);
        }

        private async void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(Refresh);
        }

        private void deletepersonBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDataControl userDataControl = new UserDataControl();
            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    List<AddressUnit> li = new List<AddressUnit>();
                    foreach (AddressUnit addressUnit in wrapPanel.Children)
                    {
                        if (addressUnit.IsChecked)
                        {
                            userDataControl.deleteUserInfo(addressUnit.userInfo);
                            li.Add(addressUnit);
                        }
                    }
                    foreach (AddressUnit addressUnit1 in li)
                    {
                        wrapPanel.Children.Remove(addressUnit1);
                    }
                    //await Task.Run(Refresh);
                }));
            });

        }

        private void updatepersonBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (AddressUnit addressUnit in wrapPanel.Children)
            {
                if (addressUnit.IsChecked)
                {
                    AddressUnitEdit addressUnitEdit = new AddressUnitEdit(this, addressUnit.userInfo, 1);
                    NavigationService.GetNavigationService(this).Navigate(addressUnitEdit);
                    break;
                }
            }
        }
    }
}
