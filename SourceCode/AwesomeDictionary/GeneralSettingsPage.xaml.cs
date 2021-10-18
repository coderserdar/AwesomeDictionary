using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using AwesomeDictionary.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace AwesomeDictionary
{
    public partial class GeneralSettingsPage : PhoneApplicationPage
    {

        public GeneralSettingsPage()
        {
            InitializeComponent();
            SetBackgroundColor();

            pvGeneralSettings.Title = AppResources.GeneralSettings;

            piLanguage.Header = AppResources.Language;
            piDictionaryInstall.Header = AppResources.DictionaryInstall;
            //piOtherSettings.Header = AppResources.OtherSettings;
            piBackground.Header = AppResources.Background;

            //lblOneDrive.Text = AppResources.OneDrive;

            btnLanguage.Content = AppResources.Select;
            btnBackgroundColor.Content = AppResources.Select;
            //btnOneDrive.Content = AppResources.Login;
            //btnOneDrive.SignInText = AppResources.SignIn;
            //btnOneDrive.SignOutText = AppResources.SignOut;
            txtInstalling.Text = AppResources.Installing;

            pbInstall.Visibility = Visibility.Collapsed;
            txtInstalling.Visibility = Visibility.Collapsed;
            txtInstalling.BorderBrush = this.LayoutRoot.Background;

            btnRemoveBackgroundImage.Content = AppResources.RemoveBackgroundImage;
            lblBackgroundImage.Text = AppResources.BackgroundImage;
            btnBackgroundImage.Content = AppResources.Select;
            btnResetSettings.Content = AppResources.ResetSettings;

            piFont.Header = AppResources.Font;
            btnFontFamily.Content = AppResources.Select;
            btnFontSize.Content = AppResources.Select;

            btnInstall.Content = AppResources.Install;
            btnUninstall.Content = AppResources.Uninstall;

            txtBuyukLugat.Text = AppResources.BuyukLugat + " (49331 " + AppResources.Word + ")";
            txtComputer.Text = AppResources.ComputerDictionary + " (3508 " + AppResources.Word + ")";
            txtGerman.Text = AppResources.GermanTurkish + " (17526 " + AppResources.Word + ")";
            txtOxford.Text = AppResources.OxfordDictionary + " (36369 " + AppResources.Word + ")";
            txtWordMeaning.Text = AppResources.WordMeaning + " (10535 " + AppResources.Word + ")";
            txtRisaleNur.Text = AppResources.RisaleNur + " (9478 " + AppResources.Word + ")";
            txtEnglishVol1.Text = AppResources.EnglishTurkishVol1 + " (127157 " + AppResources.Word + ")";
            txtEnglishVol2.Text = AppResources.EnglishTurkishVol2 + " (3699 " + AppResources.Word + ")";


            //cbSync.Content = AppResources.SyncOnOneFile;
            //cbSync.IsEnabled = false;

            SetBackgroundColor();

            using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            {
                var appSettings = context.AppSettings.First() as AppSettings;

                lblFontFamily.Text = AppResources.FontFamily + " (" + AppResources.Selected + ": " + appSettings.FontFamily + ")";
                lblFontSize.Text = AppResources.FontSize + " (" + AppResources.Selected + ": " + appSettings.FontSize + ")";

                if (appSettings.AppLangName == "EN")
                {
                    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.English + ")";
                }
                if (appSettings.AppLangName == "TR")
                {
                    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Turkish + ")";
                }
                if (appSettings.AppLangName == "DE")
                {
                    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.German + ")";
                }
                //if (appSettings.AppLangName == "ES")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Spanish + ")";
                //}

                //if (appSettings.AppLangName == "PT")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Portuguese + ")";
                //}
                //if (appSettings.AppLangName == "AR")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Arabic + ")";
                //}
                //if (appSettings.AppLangName == "FA")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Persian + ")";
                //}
                //if (appSettings.AppLangName == "IT")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Italian + ")";
                //}
                //if (appSettings.AppLangName == "FR")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.French + ")";
                //}
                //if (appSettings.AppLangName == "RU")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Russian + ")";
                //}
                //if (appSettings.AppLangName == "ZH")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Chinese + ")";
                //}
                //if (appSettings.AppLangName == "JA")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Japanese + ")";
                //}
                //if (appSettings.AppLangName == "SA")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Sanskrit + ")";
                //}
                //if (appSettings.AppLangName == "TH")
                //{
                //    lblLanguage.Text = AppResources.Language + " (" + AppResources.Selected + ": " + AppResources.Thai + ")";
                //}

                if (appSettings.AppBackgroundColor == "BLA")
                {
                    lblBackgroundColor.Text = AppResources.BackgroundColor + " (" + AppResources.Selected + ": " + AppResources.Black + ")";
                }
                if (appSettings.AppBackgroundColor == "BLU")
                {
                    lblBackgroundColor.Text = AppResources.BackgroundColor + " (" + AppResources.Selected + ": " + AppResources.Blue + ")";
                }
                if (appSettings.AppBackgroundColor == "BRO")
                {
                    lblBackgroundColor.Text = AppResources.BackgroundColor + " (" + AppResources.Selected + ": " + AppResources.Brown + ")";
                }
                if (appSettings.AppBackgroundColor == "RED")
                {
                    lblBackgroundColor.Text = AppResources.BackgroundColor + " (" + AppResources.Selected + ": " + AppResources.Red + ")";
                }
                if (appSettings.AppBackgroundColor == "GRE")
                {
                    lblBackgroundColor.Text = AppResources.BackgroundColor + " (" + AppResources.Selected + ": " + AppResources.Green + ")";
                }
                if (appSettings.AppBackgroundColor == "YEL")
                {
                    lblBackgroundColor.Text = AppResources.BackgroundColor + " (" + AppResources.Selected + ": " + AppResources.Yellow + ")";
                }
                if (appSettings.AppBackgroundColor == "GRA")
                {
                    lblBackgroundColor.Text = AppResources.BackgroundColor + " (" + AppResources.Selected + ": " + AppResources.Gray + ")";
                }
                if (appSettings.AppBackgroundColor == "ORA")
                {
                    lblBackgroundColor.Text = AppResources.BackgroundColor + " (" + AppResources.Selected + ": " + AppResources.Orange + ")";
                }
                if (appSettings.AppBackgroundColor == "PUR")
                {
                    lblBackgroundColor.Text = AppResources.BackgroundColor + " (" + AppResources.Selected + ": " + AppResources.Purple + ")";
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetBackgroundColor();
            //while (NavigationService.CanGoBack)
            //NavigationService.RemoveBackEntry();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            //while (NavigationService.CanGoBack)
            //NavigationService.RemoveBackEntry();

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

        private void btnLanguage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/LanguageSettingsPage.xaml", UriKind.Relative));
        }

        private void btnBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/BackgroundColorSettingsPage.xaml", UriKind.Relative));
        }

        private void btnBackgroundImage_Click(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask objPhotoChooser = new PhotoChooserTask();
            objPhotoChooser.Completed += new EventHandler<PhotoResult>(PhotoChooseCall);
            objPhotoChooser.Show();
        }

        private void PhotoChooseCall(object sender, PhotoResult e)
        {
            switch (e.TaskResult)
            {
                case TaskResult.OK:
                    using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
                    {
                        var appSettings = context.AppSettings;
                        foreach (var appSetting in appSettings)
                        {
                            appSetting.AppBackgroundImage = new byte[e.ChosenPhoto.Length];
                            e.ChosenPhoto.Position = 0;
                            e.ChosenPhoto.Read(appSetting.AppBackgroundImage, 0, appSetting.AppBackgroundImage.Length);
                            //noteFolder.NoteFolderPassword = "";
                        }
                        context.SubmitChanges();
                        MessageBox.Show(AppResources.BackgroundImageChangedSuccessfully);
                    }
                    break;
                case TaskResult.Cancel:
                    //MessageBox.Show("Cancelled");
                    break;
                case TaskResult.None:
                    //MessageBox.Show("Nothing Entered");
                    break;
            }
            SetBackgroundColor();
        }

        private void btnRemoveBackgroundImage_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            {
                var appSettings = context.AppSettings;
                foreach (var appSetting in appSettings)
                {
                    appSetting.AppBackgroundImage = null;
                }
                context.SubmitChanges();
                MessageBox.Show(AppResources.BackgroundImageRemovedSuccessfully);
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            //pvGeneralSettings.Title = AppResources.GeneralSettings;

            //piLanguage.Header = AppResources.Language;
            //piSync.Header = AppResources.Sync;
            //piOtherSettings.Header = AppResources.OtherSettings;
            //piBackground.Header = AppResources.Background;

            ////lblOneDrive.Text = AppResources.OneDrive;

            //btnCategoryOrder.Content = AppResources.Select;
            //btnCategoryOrderStyle.Content = AppResources.Select;
            //btnLanguage.Content = AppResources.Select;
            //btnBackgroundColor.Content = AppResources.Select;
            ////btnOneDrive.Content = AppResources.Login;
            ////btnOneDrive.SignInText = AppResources.SignIn;
            ////btnOneDrive.SignOutText = AppResources.SignOut;
            //btnOneDriveSync.Content = AppResources.Sync;
            //lblOneDrive.Text = AppResources.OneDrive;
            //txtInstalling.Text = AppResources.Synchronizing;

            //pbSync.Visibility = Visibility.Collapsed;
            //txtInstalling.Visibility = Visibility.Collapsed;
            //txtInstalling.BorderBrush = this.LayoutRoot.Background;

            //btnRemoveBackgroundImage.Content = AppResources.RemoveBackgroundImage;
            //lblBackgroundImage.Text = AppResources.BackgroundImage;
            //btnBackgroundImage.Content = AppResources.Select;
            //btnResetSettings.Content = AppResources.ResetSettings;

            //btnOneDriveSync.IsEnabled = false;
            //cbSync.Content = AppResources.SyncOnOneFile;
            //cbSync.IsEnabled = false;
            //btnOneDrive.Content = "Sign In";

            //SetBackgroundColor();
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        private void btnResetSettings_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            {
                var appSettings = context.AppSettings;
                foreach (var appSetting in appSettings)
                {
                    appSetting.AppBackgroundImage = null;
                    appSetting.AppBackgroundColor = "BLA";
                }
                context.SubmitChanges();
                this.LayoutRoot.Background = new SolidColorBrush(Colors.Black);
                MessageBox.Show(AppResources.BackgroundSettingsResetSuccessfully);
            }
        }

        private void btnFontSize_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/FontSizeSettingsPage.xaml", UriKind.Relative));
        }

        private void btnFontFamily_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/FontFamilySettingsPage.xaml", UriKind.Relative));
        }

        // burası düzenlenecek
        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (cbBuyukLugat.IsChecked == false && cbComputer.IsChecked == false && cbEnglishVol1.IsChecked == false && cbEnglishVol2.IsChecked == false && cbOxford.IsChecked == false && cbRisaleNur.IsChecked == false && cbWordMeaning.IsChecked == false && cbGerman.IsChecked == false)
                {
                    MessageBox.Show(AppResources.AtLeastOneDictionary);
                }
                else
                {
                    using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
                    {
                        SozlukYukle(context);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(AppResources.SystemFault);
            }
        }

        private void SozlukYukle2(AwesomeDictionaryDataContext context)
        {
            if (cbBuyukLugat.IsChecked == true)
            {
                if (context.BuyukLugats.ToList().Count > 0)
                {
                    var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.BuyukLugat)).ToList() as List<All>;
                    context.AllNames.DeleteAllOnSubmit(allList);
                    context.BuyukLugats.DeleteAllOnSubmit(context.BuyukLugats.ToList());
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var buyukLugat = ReadFile(@"Sources\BuyukLugatEsas.txt");
                var buyukLugatLines = buyukLugat.Split('\n').ToList() as List<string>;
                for (int i = 1; i < buyukLugatLines.Count; i++)
                {
                    var information = buyukLugatLines[i].Split('_').ToList() as List<string>;
                    BuyukLugat buyukLugatWord = new BuyukLugat();
                    buyukLugatWord.BuyukLugatName = information[0].ToUpper().TrimEnd().TrimStart();
                    buyukLugatWord.BuyukLugatMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    buyukLugatWord.BuyukLugatNameMeaning = buyukLugatWord.BuyukLugatName + " " + buyukLugatWord.BuyukLugatNameMeaning;
                    context.BuyukLugats.InsertOnSubmit(buyukLugatWord);
                }
                context.SubmitChanges();
                var buyukLugats = context.BuyukLugats.ToList() as List<BuyukLugat>;
                for (int i = 0; i < buyukLugats.Count; i++)
                {
                    All allName = new All();
                    allName.AllName = buyukLugats[i].BuyukLugatName;
                    allName.AllMeaning = buyukLugats[i].BuyukLugatMeaning;
                    allName.AllNameMeaning = buyukLugats[i].BuyukLugatName + " " + buyukLugats[i].BuyukLugatMeaning;
                    allName.AllSource = AppResources.BuyukLugat;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.BuyukLugat + ")";
                    context.AllNames.InsertOnSubmit(allName);
                }
                context.SubmitChanges();
            }

            if (cbGerman.IsChecked == true)
            {
                if (context.AlmancaTurkces.ToList().Count > 0)
                {
                    var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.GermanTurkish)).ToList() as List<All>;
                    context.AllNames.DeleteAllOnSubmit(allList);
                    context.AlmancaTurkces.DeleteAllOnSubmit(context.AlmancaTurkces.ToList());
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var almancaTurkce = ReadFile(@"Sources\AlmancaTurkceSozluk.txt");
                var almancaTurkceLines = almancaTurkce.Split('\n').ToList() as List<string>;
                for (int i = 1; i < almancaTurkceLines.Count; i++)
                {
                    var information = almancaTurkceLines[i].Split('_').ToList() as List<string>;
                    AlmancaTurkce almancaTurkceWord = new AlmancaTurkce();
                    almancaTurkceWord.AlmancaTurkceName = information[0].ToUpper().TrimEnd().TrimStart();
                    almancaTurkceWord.AlmancaTurkceMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    almancaTurkceWord.AlmancaTurkceNameMeaning = almancaTurkceWord.AlmancaTurkceName + " " + almancaTurkceWord.AlmancaTurkceNameMeaning;
                    context.AlmancaTurkces.InsertOnSubmit(almancaTurkceWord);
                }
                context.SubmitChanges();
                var almancaTurkces = context.AlmancaTurkces.ToList() as List<AlmancaTurkce>;
                for (int i = 0; i < almancaTurkces.Count; i++)
                {
                    All allName = new All();
                    allName.AllName = almancaTurkces[i].AlmancaTurkceName;
                    allName.AllMeaning = almancaTurkces[i].AlmancaTurkceMeaning;
                    allName.AllNameMeaning = almancaTurkces[i].AlmancaTurkceName + " " + almancaTurkces[i].AlmancaTurkceMeaning;
                    allName.AllSource = AppResources.GermanTurkish;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.GermanTurkish + ")";
                    context.AllNames.InsertOnSubmit(allName);
                }
                context.SubmitChanges();
            }

            if (cbComputer.IsChecked == true)
            {
                if (context.Bilisims.ToList().Count > 0)
                {
                    var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.ComputerDictionary)).ToList() as List<All>;
                    context.AllNames.DeleteAllOnSubmit(allList);
                    context.Bilisims.DeleteAllOnSubmit(context.Bilisims.ToList());
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var bilisimSozlugu = ReadFile(@"Sources\BilisimSozlugu.txt");
                var bilisimSozluguLines = bilisimSozlugu.Split('\n').ToList() as List<string>;
                for (int i = 1; i < bilisimSozluguLines.Count; i++)
                {
                    var information = bilisimSozluguLines[i].Split('_').ToList() as List<string>;
                    BilisimSozlugu bilisimSozluguWord = new BilisimSozlugu();
                    bilisimSozluguWord.BilisimSozluguName = information[0].ToUpper().TrimEnd().TrimStart();
                    bilisimSozluguWord.BilisimSozluguMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    bilisimSozluguWord.BilisimSozluguNameMeaning = bilisimSozluguWord.BilisimSozluguName + " " + bilisimSozluguWord.BilisimSozluguNameMeaning;
                    context.Bilisims.InsertOnSubmit(bilisimSozluguWord);
                }
                context.SubmitChanges();
                var bilisimSozlugus = context.Bilisims.ToList() as List<BilisimSozlugu>;
                for (int i = 0; i < bilisimSozlugus.Count; i++)
                {
                    All allName = new All();
                    allName.AllName = bilisimSozlugus[i].BilisimSozluguName;
                    allName.AllMeaning = bilisimSozlugus[i].BilisimSozluguMeaning;
                    allName.AllNameMeaning = bilisimSozlugus[i].BilisimSozluguName + " " + bilisimSozlugus[i].BilisimSozluguMeaning;
                    allName.AllSource = AppResources.ComputerDictionary;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.ComputerDictionary + ")";
                    context.AllNames.InsertOnSubmit(allName);
                }
                context.SubmitChanges();
            }

            if (cbOxford.IsChecked == true)
            {
                if (context.Oxfords.ToList().Count > 0)
                {
                    var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.OxfordDictionary)).ToList() as List<All>;
                    context.AllNames.DeleteAllOnSubmit(allList);
                    context.Oxfords.DeleteAllOnSubmit(context.Oxfords.ToList());
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var oxfordEnglishEnglish = ReadFile(@"Sources\OxfordEnglishTurkish.txt");
                var oxfordEnglishEnglishLines = oxfordEnglishEnglish.Split('\n').ToList() as List<string>;
                for (int i = 1; i < oxfordEnglishEnglishLines.Count; i++)
                {
                    var information = oxfordEnglishEnglishLines[i].Split('_').ToList() as List<string>;
                    OxfordEnglishEnglish oxfordEnglishEnglishWord = new OxfordEnglishEnglish();
                    oxfordEnglishEnglishWord.OxfordName = information[0].ToUpper().TrimEnd().TrimStart();
                    oxfordEnglishEnglishWord.OxfordMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    oxfordEnglishEnglishWord.OxfordNameMeaning = oxfordEnglishEnglishWord.OxfordName + " " + oxfordEnglishEnglishWord.OxfordMeaning;
                    context.Oxfords.InsertOnSubmit(oxfordEnglishEnglishWord);
                }
                context.SubmitChanges();
                var oxfordEnglishEnglishs = context.Oxfords.ToList() as List<OxfordEnglishEnglish>;
                for (int i = 0; i < oxfordEnglishEnglishs.Count; i++)
                {
                    All allName = new All();
                    allName.AllName = oxfordEnglishEnglishs[i].OxfordName;
                    allName.AllMeaning = oxfordEnglishEnglishs[i].OxfordMeaning;
                    allName.AllNameMeaning = oxfordEnglishEnglishs[i].OxfordName + " " + oxfordEnglishEnglishs[i].OxfordMeaning;
                    allName.AllSource = AppResources.OxfordDictionary;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.OxfordDictionary + ")";
                    context.AllNames.InsertOnSubmit(allName);
                }
                context.SubmitChanges();
            }

            if (cbRisaleNur.IsChecked == true)
            {
                if (context.RisaleNurs.ToList().Count > 0)
                {
                    var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.RisaleNur)).ToList() as List<All>;
                    context.AllNames.DeleteAllOnSubmit(allList);
                    context.RisaleNurs.DeleteAllOnSubmit(context.RisaleNurs.ToList());
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var risaleNur = ReadFile(@"Sources\RisaleNurSozlugu.txt");
                var risaleNurLines = risaleNur.Split('\n').ToList() as List<string>;
                for (int i = 1; i < risaleNurLines.Count; i++)
                {
                    var information = risaleNurLines[i].Split('_').ToList() as List<string>;
                    RisaleNur risaleNurWord = new RisaleNur();
                    risaleNurWord.RisaleNurName = information[0].ToUpper().TrimEnd().TrimStart();
                    risaleNurWord.RisaleNurMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    risaleNurWord.RisaleNurNameMeaning = risaleNurWord.RisaleNurName + " " + risaleNurWord.RisaleNurNameMeaning;
                    context.RisaleNurs.InsertOnSubmit(risaleNurWord);
                }
                context.SubmitChanges();
                var risaleNurs = context.RisaleNurs.ToList() as List<RisaleNur>;
                for (int i = 0; i < risaleNurs.Count; i++)
                {
                    All allName = new All();
                    allName.AllName = risaleNurs[i].RisaleNurName;
                    allName.AllMeaning = risaleNurs[i].RisaleNurMeaning;
                    allName.AllNameMeaning = risaleNurs[i].RisaleNurName + " " + risaleNurs[i].RisaleNurMeaning;
                    allName.AllSource = AppResources.RisaleNur;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.RisaleNur + ")";
                    context.AllNames.InsertOnSubmit(allName);

                }
                context.SubmitChanges();
            }

            if (cbEnglishVol1.IsChecked == true)
            {
                if (context.EnglishTurkishVol1s.ToList().Count > 0)
                {
                    var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.EnglishTurkishVol1)).ToList() as List<All>;
                    context.AllNames.DeleteAllOnSubmit(allList);
                    context.EnglishTurkishVol1s.DeleteAllOnSubmit(context.EnglishTurkishVol1s.ToList());
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var englishTurkishVol1 = ReadFile(@"Sources\IngilizceTurkceSozlukv1.txt");
                var englishTurkishVol1Lines = englishTurkishVol1.Split('\n').ToList() as List<string>;
                for (int i = 1; i < englishTurkishVol1Lines.Count; i++)
                {
                    var information = englishTurkishVol1Lines[i].Split('_').ToList() as List<string>;
                    EnglishTurkishVol1 englishTurkishVol1Word = new EnglishTurkishVol1();
                    englishTurkishVol1Word.EnglishVol1Name = information[0].ToUpper().TrimEnd().TrimStart();
                    englishTurkishVol1Word.EnglishVol1Meaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    englishTurkishVol1Word.EnglishVol1NameMeaning = englishTurkishVol1Word.EnglishVol1Name + " " + englishTurkishVol1Word.EnglishVol1NameMeaning;
                    context.EnglishTurkishVol1s.InsertOnSubmit(englishTurkishVol1Word);
                }
                context.SubmitChanges();
                var englishTurkishVol1s = context.EnglishTurkishVol1s.ToList() as List<EnglishTurkishVol1>;
                for (int i = 0; i < englishTurkishVol1s.Count; i++)
                {
                    All allName = new All();
                    allName.AllName = englishTurkishVol1s[i].EnglishVol1Name;
                    allName.AllMeaning = englishTurkishVol1s[i].EnglishVol1Meaning;
                    allName.AllNameMeaning = englishTurkishVol1s[i].EnglishVol1Name + " " + englishTurkishVol1s[i].EnglishVol1NameMeaning;
                    allName.AllSource = AppResources.EnglishTurkishVol1;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.EnglishTurkishVol1 + ")";
                    context.AllNames.InsertOnSubmit(allName);
                }
                context.SubmitChanges();
            }

            if (cbEnglishVol2.IsChecked == true)
            {
                if (context.EnglishTurkishVol2s.ToList().Count > 0)
                {
                    var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.EnglishTurkishVol2)).ToList() as List<All>;
                    context.AllNames.DeleteAllOnSubmit(allList);
                    context.EnglishTurkishVol2s.DeleteAllOnSubmit(context.EnglishTurkishVol2s.ToList());
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var englishTurkishVol2 = ReadFile(@"Sources\IngilizceTurkceSozlukv2.txt");
                var englishTurkishVol2Lines = englishTurkishVol2.Split('\n').ToList() as List<string>;
                for (int i = 1; i < englishTurkishVol2Lines.Count; i++)
                {
                    var information = englishTurkishVol2Lines[i].Split('_').ToList() as List<string>;
                    EnglishTurkishVol2 englishTurkishVol2Word = new EnglishTurkishVol2();
                    englishTurkishVol2Word.EnglishVol2Name = information[0].ToUpper().TrimEnd().TrimStart();
                    englishTurkishVol2Word.EnglishVol2Meaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    englishTurkishVol2Word.EnglishVol2NameMeaning = englishTurkishVol2Word.EnglishVol2Name + " " + englishTurkishVol2Word.EnglishVol2NameMeaning;
                    context.EnglishTurkishVol2s.InsertOnSubmit(englishTurkishVol2Word);
                }
                context.SubmitChanges();
                var englishTurkishVol2s = context.EnglishTurkishVol2s.ToList() as List<EnglishTurkishVol2>;
                for (int i = 0; i < englishTurkishVol2s.Count; i++)
                {
                    All allName = new All();
                    allName.AllName = englishTurkishVol2s[i].EnglishVol2Name;
                    allName.AllMeaning = englishTurkishVol2s[i].EnglishVol2Meaning;
                    allName.AllNameMeaning = englishTurkishVol2s[i].EnglishVol2Name + " " + englishTurkishVol2s[i].EnglishVol2NameMeaning;
                    allName.AllSource = AppResources.EnglishTurkishVol2;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.EnglishTurkishVol2 + ")";
                    context.AllNames.InsertOnSubmit(allName);
                }
                context.SubmitChanges();
            }

            if (cbWordMeaning.IsChecked == true)
            {
                if (context.Kelimes.ToList().Count > 0)
                {
                    var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.WordMeaning)).ToList() as List<All>;
                    context.AllNames.DeleteAllOnSubmit(allList);
                    context.Kelimes.DeleteAllOnSubmit(context.Kelimes.ToList());
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var kelimeAnlamlari = ReadFile(@"Sources\KelimeAnlamlari.txt");
                var kelimeAnlamlariLines = kelimeAnlamlari.Split('\n').ToList() as List<string>;
                for (int i = 1; i < kelimeAnlamlariLines.Count; i++)
                {
                    var information = kelimeAnlamlariLines[i].Split('_').ToList() as List<string>;
                    KelimeAnlamlari kelimeAnlamlariWord = new KelimeAnlamlari();
                    kelimeAnlamlariWord.KelimeAnlamlariName = information[0].ToUpper().TrimEnd().TrimStart();
                    kelimeAnlamlariWord.KelimeAnlamlariMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    kelimeAnlamlariWord.KelimeAnlamlariNameMeaning = kelimeAnlamlariWord.KelimeAnlamlariName + " " + kelimeAnlamlariWord.KelimeAnlamlariNameMeaning;
                    context.Kelimes.InsertOnSubmit(kelimeAnlamlariWord);
                }
                context.SubmitChanges();
                var kelimeAnlamlaris = context.Kelimes.ToList() as List<KelimeAnlamlari>;
                for (int i = 0; i < kelimeAnlamlaris.Count; i++)
                {
                    All allName = new All();
                    allName.AllName = kelimeAnlamlaris[i].KelimeAnlamlariName;
                    allName.AllMeaning = kelimeAnlamlaris[i].KelimeAnlamlariMeaning;
                    allName.AllNameMeaning = kelimeAnlamlaris[i].KelimeAnlamlariName + " " + kelimeAnlamlaris[i].KelimeAnlamlariNameMeaning;
                    allName.AllSource = AppResources.WordMeaning;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.WordMeaning + ")";
                    context.AllNames.InsertOnSubmit(allName);
                }
                context.SubmitChanges();
            }

            MessageBox.Show(AppResources.DictionariesInstalledSuccessfully);
        }

        private void SozlukYukle(AwesomeDictionaryDataContext context)
        {
            //pbInstall.Visibility = Visibility.Visible;

            if (cbBuyukLugat.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.BuyukLugat)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);    
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var buyukLugat = ReadFile(@"Sources\BuyukLugatEsas.txt");
                var buyukLugatLines = buyukLugat.Split('\n').ToList() as List<string>;
                string text = AppResources.Installing + " " + AppResources.BuyukLugat;
                //pbInstall.Value = 0;
                List<All> allNames = new List<All>();
                for (int i = 1; i < buyukLugatLines.Count; i++)
                {
                    //pbInstall.Value = i;
                    var information = buyukLugatLines[i].Split('_').ToList() as List<string>;
                    All allName = new All();
                    allName.AllName = information[0].ToUpper().TrimEnd().TrimStart();
                    allName.AllMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    allName.AllNameMeaning = allName.AllName + " " + allName.AllNameMeaning;
                    allName.AllSource = AppResources.BuyukLugat;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.BuyukLugat + ")";
                    allNames.Add(allName);
                    //txtInstalling.Text = text + " (" + i + "/" + buyukLugatLines.Count + ")";
                }
                context.AllNames.InsertAllOnSubmit(allNames);
            }

            if (cbGerman.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.GermanTurkish)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var buyukLugat = ReadFile(@"Sources\AlmancaTurkceSozluk.txt");
                var buyukLugatLines = buyukLugat.Split('\n').ToList() as List<string>;
                string text = AppResources.Installing + " " + AppResources.GermanTurkish;
                //pbInstall.Value = 0;
                List<All> allNames = new List<All>();
                for (int i = 1; i < buyukLugatLines.Count; i++)
                {
                    //pbInstall.Value = i;
                    var information = buyukLugatLines[i].Split('_').ToList() as List<string>;
                    All allName = new All();
                    allName.AllName = information[0].ToUpper().TrimEnd().TrimStart();
                    allName.AllMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    allName.AllNameMeaning = allName.AllName + " " + allName.AllNameMeaning;
                    allName.AllSource = AppResources.GermanTurkish;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.GermanTurkish + ")";
                    allNames.Add(allName);
                    //txtInstalling.Text = text + " (" + i + "/" + buyukLugatLines.Count + ")";
                }
                context.AllNames.InsertAllOnSubmit(allNames);
                
            }

            if (cbComputer.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.ComputerDictionary)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var buyukLugat = ReadFile(@"Sources\BilisimSozlugu.txt");
                var buyukLugatLines = buyukLugat.Split('\n').ToList() as List<string>;
                string text = AppResources.Installing + " " + AppResources.ComputerDictionary;
                //pbInstall.Value = 0;
                List<All> allNames = new List<All>();
                for (int i = 1; i < buyukLugatLines.Count; i++)
                {
                    //pbInstall.Value = i;
                    var information = buyukLugatLines[i].Split('_').ToList() as List<string>;
                    All allName = new All();
                    allName.AllName = information[0].ToUpper().TrimEnd().TrimStart();
                    allName.AllMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    allName.AllNameMeaning = allName.AllName + " " + allName.AllNameMeaning;
                    allName.AllSource = AppResources.ComputerDictionary;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.ComputerDictionary + ")";
                    allNames.Add(allName);
                    //txtInstalling.Text = text + " (" + i + "/" + buyukLugatLines.Count + ")";
                }
                context.AllNames.InsertAllOnSubmit(allNames);
            }

            if (cbOxford.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.OxfordDictionary)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var buyukLugat = ReadFile(@"Sources\OxfordEnglishEnglish.txt");
                var buyukLugatLines = buyukLugat.Split('\n').ToList() as List<string>;
                string text = AppResources.Installing + " " + AppResources.OxfordDictionary;
                //pbInstall.Value = 0;
                List<All> allNames = new List<All>();
                for (int i = 1; i < buyukLugatLines.Count; i++)
                {
                    //pbInstall.Value = i;
                    var information = buyukLugatLines[i].Split('_').ToList() as List<string>;
                    All allName = new All();
                    allName.AllName = information[0].ToUpper().TrimEnd().TrimStart();
                    allName.AllMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    allName.AllNameMeaning = allName.AllName + " " + allName.AllNameMeaning;
                    allName.AllSource = AppResources.OxfordDictionary;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.OxfordDictionary + ")";
                    allNames.Add(allName);
                    //txtInstalling.Text = text + " (" + i + "/" + buyukLugatLines.Count + ")";
                }
                context.AllNames.InsertAllOnSubmit(allNames);
            }

            if (cbRisaleNur.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.RisaleNur)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var buyukLugat = ReadFile(@"Sources\RisaleNurSozlugu.txt");
                var buyukLugatLines = buyukLugat.Split('\n').ToList() as List<string>;
                string text = AppResources.Installing + " " + AppResources.RisaleNur;
                //pbInstall.Value = 0;
                List<All> allNames = new List<All>();
                for (int i = 1; i < buyukLugatLines.Count; i++)
                {
                    //pbInstall.Value = i;
                    var information = buyukLugatLines[i].Split('_').ToList() as List<string>;
                    All allName = new All();
                    allName.AllName = information[0].ToUpper().TrimEnd().TrimStart();
                    allName.AllMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    allName.AllNameMeaning = allName.AllName + " " + allName.AllNameMeaning;
                    allName.AllSource = AppResources.RisaleNur;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.RisaleNur + ")";
                    allNames.Add(allName);
                    //txtInstalling.Text = text + " (" + i + "/" + buyukLugatLines.Count + ")";
                }
                context.AllNames.InsertAllOnSubmit(allNames);
            }

            if (cbEnglishVol1.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.EnglishTurkishVol1)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var buyukLugat = ReadFile(@"Sources\IngilizceTurkceSozlukv1.txt");
                var buyukLugatLines = buyukLugat.Split('\n').ToList() as List<string>;
                string text = AppResources.Installing + " " + AppResources.EnglishTurkishVol1;
                //pbInstall.Value = 0;
                List<All> allNames = new List<All>();
                for (int i = 1; i < buyukLugatLines.Count; i++)
                {
                    //pbInstall.Value = i;
                    var information = buyukLugatLines[i].Split('_').ToList() as List<string>;
                    All allName = new All();
                    allName.AllName = information[0].ToUpper().TrimEnd().TrimStart();
                    allName.AllMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    allName.AllNameMeaning = allName.AllName + " " + allName.AllNameMeaning;
                    allName.AllSource = AppResources.EnglishTurkishVol1;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.EnglishTurkishVol1 + ")";
                    allNames.Add(allName);
                    //txtInstalling.Text = text + " (" + i + "/" + buyukLugatLines.Count + ")";
                }
                context.AllNames.InsertAllOnSubmit(allNames);
            }

            if (cbEnglishVol2.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.EnglishTurkishVol2)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var buyukLugat = ReadFile(@"Sources\IngilizceTurkceSozlukv2.txt");
                var buyukLugatLines = buyukLugat.Split('\n').ToList() as List<string>;
                string text = AppResources.Installing + " " + AppResources.EnglishTurkishVol2;
                //pbInstall.Value = 0;
                List<All> allNames = new List<All>();
                for (int i = 1; i < buyukLugatLines.Count; i++)
                {
                    //pbInstall.Value = i;
                    var information = buyukLugatLines[i].Split('_').ToList() as List<string>;
                    All allName = new All();
                    allName.AllName = information[0].ToUpper().TrimEnd().TrimStart();
                    allName.AllMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    allName.AllNameMeaning = allName.AllName + " " + allName.AllNameMeaning;
                    allName.AllSource = AppResources.EnglishTurkishVol2;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.EnglishTurkishVol2 + ")";
                    allNames.Add(allName);
                    //txtInstalling.Text = text + " (" + i + "/" + buyukLugatLines.Count + ")";
                }
                context.AllNames.InsertAllOnSubmit(allNames);
            }

            if (cbWordMeaning.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.WordMeaning)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                //burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                var buyukLugat = ReadFile(@"Sources\KelimeAnlamlari.txt");
                var buyukLugatLines = buyukLugat.Split('\n').ToList() as List<string>;
                string text = AppResources.Installing + " " + AppResources.WordMeaning;
                //pbInstall.Value = 0;
                List<All> allNames = new List<All>();
                for (int i = 1; i < buyukLugatLines.Count; i++)
                {
                    //pbInstall.Value = i;
                    var information = buyukLugatLines[i].Split('_').ToList() as List<string>;
                    All allName = new All();
                    allName.AllName = information[0].ToUpper().TrimEnd().TrimStart();
                    allName.AllMeaning = information[1].TrimEnd().TrimStart() == null ? "" : information[1].TrimEnd().TrimStart();
                    allName.AllNameMeaning = allName.AllName + " " + allName.AllNameMeaning;
                    allName.AllSource = AppResources.WordMeaning;
                    allName.AllNameSource = allName.AllName + " (" + AppResources.WordMeaning + ")";
                    allNames.Add(allName);
                    //txtInstalling.Text = text + " (" + i + "/" + buyukLugatLines.Count + ")";
                }
                context.AllNames.InsertAllOnSubmit(allNames);
            }
            context.SubmitChanges();
            //pbInstall.Visibility = Visibility.Collapsed;

            MessageBox.Show(AppResources.DictionariesInstalledSuccessfully);
        }

        public string ReadFile(string filePath)
        {
            var ResrouceStream = Application.GetResourceStream(new Uri(filePath, UriKind.Relative));
            if (ResrouceStream != null)
            {
                Stream myFileStream = ResrouceStream.Stream;
                if (myFileStream.CanRead)
                {
                    StreamReader myStreamReader = new StreamReader(myFileStream);

                    return myStreamReader.ReadToEnd();
                }
            }
            return "";
        }

        private void btnUninstall_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (cbBuyukLugat.IsChecked == false && cbComputer.IsChecked == false && cbEnglishVol1.IsChecked == false && cbEnglishVol2.IsChecked == false && cbOxford.IsChecked == false && cbRisaleNur.IsChecked == false && cbWordMeaning.IsChecked == false && cbGerman.IsChecked == false)
                {
                    MessageBox.Show(AppResources.AtLeastOneDictionary);
                }
                else
                {
                    using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
                    {
                        SozlukKaldir(context);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(AppResources.SystemFault);
            }
        }

        private void SozlukKaldir(AwesomeDictionaryDataContext context)
        {
            //pbInstall.Visibility = Visibility.Visible;

            if (cbBuyukLugat.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.BuyukLugat)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                context.SubmitChanges();
            }

            if (cbGerman.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.GermanTurkish)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                context.SubmitChanges();
            }

            if (cbComputer.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.ComputerDictionary)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                context.SubmitChanges();
            }
            if (cbOxford.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.OxfordDictionary)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                context.SubmitChanges();
            }
            if (cbRisaleNur.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.RisaleNur)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                context.SubmitChanges();
            }
            if (cbEnglishVol1.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.EnglishTurkishVol1)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                context.SubmitChanges();
            }
            if (cbEnglishVol2.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.EnglishTurkishVol2)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                context.SubmitChanges();
            }
            if (cbWordMeaning.IsChecked == true)
            {
                var allList = context.AllNames.Where(j => j.AllSource.Equals(AppResources.WordMeaning)).ToList() as List<All>;
                if (allList != null)
                {
                    context.AllNames.DeleteAllOnSubmit(allList);
                }
                context.SubmitChanges();
            }
            //context.SubmitChanges();

            MessageBox.Show(AppResources.DictionariesUninstalledSuccessfully);
        }
    }
}