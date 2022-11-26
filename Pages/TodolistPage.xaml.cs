using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinkToDo.Pages
{
    /// <summary>
    /// TodolistPage.xaml 的交互逻辑
    /// </summary>
    public partial class TodolistPage : Page
    {
        bool isShowMore { get; set; } = true;
        public TodolistPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInAnimation(sender);
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


        private void moreBtn_Click(object sender, RoutedEventArgs e)
        {
            isShowMore = !isShowMore;
            double from = isShowMore ? 0 : 90;
            double to = isShowMore ? 90 : 0;
            todoList2.Visibility = isShowMore ? Visibility.Visible : Visibility.Collapsed;
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(0.3),
                DecelerationRatio = 0.5
            };
            Storyboard.SetTarget(doubleAnimation, moreIcon);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.(RotateTransform.Angle)"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }

        private void todoTaskContentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(todoTaskContentTextBox.Text.Length > 0)
            {
                spFuncArea.Visibility = Visibility.Visible;
            }
            else
            {
                spFuncArea.Visibility = Visibility.Collapsed;
            }
        }

        private void Border_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void todoTaskContentTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("focus");
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine("mouse_down");
            g0Focus0.Visibility = Visibility.Collapsed;
            g0Focus1.Visibility = Visibility.Visible;
            g1Focus0.Visibility = Visibility.Collapsed;
            g1Focus1.Visibility = Visibility.Visible;
            todoTaskContentTextBox.Focus();
        }

        private void todoTaskContentTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("lost_focus");
            todoTaskContentTextBox.Text= null;
            g0Focus0.Visibility = Visibility.Visible;
            g0Focus1.Visibility = Visibility.Collapsed;
            g1Focus0.Visibility = Visibility.Visible;
            g1Focus1.Visibility = Visibility.Collapsed;
        }
    }
}
