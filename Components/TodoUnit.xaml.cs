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
        public TodoUnit()
        {
            InitializeComponent();
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
    }
}
