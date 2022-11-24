using LinkToDo.Myscripts;
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
    /// AddressUnit.xaml 的交互逻辑
    /// </summary>
    public partial class AddressUnit : UserControl
    {

        public bool IsChecked { get; set; } = false;
        public UserInfo userInfo { get; set; }
        public AddressUnit()
        {
            InitializeComponent();
        }
        public AddressUnit(UserInfo uI)
        {
            InitializeComponent();
            userInfo = uI;
            nameLabel.Content = userInfo.Name;
            phoneLabel.Content = userInfo.PhoneNum;
            emailLabel.Content = userInfo.Email;
            img.Source=userInfo.getImg();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = mainBorder.Width,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = 195,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(mainBorder, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(mainBorder, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }

        private Timeline CreatDoubleAnimation(UIElement uIElement, string propertyPath, DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
        {
            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return doubleAnimationUsingKeyFrames;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = 195,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            int target_num = 180;
            if (IsChecked) target_num = 190;
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = target_num,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(mainBorder, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(mainBorder, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsChecked = !IsChecked;
            Console.WriteLine(IsChecked.ToString());
            if (IsChecked)
            {
                Storyboard storyboard = new Storyboard();
                ColorAnimationUsingKeyFrames colorAnimationUsingKeyFrames = new ColorAnimationUsingKeyFrames();
                EasingColorKeyFrame easingColorKeyFrame1 = new EasingColorKeyFrame()
                {
                    Value = new Color()
                    {
                        R = 249,
                        G = 249,
                        B = 249,
                        A = 255
                    },
                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut },
                    KeyTime = TimeSpan.FromSeconds(0)
                };
                EasingColorKeyFrame easingColorKeyFrame2 = new EasingColorKeyFrame()
                {
                    Value = new Color()
                    {
                        R = 255,
                        G = 85,
                        B = 85,
                        A = 255
                    },
                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut },
                    KeyTime = TimeSpan.FromSeconds(0.5)
                };
                colorAnimationUsingKeyFrames.KeyFrames.Add(easingColorKeyFrame1);
                colorAnimationUsingKeyFrames.KeyFrames.Add(easingColorKeyFrame2);

                storyboard.Children.Add(CreatColorAnimation(mainBorder, "Background.(SolidColorBrush.Color)", colorAnimationUsingKeyFrames));

                storyboard.Begin();
            }
            else
            {
                Storyboard storyboard = new Storyboard();
                ColorAnimationUsingKeyFrames colorAnimationUsingKeyFrames = new ColorAnimationUsingKeyFrames();
                EasingColorKeyFrame easingColorKeyFrame1 = new EasingColorKeyFrame()
                {
                    Value = new Color()
                    {
                        R = 255,
                        G = 85,
                        B = 85,
                        A = 255
                    },

                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut },
                    KeyTime = TimeSpan.FromSeconds(0)
                };
                EasingColorKeyFrame easingColorKeyFrame2 = new EasingColorKeyFrame()
                {
                    Value = new Color()
                    {
                        R = 249,
                        G = 249,
                        B = 249,
                        A = 255
                    },
                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut },
                    KeyTime = TimeSpan.FromSeconds(0.5)
                };
                colorAnimationUsingKeyFrames.KeyFrames.Add(easingColorKeyFrame1);
                colorAnimationUsingKeyFrames.KeyFrames.Add(easingColorKeyFrame2);

                storyboard.Children.Add(CreatColorAnimation(mainBorder, "Background.(SolidColorBrush.Color)", colorAnimationUsingKeyFrames));

                storyboard.Begin();
            }
        }
        private Timeline CreatColorAnimation(UIElement uIElement, string propertyPath, ColorAnimationUsingKeyFrames colorAnimationUsingKeyFrames)
        {
            Storyboard.SetTarget(colorAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(colorAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return colorAnimationUsingKeyFrames;
        }
    }
}