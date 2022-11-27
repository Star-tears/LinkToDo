using LinkToDo.Myscripts;
using LinkToDo.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinkToDo.Components
{
    /// <summary>
    /// TodoUnit.xaml 的交互逻辑
    /// </summary>
    public partial class TodoUnit : UserControl
    {
        public TodoInfo todoInfo;
        TodolistPage todolistPage;
        public TodoUnit()
        {
            InitializeComponent();
        }

        public TodoUnit(TodolistPage todolistPage, TodoInfo todoInfo)
        {
            InitializeComponent();
            this.todoInfo = todoInfo;
            this.todolistPage = todolistPage;
            Init();
        }
        private void Init()
        {
            todoContentText.Text = todoInfo.Content;
            todoDateTimeText.Text = todoInfo.Date.ToString("yyyy-MM-dd HH:mm:ss");
            todoTeammateListText.Text = todoInfo.Teammate;
            if (todoInfo.IsDone > 0)
            {
                isDoneBtn.IsChecked = true;
            }
            if (todoInfo.Priority > 0)
            {
                isImportantBtn.IsChecked = true;
            }
        }

        private void unCheckPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            checkHoverShow.Visibility = Visibility.Visible;
        }

        private void unCheckPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            checkHoverShow.Visibility = Visibility.Hidden;
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                From = 200,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                DecelerationRatio = 1
            };
            Storyboard.SetTarget(doubleAnimation, (Border)sender);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, (Border)sender);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            storyboard.Children.Add(doubleAnimation2);
            storyboard.Begin();
        }

        private void starBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            starPath.Fill = (SolidColorBrush)this.FindResource("PrimaryBlueColor");
        }

        private void starBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            starPath.Fill = (SolidColorBrush)this.FindResource("PrimaryGrayColor");
        }

        private async void isDoneBtn_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    Console.WriteLine("isDoneBtn: " + isDoneBtn.IsChecked);
                    if (isDoneBtn.IsChecked == true)
                    {
                        todoInfo.IsDone = 1;
                        todolistPage.todoList1.Children.Remove(this);
                        todolistPage.todoList2.Children.Insert(0, this);
                    }
                    else
                    {
                        todoInfo.IsDone = 0;
                        todolistPage.todoList2.Children.Remove(this);
                        int pos = todolistPage.todoList1.Children.Count;
                        for (int i = 0; i < todolistPage.todoList1.Children.Count; i++)
                        {
                            if (todoInfo.CompareTo(((TodoUnit)todolistPage.todoList1.Children[i]).todoInfo) >= 0)
                            {
                                pos = i;
                                break;
                            }
                        }
                        todolistPage.todoList1.Children.Insert(pos, this);
                    }
                }));
            });

        }

        private async void isImportantBtn_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    Console.WriteLine("isImportantBtn: " + isImportantBtn.IsChecked);
                    if (isDoneBtn.IsChecked == false)
                    {
                        todoInfo.Priority = isImportantBtn.IsChecked==true ? 5 : 0;
                        todolistPage.todoList1.Children.Remove(this);
                        int pos = todolistPage.todoList1.Children.Count;
                        for (int i = 0; i < todolistPage.todoList1.Children.Count; i++)
                        {
                            if (todoInfo.CompareTo(((TodoUnit)todolistPage.todoList1.Children[i]).todoInfo) >= 0)
                            {
                                pos = i;
                                break;
                            }
                        }
                        todolistPage.todoList1.Children.Insert(pos, this);
                    }
                }));
            });
        }

        private void isDoneBtn_Checked(object sender, RoutedEventArgs e)
        {
            todoContentText.Opacity = 0.7;
            todoContentText.TextDecorations = TextDecorations.Strikethrough;
            todoTeammateListTextTitle.Opacity = 0.8;
            todoTeammateListText.Opacity = 0.8;
            calenderIcon.Fill = (SolidColorBrush)this.FindResource("TextPrimaryColor");
            calenderIcon.Opacity = 0.7;
            todoDateTimeText.Foreground = (SolidColorBrush)this.FindResource("TextPrimaryColor");
            todoDateTimeText.Opacity = 0.7;
        }

        private void isDoneBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            todoContentText.Opacity = 1;
            todoContentText.TextDecorations = null;
            todoTeammateListTextTitle.Opacity = 1;
            todoTeammateListText.Opacity = 1;
            calenderIcon.Fill = (LinearGradientBrush)this.FindResource("DangerBrush");
            calenderIcon.Opacity = 1;
            todoDateTimeText.Foreground = (LinearGradientBrush)this.FindResource("DangerBrush");
            todoDateTimeText.Opacity = 1;
        }

        private void mainBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            mainBorder.Background = (SolidColorBrush)this.FindResource("TEAL_A");
        }

        private void mainBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            mainBorder.Background = (SolidColorBrush)this.FindResource("PrimaryBackgroundColor");
        }
    }
}
