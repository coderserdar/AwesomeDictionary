﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data.Common;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Primitives;
using Microsoft.Phone.Shell;
using AwesomeDictionary.Resources;

namespace AwesomeDictionary
{
    public partial class FontSizeSettingsPage : PhoneApplicationPage
    {
        public FontSizeSettingsPage()
        {
            InitializeComponent();

            lstFontSize.Items.Clear();
            lstFontSize.Items.Add("14");
            lstFontSize.Items.Add("18");
            lstFontSize.Items.Add("22");
            lstFontSize.Items.Add("26");
            lstFontSize.Items.Add("28");
            lstFontSize.Items.Add("30");
            lstFontSize.Items.Add("32");
            lstFontSize.Items.Add("34");
            lstFontSize.Items.Add("36");
            lstFontSize.Items.Add("38");
            lstFontSize.Items.Add("40");
            lstFontSize.Items.Add("42");
            lstFontSize.Items.Add("44");
            lstFontSize.Items.Add("64");
            lstFontSize.Items.Add("72");
            lstFontSize.SelectedIndex = -1;

            lblGeneralSettings.Text = AppResources.GeneralSettings;
            lblFontSize.Text = AppResources.SelectFontSize;

            SetBackgroundColor();
        }

        private void lstFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFontSize.SelectedIndex != -1)
            {
                using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
                {
                    var appSettings = context.AppSettings;
                    foreach (var item in appSettings)
                    {
                        item.FontSize = lstFontSize.SelectedItem.ToString();
                    }
                    context.SubmitChanges();
                    MessageBox.Show(AppResources.FontSizeChangedSuccessfully);
                }
            }
            NavigationService.Navigate(new Uri("/GeneralSettingsPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        protected override void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            // displays "Fragment: Detail"
            //MessageBox.Show("Folder Id: " + e.Fragment);
            base.OnFragmentNavigation(e);
            //artistId = int.Parse(e.Fragment);
            //using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            //{
            //    var artist = context.Artists.Where(j => j.ArtistId.Equals(artistId)).Single() as Artist;
            //    lblArtistName.Text = artist.ArtistName;
            //    lblFontSize.Text = AppResources.SelectFontSize;
            //}
            
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.Navigate(new Uri("/GeneralSettingsPage.xaml", UriKind.Relative));
            }
        }

        private void SetBackgroundColor()
        {
            AppSettings appSettings = new AppSettings();
            using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            {
                appSettings = context.AppSettings.First() as AppSettings;
            }

            if (appSettings.AppBackgroundImage != null)
            {
                MemoryStream stream = new MemoryStream(appSettings.AppBackgroundImage);
                BitmapImage image = new BitmapImage();
                image.SetSource(stream);
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = image;
                this.LayoutRoot.Background = ib;
            }
            else
            {
                switch (appSettings.AppBackgroundColor)
                {
                    case "BLA":
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Black);
                        break;
                    case "BLU":
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Blue);
                        break;
                    case "BRO":
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Brown);
                        break;
                    case "RED":
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case "GRE":
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Green);
                        break;
                    case "GRA":
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Gray);
                        break;
                    case "YEL":
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case "ORA":
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Orange);
                        break;
                    case "PUR":
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Purple);
                        break;
                    default:
                        this.LayoutRoot.Background = new SolidColorBrush(Colors.Black);
                        break;
                }
            }
        }
    }
}