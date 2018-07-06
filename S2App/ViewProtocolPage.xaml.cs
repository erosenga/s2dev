﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace S2App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewProtocolPage : Page
    {
        public ViewProtocolPage()
        {
            this.InitializeComponent();
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
                        ((Frame)Window.Current.Content).Navigate(typeof(WelcomeUserPage));
        }

        private void ButtonProtocols_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRunLog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAdministration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

        private void PrepareSampleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditProtocolButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
