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
                Duration = TimeSpan.FromSeconds(0.6)
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                From = 200,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.6),
                DecelerationRatio = 1
            };
            Storyboard.SetTarget(doubleAnimation, mainBorder);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, mainBorder);
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

        private void isDoneBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("isDoneBtn: " + isDoneBtn.IsChecked);
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.8)
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromSeconds(0.8),
                DecelerationRatio = 1
            };
            Storyboard.SetTarget(doubleAnimation, mainBorder);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, mainBorder);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            storyboard.Children.Add(doubleAnimation2);
            storyboard.Begin();

            todoInfo.IsDone = isDoneBtn.IsChecked == true ? 1 : 0;
            todolistPage.UpdateTodoInfo(todoInfo);
            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {

                    if (isDoneBtn.IsChecked == false)
                    {
                        todolistPage.todoList2.Children.Remove(this);
                    }
                    else if (isImportantBtn.IsChecked == true)
                    {
                        todolistPage.todoList0.Children.Remove(this);
                    }
                    else
                    {
                        todolistPage.todoList1.Children.Remove(this);
                    }
                    addTodoUnitIntoTodoList();
                }));
            });

        }

        private void isImportantBtn_Click(object sender, RoutedEventArgs e)
        {
            todoInfo.Priority = isImportantBtn.IsChecked == true ? 5 : 0;
            todolistPage.UpdateTodoInfo(todoInfo);
            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    if (isDoneBtn.IsChecked == true)
                    {
                        todolistPage.todoList2.Children.Remove(this);
                    }
                    else if (isImportantBtn.IsChecked == true)
                    {
                        todolistPage.todoList1.Children.Remove(this);
                    }
                    else
                    {
                        todolistPage.todoList0.Children.Remove(this);
                    }
                    addTodoUnitIntoTodoList();

                }));
            });

        }
        public void addTodoUnitIntoTodoList()
        {
            if (isDoneBtn.IsChecked == true)
            {
                todolistPage.todoList2.Children.Insert(0, this);
            }
            else if (isImportantBtn.IsChecked == true)
            {
                int pos = todolistPage.todoList0.Children.Count;
                // 二分查找第一个小于this的TodoUnit
                int l = 0, r = pos - 1;
                while (l <= r)
                {
                    int mid = (l + r) >> 1;
                    if (todoInfo.CompareTo(((TodoUnit)todolistPage.todoList0.Children[mid]).todoInfo) >= 0)
                    {
                        pos = mid;
                        r = mid - 1;
                    }
                    else
                    {
                        l = mid + 1;
                    }
                }
                todolistPage.todoList0.Children.Insert(pos, this);
            }
            else
            {
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

        private void deleteMI_Click(object sender, RoutedEventArgs e)
        {
            todolistPage.DeleteTodoInfo(todoInfo);
            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {

                    if (isDoneBtn.IsChecked == true)
                    {
                        todolistPage.todoList2.Children.Remove(this);
                    }
                    else if (isImportantBtn.IsChecked == true)
                    {
                        todolistPage.todoList0.Children.Remove(this);
                    }
                    else
                    {
                        todolistPage.todoList1.Children.Remove(this);
                    }
                }));
            });
        }
    }
}
