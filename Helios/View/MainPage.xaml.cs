namespace Helios.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using Helios.Resources;
    using Helios.Model;

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //Settings.Clip = new Clip(10, 40, 50);
            //TheSlider.DataContext = new ClipWrapper(Settings.Clip);
        }
    }
}