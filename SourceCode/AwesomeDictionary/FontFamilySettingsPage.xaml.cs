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
    public partial class FontFamilySettingsPage : PhoneApplicationPage
    {
        public FontFamilySettingsPage()
        {
            InitializeComponent();

            lstFontFamily.Items.Clear();
            lstFontFamily.Items.Add("Arial");
            lstFontFamily.Items.Add("Arial Black");
            lstFontFamily.Items.Add("Baskerville Old Face");
            lstFontFamily.Items.Add("Berlin Sans FB");
            lstFontFamily.Items.Add("Albumman Old Style");
            lstFontFamily.Items.Add("Calibri");
            lstFontFamily.Items.Add("Cambria");
            lstFontFamily.Items.Add("Candara");
            lstFontFamily.Items.Add("Comic Sans MS");
            lstFontFamily.Items.Add("Consolas");
            lstFontFamily.Items.Add("Constantia");
            lstFontFamily.Items.Add("Courier New");
            lstFontFamily.Items.Add("DokChampa");
            lstFontFamily.Items.Add("Ebrima");
            lstFontFamily.Items.Add("Georgia");
            lstFontFamily.Items.Add("Lucida Sans Unicode");
            lstFontFamily.Items.Add("Meiryo UI");
            lstFontFamily.Items.Add("Microsoft YaHei");
            lstFontFamily.Items.Add("Malgun Gothic");
            lstFontFamily.Items.Add("Segoe UI");
            lstFontFamily.Items.Add("Segoe WP");
            lstFontFamily.Items.Add("Tahoma");
            lstFontFamily.Items.Add("Trebuchet MS");
            lstFontFamily.Items.Add("Times New Roman");
            lstFontFamily.Items.Add("Verdana");
            lstFontFamily.SelectedIndex = -1;

            lblFontFamily.Text = AppResources.SelectFontFamily;
            lblGeneralSettings.Text = AppResources.GeneralSettings;

            SetBackgroundColor();
        }

        private void lstFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFontFamily.SelectedIndex != -1)
            {
                using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
                {
                    var appSettings = context.AppSettings;
                    foreach (var item in appSettings)
                    {
                        item.FontFamily = lstFontFamily.SelectedItem.ToString();
                    }
                    context.SubmitChanges();
                    MessageBox.Show(AppResources.FontFamilyChangedSuccessfully);
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
            //    lblFontFamily.Text = AppResources.SelectFontFamily;
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