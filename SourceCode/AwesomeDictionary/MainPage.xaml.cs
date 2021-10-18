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
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton button2 = new ApplicationBarIconButton();
            button2.IconUri = new Uri("/Assets/Search.png", UriKind.Relative);
            button2.Text = AppResources.Search;
            ApplicationBar.Buttons.Add(button2);
            button2.Click += new EventHandler(SearchButton_Click);

            ApplicationBarIconButton button3 = new ApplicationBarIconButton();
            button3.IconUri = new Uri("/Assets/Settings.png", UriKind.Relative);
            button3.Text = AppResources.Settings;
            ApplicationBar.Buttons.Add(button3);
            button3.Click += new EventHandler(SettingsButton_Click);

            //ApplicationBarIconButton button4 = new ApplicationBarIconButton();
            //button4.IconUri = new Uri("/Assets/Statistics.png", UriKind.Relative);
            //button4.Text = AppResources.Statistics;
            //ApplicationBar.Buttons.Add(button4);
            //button4.Click += new EventHandler(StatisticsButton_Click);

            ApplicationBarMenuItem menuItem1 = new ApplicationBarMenuItem();
            menuItem1.Text = AppResources.About;
            ApplicationBar.MenuItems.Add(menuItem1);
            menuItem1.Click += new EventHandler(AboutMenuItem_Click);

            SetBackgroundColor();

            piFavourite.Header = AppResources.MyFavourites;
            piRandomWords.Header = AppResources.RandomWords;

        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show(AppResources.ExitAppQuestion,
                AppResources.ExitApp, MessageBoxButton.OKCancel)
                != MessageBoxResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Terminate();
            }
        }

        private void pNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString);
            if (pNames.SelectedIndex == 0)
            {
                List<Favourite> favourites = context.Favourites.OrderBy(j => j.FavouriteName).ToList() as List<Favourite>;
                List<All> allNames = new List<All>();
                for (int i = 0; i < favourites.Count; i++)
                {
                    var allName = context.AllNames.Where(j => j.AllId.Equals(favourites[i].FavouriteAllId)).SingleOrDefault() as All;
                    allNames.Add(allName);
                }
                List<AlphaKeyGroup<All>> DataSource = AlphaKeyGroup<All>.CreateGroups(allNames,
                   System.Threading.Thread.CurrentThread.CurrentUICulture,
                   (All a) => { return a.AllName; }, true);
                llsFavourites.ItemsSource = DataSource;
            }

            else if (pNames.SelectedIndex == 1)
            {
                List<All> allNames = new List<All>();
                if (context.AllNames.ToList().Count > 0)
                {
                    List<int> sayiListesi = new List<int>();
                    Random random = new Random();
                    int tableCount = context.AllNames.ToList().Count;
                    for (int i = 0; i < 10; i++)
                    {
                        int sayi = random.Next(i, tableCount);
                        sayiListesi.Add(sayi);
                    }
                    for (int k = 0; k < sayiListesi.Count(); k++)
                    {
                        //int sayi = random.Next(0, 500000);
                        var allName = context.AllNames.Where(j => j.AllId.Equals(sayiListesi[k])).SingleOrDefault() as All;
                        //var allName = context.AllNames.Where(j => j.AllId.Equals(sayi)).SingleOrDefault() as All;
                        if (allName != null)
                        {
                            allNames.Add(allName);
                        }
                        //if(allNames.Count == 10)
                        //{
                        //    break;
                        //}
                    }
                }
                List<AlphaKeyGroup<All>> DataSource = AlphaKeyGroup<All>.CreateGroups(allNames,
                   System.Threading.Thread.CurrentThread.CurrentUICulture,
                   (All a) => { return a.AllName; }, true);
                llsRandomWords.ItemsSource = DataSource;
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/GeneralSettingsPage.xaml", UriKind.Relative));
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.Relative));
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
        private void StatisticsButton_Click(object sender, EventArgs e)
        {
            //NavigationService.Navigate(new Uri("/StatisticsPage.xaml", UriKind.Relative));
        }

        private void AddNameButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NameDetailPage.xaml", UriKind.Relative));
            //PhoneApplicationPage_Loaded(this, new RoutedEventArgs());
        }


        private void llsFavourites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var favourite = llsFavourites.SelectedItem as Favourite;
            var favourite = llsFavourites.SelectedItem as All;
            NavigationService.Navigate(new Uri("/NameDetailPage.xaml#" + favourite.AllId, UriKind.Relative));
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

        private void llsRandomWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var randomWord = llsRandomWords.SelectedItem as All;
            NavigationService.Navigate(new Uri("/NameDetailPage.xaml#" + randomWord.AllId, UriKind.Relative));
        }

    }
}