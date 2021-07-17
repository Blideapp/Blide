using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Blide
{
    /*
     * adds toggle button to WPF
     */
    public partial class ToggleButton : UserControl
    {
        Thickness LeftSide = new Thickness(-39, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -39, 0);
        SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(200, 200, 200));
        SolidColorBrush On = new SolidColorBrush(Color.FromRgb(33, 150, 243));
        private bool Toggled = false;

        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = Off;
            Toggled = false;
            Dot.Margin = LeftSide;
        }

        public bool Toggled1 { get => Toggled; set => Toggled = value; }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;

            }
            else
            {

                Back.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;

            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;

            }
            else
            {

                Back.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;

            }

        }

        public void setStatus(Boolean status)
        {
            if (status)
            {
                Back.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;

            }
            else
            {
                Back.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;
            }

        }
        public Boolean getStatus() 
        {
            return Toggled;
        }
    }
}
