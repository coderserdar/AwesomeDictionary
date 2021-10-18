using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;
using AwesomeDictionary.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AwesomeDictionary
{
    public partial class SearchPage : PhoneApplicationPage
    {
        public SearchPage()
        {
            InitializeComponent();
            SetBackgroundColor();

            txtSearchResult.Text = AppResources.SearchResults;
            txtSearchWithMeaning.Text = AppResources.SearchInMeanings;
            lblSearch.Text = AppResources.Search;
            //btnSearch.Content = AppResources.Search;
            //lstSearch.SelectedIndex = -1;
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            lstSearch.Items.Clear();
            var nameList = new List<All>();
            if (txtSearch.Text.TrimStart().TrimEnd().Length < 1)
            {
                MessageBox.Show(AppResources.SearchTrimFault);
            }
            else
            {
                using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
                {
                    if (cbSearchWithMeaning.IsChecked == true)
                    {
                        nameList = context.AllNames.Where(j => j.AllNameMeaning.ToLower().Contains(txtSearch.Text.TrimEnd().TrimStart().ToLower())).ToList() as List<All>;
                    }
                    else
                    {
                        nameList = context.AllNames.Where(j => j.AllName.ToLower().Contains(txtSearch.Text.TrimEnd().TrimStart().ToLower())).ToList() as List<All>;
                    }
                    //var noteList = context.Notes.ToList() as List<Note>;

                    if (nameList != null)
                    {
                        txtSearchResult.Text = AppResources.SearchResults + " (" + nameList.Count() + ")";
                    }

                    //List<All> allNames = nameList.OrderBy(j => j.AllName).ToList() as List<All>;
                    //List<AlphaKeyGroup<All>> DataSource = AlphaKeyGroup<All>.CreateGroups(allNames,
                    //    System.Threading.Thread.CurrentThread.CurrentUICulture,
                    //    (All a) => { return a.AllName; }, true);
                    //llsAllNames.ItemsSource = DataSource;

                    var nameList2 = nameList.OrderBy(j => j.AllName).ToList() as List<All>;
                    //lstSearch.ItemsSource = nameList;
                    for (int i = 0; i < nameList2.Count; i++)
                    {
                        lstSearch.Items.Add(nameList2[i] as All);
                    }
                    //lstSearch.ItemTemplate.
                    //lstSearch.DisplayMemberPath = "NoteName" + " (" + "CreationDate" + ")";
                    lstSearch.DisplayMemberPath = "AllNameSource";
                    MessageBox.Show(AppResources.SearchCompleted);
                }
            }
        }

        private void lstSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstSearch.SelectedIndex != -1)
                {
                    All selectedName = lstSearch.SelectedItem as All;
                    NavigationService.Navigate(new Uri("/NameDetailPage.xaml#" + selectedName.AllId, UriKind.Relative));
                    lstSearch.SelectedIndex = -1;
                }

            }
            catch (Exception)
            {
                MessageBox.Show(AppResources.SystemFault);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        private void txtSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                lstSearch.Items.Clear();
                var nameList = new List<All>();
                if (txtSearch.Text.TrimStart().TrimEnd().Length < 1)
                {
                    MessageBox.Show(AppResources.SearchTrimFault);
                }
                else
                {
                    using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
                    {
                        if (cbSearchWithMeaning.IsChecked == true)
                        {
                            nameList = context.AllNames.Where(j => j.AllNameMeaning.ToLower().Contains(txtSearch.Text.TrimEnd().TrimStart().ToLower())).ToList() as List<All>;
                        }
                        else
                        {
                            nameList = context.AllNames.Where(j => j.AllName.ToLower().Contains(txtSearch.Text.TrimEnd().TrimStart().ToLower())).ToList() as List<All>;
                        }
                        //var noteList = context.Notes.ToList() as List<Note>;

                        if (nameList != null)
                        {
                            txtSearchResult.Text = AppResources.SearchResults + " (" + nameList.Count() + ")";
                        }

                        //List<All> allNames = nameList.OrderBy(j => j.AllName).ToList() as List<All>;
                        //List<AlphaKeyGroup<All>> DataSource = AlphaKeyGroup<All>.CreateGroups(allNames,
                        //    System.Threading.Thread.CurrentThread.CurrentUICulture,
                        //    (All a) => { return a.AllName; }, true);
                        //llsAllNames.ItemsSource = DataSource;

                        var nameList2 = nameList.OrderBy(j => j.AllName).ToList() as List<All>;
                        //lstSearch.ItemsSource = nameList;
                        for (int i = 0; i < nameList2.Count; i++)
                        {
                            lstSearch.Items.Add(nameList2[i] as All);
                        }
                        //lstSearch.ItemTemplate.
                        //lstSearch.DisplayMemberPath = "NoteName" + " (" + "CreationDate" + ")";
                        lstSearch.DisplayMemberPath = "AllNameSource";
                        MessageBox.Show(AppResources.SearchCompleted);
                    }
                }
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //txtSearch.Focus();
        }
    }
}