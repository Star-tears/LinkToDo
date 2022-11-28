using HandyControl.Controls;
using LinkToDo.Components;
using LinkToDo.Myscripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private TodoDataControl todoDataControl;

        private bool isShowMore { get; set; } = true;
        public TodolistPage()
        {
            InitializeComponent();
            Init();

        }
        private void Init()
        {
            Refresh();
        }
        private void Refresh()
        {
            Task.Run(() =>
            {
                todoDataControl = new TodoDataControl();
                List<TodoInfo> todoUnitList0, todoUnitList1, todoUnitList2;
                todoUnitList0 = new List<TodoInfo>();
                todoUnitList1 = new List<TodoInfo>();
                todoUnitList2 = new List<TodoInfo>();
                DataTable dt = todoDataControl.queryTodoInfo();
                foreach (DataRow row in dt.Rows)
                {
                    TodoInfo tmp_todoInfo = new TodoInfo((string)row[0], (string)row[1], (DateTime)row[2], (int)row[3], (int)row[4], (string)row[5]);
                    if (tmp_todoInfo.IsDone == 0 && tmp_todoInfo.Priority > 0)
                    {
                        todoUnitList0.Add(tmp_todoInfo);
                    }
                    else if (tmp_todoInfo.IsDone == 0)
                    {
                        todoUnitList1.Add(tmp_todoInfo);
                    }
                    else
                    {
                        todoUnitList2.Add(tmp_todoInfo);
                    }
                }
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    todoList0.Children.Clear();
                    foreach (TodoInfo sub_todoInfo in todoUnitList0)
                    {
                        todoList0.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    todoList1.Children.Clear();
                    foreach (TodoInfo sub_todoInfo in todoUnitList1)
                    {
                        todoList1.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    todoList2.Children.Clear();
                    foreach (TodoInfo sub_todoInfo in todoUnitList2)
                    {
                        todoList2.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }

                }));
                Refresh_TodoDoneCount();
            });
        }
        public void UpdateTodoInfo(TodoInfo todoInfo)
        {
            Task.Run(() =>
            {
                TodoDataControl tmp_todoDataControl = new TodoDataControl();
                tmp_todoDataControl.updateTodoInfo(todoInfo);
                Refresh_TodoDoneCount();
            });
        }
        public void DeleteTodoInfo(TodoInfo todoInfo)
        {
            Task.Run(() =>
            {
                TodoDataControl tmp_todoDataControl = new TodoDataControl();
                tmp_todoDataControl.deleteTodoInfo(todoInfo);
                Refresh_TodoDoneCount();
            });
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
            if (todoTaskContentTextBox.Text.Length > 0)
            {
                spFuncArea.Visibility = Visibility.Visible;
            }
            else
            {
                spFuncArea.Visibility = Visibility.Collapsed;
            }
        }

        private void todoTaskContentTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("todoTaskContentTextBox: " + "focus");

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("border: " + "mouse_down");
            g0Focus0.Visibility = Visibility.Collapsed;
            g0Focus1.Visibility = Visibility.Visible;
            g1Focus0.Visibility = Visibility.Collapsed;
            g1Focus1.Visibility = Visibility.Visible;
            dateTimePickers.SelectedDateTime = DateTime.Now.AddDays(7);
            todoTaskContentTextBox.Focus();
        }

        private void todoTaskContentTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("todoTaskContentTextBox: " + "lost_focus");
            //todoTaskContentTextBox.Text = null;
            //g0Focus0.Visibility = Visibility.Visible;
            //g0Focus1.Visibility = Visibility.Collapsed;
            //g1Focus0.Visibility = Visibility.Visible;
            //g1Focus1.Visibility = Visibility.Collapsed;
        }
        private void todoTaskContentTextBox_LostFocus()
        {
            todoTaskContentTextBox.Text = null;
            g0Focus0.Visibility = Visibility.Visible;
            g0Focus1.Visibility = Visibility.Collapsed;
            g1Focus0.Visibility = Visibility.Visible;
            g1Focus1.Visibility = Visibility.Collapsed;
        }

        private void todoTaskContentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (todoTaskContentTextBox.Text.Length > 0)
                {
                    TodoInfo tmp_todoInfo = new TodoInfo(MyUtils.genUUID(), todoTaskContentTextBox.Text, dateTimePickers.SelectedDateTime.Value, 0, 0, teammateList.Text);
                    Task.Run(() =>
                   {
                       todoDataControl.insertTodoInfo(tmp_todoInfo);
                   });
                    todoTaskContentTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
                else
                {
                    Growl.Info("未输入任务内容");
                }
            }
        }


        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void todolistPanelScr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            todoTaskContentTextBox_LostFocus();
        }

        private void Refresh_TodoDoneCount()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                todoDoneCount.Text = todoList2.Children.Count.ToString();
            }));
        }
    }
}
