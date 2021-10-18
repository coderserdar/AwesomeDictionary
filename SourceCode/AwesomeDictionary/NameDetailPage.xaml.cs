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
    public partial class NameDetailPage : PhoneApplicationPage
    {
        public string pageName;
        public int wordId;
        double InputHeight = 0.0;
        public bool flag;
        public bool isFilled;
        public double ratingValue = 0;
        string gender;
        All allName = new All();

        public NameDetailPage()
        {
            InitializeComponent();

            ApplicationBar = new ApplicationBar();

            //ApplicationBarIconButton button1 = new ApplicationBarIconButton();
            //button1.IconUri = new Uri("/Assets/Save.png", UriKind.Relative);
            //button1.Text = "Kaydet";
            //ApplicationBar.Buttons.Add(button1);
            //button1.Click += new EventHandler(SaveButton_Click);

            ApplicationBarIconButton button2 = new ApplicationBarIconButton();
            button2.IconUri = new Uri("/Assets/SendWithMail.png", UriKind.Relative);
            button2.Text = AppResources.SendWithEmail;
            ApplicationBar.Buttons.Add(button2);
            button2.Click += new EventHandler(SendMailButton_Click);

            ApplicationBarIconButton button3 = new ApplicationBarIconButton();
            button3.IconUri = new Uri("/Assets/SendWithSMS.png", UriKind.Relative);
            button3.Text = AppResources.SendWithSMS;
            ApplicationBar.Buttons.Add(button3);
            button3.Click += new EventHandler(SendSMSButton_Click);

            ApplicationBarIconButton button4 = new ApplicationBarIconButton();
            button4.IconUri = new Uri("/Assets/Share.png", UriKind.Relative);
            button4.Text = AppResources.Share;
            ApplicationBar.Buttons.Add(button4);
            button4.Click += new EventHandler(ShareNameButton_Click);

            isFilled = false;

            //ApplicationBarMenuItem menuItem1 = new ApplicationBarMenuItem();
            //menuItem1.Text = "Sil";
            //ApplicationBar.MenuItems.Add(menuItem1);
            //menuItem1.Click += new EventHandler(DeleteNameMenuItem_Click);

            //List<string> genderList = new List<string>();
            //genderList.Add("Lütfen seçiniz");
            //genderList.Add("Erkek");
            //genderList.Add("Kadın");
            //genderList.Add("Erkek-Kadın");
            //lpGender.ItemsSource = genderList;
            //lpGender.SelectedIndex = 0;

            SetBackgroundColor();
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (pageName.Contains("/SearchPage.xaml"))
            {
                //this.NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.Relative));
            }
            else
            {
                this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var lastPage = NavigationService.BackStack.FirstOrDefault();
            pageName = lastPage.Source.ToString();
            using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            {
                var appSettings = context.AppSettings.First();

                FontFamily temp = new FontFamily(appSettings.FontFamily);
                double fontsize = double.Parse(appSettings.FontSize);
                txtMeaning.FontFamily = temp;
                txtMeaning.FontSize = fontsize;
            }

            txtMeaning.IsEnabled = false;
            isFilled = false;
            //SetBackgroundColor();
            //while (NavigationService.CanGoBack)
            //NavigationService.RemoveBackEntry();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            //while (NavigationService.CanGoBack)
            //NavigationService.RemoveBackEntry();

        }

        protected override void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            // displays "Fragment: Detail"
            //MessageBox.Show("Folder Id: " + e.Fragment);
            base.OnFragmentNavigation(e);
            //string fragmentName = e.Fragment.ToString();
            wordId = Convert.ToInt32(e.Fragment);
            using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            {
                var name = context.AllNames.Where(j => j.AllId.Equals(wordId)).SingleOrDefault() as All;
                allName = name;
                //for (int i = 0; i < lpGender.Items.Count; i++)
                //{
                //    if (lpGender.Items[i].ToString() == name.AllGender)
                //    {
                //        lpGender.SelectedIndex = i;
                //        break;
                //    }
                //}
                lblNameDetail.Text = name.AllName;
                txtMeaning.Text = name.AllMeaning + Environment.NewLine + Environment.NewLine + AppResources.Source + ":" + name.AllSource + "";
                txtMeaning.IsEnabled = false;
                //txtMeaning.Text = name.AllMeaning;

                var favourite = context.Favourites.Where(j => j.FavouriteAllId.Equals(wordId)).SingleOrDefault() as Favourite;
                if (favourite != null)
                {
                    ApplicationBarMenuItem menuItem4 = new ApplicationBarMenuItem();
                    menuItem4.Text = AppResources.RemoveFromFavourites;
                    ApplicationBar.MenuItems.Add(menuItem4);
                    menuItem4.Click += new EventHandler(RemoveFavouritesMenuItem_Click);
                }
                else
                {
                    ApplicationBarMenuItem menuItem3 = new ApplicationBarMenuItem();
                    menuItem3.Text = AppResources.AddToFavourites;
                    ApplicationBar.MenuItems.Add(menuItem3);
                    menuItem3.Click += new EventHandler(AddFavouritesMenuItem_Click);
                }

                //var myUpdate = context.MyUpdates.Where(j => j.MyUpdateName.Equals(fragmentName)).SingleOrDefault() as MyUpdate; ;
                //if (myUpdate != null)
                //{
                //    ApplicationBarMenuItem menuItem2 = new ApplicationBarMenuItem();
                //    menuItem2.Text = "Sisteme Eklenmesi İçin Gönder";
                //    ApplicationBar.MenuItems.Add(menuItem2);
                //    menuItem2.Click += new EventHandler(SaveAndSendMenuItem_Click);

                //}


                isFilled = true;
                //pvName.SelectedIndex = 0;
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

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    pvName.SelectedIndex = 2;
            //    txtMeaning.Focus();
            //}
        }

        private void txtMeaning_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                double CurrentInputHeight = txtMeaning.ActualHeight;

                if (CurrentInputHeight > InputHeight)
                {
                    svMeaning.ScrollToVerticalOffset(svMeaning.VerticalOffset + CurrentInputHeight - InputHeight);
                }

                InputHeight = CurrentInputHeight;
            });
        }

        private void txtMeaning_GotFocus(object sender, RoutedEventArgs e)
        {
            App.RootFrame.RenderTransform = new CompositeTransform();
            flag = true;
        }

        private void txtMeaning_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            txtMeaning.Focus();
            //txtMeaning.Select(txtMeaning.Text.Length, 1);
            svMeaning.ScrollToVerticalOffset(e.GetPosition(txtMeaning).Y - 80);
        }

        private void txtMeaning_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!flag) return;
            txtMeaning.Focus();
            flag = false;
            this.pnlKeyboardPlaceHolder.Visibility = Visibility.Collapsed;
        }

        private void txtMeaning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                svMeaning.ScrollToVerticalOffset(txtMeaning.ActualHeight);
            }
        }

        private void svMeaning_GotFocus(object sender, RoutedEventArgs e)
        {
            this.svMeaning.ScrollToVerticalOffset(this.txtMeaning.ActualHeight);
            this.svMeaning.UpdateLayout();
        }

        private void SendSMSButton_Click(object sender, EventArgs e)
        {
            SmsComposeTask smsComposeTask = new SmsComposeTask();

            smsComposeTask.To = "";
            smsComposeTask.Body = CreateSendMaterial();

            smsComposeTask.Show();
            //MessageBox.Show(AppResources.SuccessfulSendWithSMS);
        }

        private void ShareNameButton_Click(object sender, EventArgs e)
        {
            ShareStatusTask shareStatusTask = new ShareStatusTask();

            shareStatusTask.Status = CreateSendMaterial();

            shareStatusTask.Show();
            //MessageBox.Show(AppResources.SuccessfulSendWithSMS);
        }

        private void SendMailButton_Click(object sender, EventArgs e)
        {
            // burada birden fazla e-posta hesabı varsa birini seçmesi söyleniyor
            //EmailAddressChooserTask emailAddressChooserTask;
            //emailAddressChooserTask = new EmailAddressChooserTask();
            //emailAddressChooserTask.Completed += new EventHandler<EmailResult>(emailAddressChooserTask_Completed);
            //emailAddressChooserTask.Show();

            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = AppResources.WordAndMeaning + " (" + lblNameDetail.Text + ")";
            emailComposeTask.Body = CreateSendMaterial();
            emailComposeTask.To = "";
            emailComposeTask.Cc = "";
            emailComposeTask.Bcc = "";

            emailComposeTask.Show();
            //MessageBox.Show(AppResources.SuccessfulSendWithMail);
        }

        private string CreateSendMaterial()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(AppResources.Word + ": " + lblNameDetail.Text);
            sb.AppendLine(AppResources.Meaning + ": " + txtMeaning.Text);
            //sb.AppendLine("Cinsiyeti: " + lpGender.Items[lpGender.SelectedIndex]);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine(AppResources.SendWithAwesomeDictionaryApp);
            return sb.ToString();
        }

        private void AddFavouritesMenuItem_Click(object sender, EventArgs e)
        {
            using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            {
                var favourites = context.Favourites.Where(j => j.FavouriteName.Equals(lblNameDetail.Text)).ToList() as List<Favourite>;
                if (favourites.Count > 0)
                {
                    MessageBox.Show(AppResources.WordAlreadyFavourite);
                }
                else
                {
                    Favourite favourite = new Favourite();
                    favourite.FavouriteName = lblNameDetail.Text;
                    favourite.FavouriteAllId = wordId;
                    context.Favourites.InsertOnSubmit(favourite);
                    context.SubmitChanges();
                    MessageBox.Show(AppResources.WordAddedFavouriteSuccessfully);
                }
            }
        }

        private void RemoveFavouritesMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(AppResources.RemoveFromFavouriteQuestion,
                   AppResources.RemoveFromFavourite, MessageBoxButton.OKCancel)
                   != MessageBoxResult.OK)
            {
            }
            else
            {
                using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
                {
                    var favourite = context.Favourites.Where(j => j.FavouriteName.Equals(lblNameDetail.Text)).ToList() as List<Favourite>;
                    context.Favourites.DeleteAllOnSubmit(favourite);
                    context.SubmitChanges();
                    MessageBox.Show(AppResources.WordRemovedFavouriteSuccessfully);
                }
            }
        }

        private void SaveAndSendMenuItem_Click(object sender, EventArgs e)
        {
            //EmailComposeTask emailComposeTask = new EmailComposeTask();

            //emailComposeTask.Subject = "İsim ve Anlamı (" + txtName.Text + ")";
            //emailComposeTask.Body = CreateSendMaterial();
            //emailComposeTask.To = "coderserdar@outlook.com";
            //emailComposeTask.Cc = "";
            //emailComposeTask.Bcc = "";

            //emailComposeTask.Show();
        }

        private void DeleteNameMenuItem_Click(object sender, EventArgs e)
        {
            //if (isFilled == true)
            //{
            //    if (MessageBox.Show("İsmi Silmek İstediğinize Emin Misiniz?",
            //           "İsmi Sil", MessageBoxButton.OKCancel)
            //           != MessageBoxResult.OK)
            //    {
            //    }
            //    else
            //    {
            //        using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            //        {

            //            switch (allName.AllGender)
            //            {
            //                case "Erkek":
            //                    var maleName = context.MaleNames.Where(j => j.MaleName.Equals(allName.AllName)).SingleOrDefault() as Male;
            //                    context.MaleNames.DeleteOnSubmit(maleName);
            //                    break;
            //                case "Kadın":
            //                    var femaleName = context.FemaleNames.Where(j => j.FemaleName.Equals(allName.AllName)).SingleOrDefault() as Female;
            //                    context.FemaleNames.DeleteOnSubmit(femaleName);
            //                    break;
            //                case "Erkek-Kadın":
            //                    var unisexName = context.UnisexNames.Where(j => j.UnisexName.Equals(allName.AllName)).SingleOrDefault() as Unisex;
            //                    context.UnisexNames.DeleteOnSubmit(unisexName);
            //                    break;
            //                default:
            //                    break;
            //            }

            //            var allNames = context.AllNames.Where(j => j.AllName.Equals(allName.AllName)).SingleOrDefault() as All;
            //            context.AllNames.DeleteOnSubmit(allNames);

            //            var myUpdates = context.MyUpdates.Where(j => j.MyUpdateName.Equals(allName.AllName)).SingleOrDefault() as MyUpdate;
            //            if (myUpdates != null)
            //            {
            //                context.MyUpdates.DeleteOnSubmit(myUpdates);
            //            }

            //            var favourites = context.Favourites.Where(j => j.FavouriteName.Equals(allName.AllName)).SingleOrDefault() as Favourite;
            //            if (favourites != null)
            //            {
            //                context.Favourites.DeleteOnSubmit(favourites);
            //            }

            //            context.SubmitChanges();
            //            MessageBox.Show("İsim, Sistemden Silindi");
            //        }
            //    }
            //}
            //this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            //{
            //    if (isFilled == false)
            //    {
            //        if (lpGender.SelectedIndex < 1 || txtName.Text.TrimEnd().TrimStart().Length < 1 || txtMeaning.Text.TrimEnd().TrimStart().Length < 1)
            //        {
            //            MessageBox.Show("Bütün Alanları Dolduranız Gerekmektedir.");
            //        }
            //        else
            //        {
            //            var allName = context.AllNames.Where(j => j.AllName.ToLower().Equals(txtName.Text.TrimEnd().TrimStart().ToLower())).SingleOrDefault() as All;
            //            if (allName != null)
            //            {
            //                MessageBox.Show("Bu İsim Sistemde Mevcut");
            //            }
            //            else
            //            {

            //                switch (lpGender.SelectedIndex)
            //                {
            //                    case 1:
            //                        Male maleName = new Male();
            //                        maleName.MaleName = txtName.Text.ToUpper();
            //                        maleName.MaleMeaning = txtMeaning.Text;
            //                        context.MaleNames.InsertOnSubmit(maleName);
            //                        break;
            //                    case 2:
            //                        Female femaleName = new Female();
            //                        femaleName.FemaleName = txtName.Text.ToUpper();
            //                        femaleName.FemaleMeaning = txtMeaning.Text;
            //                        context.FemaleNames.InsertOnSubmit(femaleName);
            //                        break;
            //                    case 3:
            //                        Unisex unisexName = new Unisex();
            //                        unisexName.UnisexName = txtName.Text.ToUpper();
            //                        unisexName.UnisexMeaning = txtMeaning.Text;
            //                        context.UnisexNames.InsertOnSubmit(unisexName);
            //                        break;
            //                    default:
            //                        break;
            //                }

            //                All allNameTemp = new All();
            //                allNameTemp.AllName = txtName.Text.ToUpper();
            //                allNameTemp.AllMeaning = txtMeaning.Text;
            //                allNameTemp.AllNameMeaning = txtName.Text + " " + txtMeaning.Text;
            //                allNameTemp.AllGender = lpGender.Items[lpGender.SelectedIndex].ToString();
            //                context.AllNames.InsertOnSubmit(allNameTemp);

            //                MyUpdate myUpdate = new MyUpdate();
            //                myUpdate.MyUpdateName = txtName.Text.ToUpper();
            //                context.MyUpdates.InsertOnSubmit(myUpdate);

            //                context.SubmitChanges();
            //                MessageBox.Show("İsim, Sisteme Başarılı Bir Şekilde Eklendi");

            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (allName.AllGender == lpGender.Items[lpGender.SelectedIndex].ToString() && allName.AllMeaning == txtMeaning.Text.TrimEnd().TrimStart())
            //        {
            //            MessageBox.Show("Herhangi bir değişiklik yapmadınız.");
            //        }
            //        else
            //        {
            //            // önce eski kayıt siliniyor
            //            switch (allName.AllGender)
            //            {
            //                case "Erkek":
            //                    var maleName = context.MaleNames.Where(j => j.MaleName.Equals(allName.AllName)).SingleOrDefault() as Male;
            //                    context.MaleNames.DeleteOnSubmit(maleName);
            //                    break;
            //                case "Kadın":
            //                    var femaleName = context.FemaleNames.Where(j => j.FemaleName.Equals(allName.AllName)).SingleOrDefault() as Female;
            //                    context.FemaleNames.DeleteOnSubmit(femaleName);
            //                    break;
            //                case "Erkek-Kadın":
            //                    var unisexName = context.UnisexNames.Where(j => j.UnisexName.Equals(allName.AllName)).SingleOrDefault() as Unisex;
            //                    context.UnisexNames.DeleteOnSubmit(unisexName);
            //                    break;
            //                default:
            //                    break;
            //            }

            //            // sonrasında yeni kayıt ekleniyor
            //            switch (lpGender.SelectedIndex)
            //            {
            //                case 1:
            //                    Male maleName = new Male();
            //                    maleName.MaleName = txtName.Text.ToUpper();
            //                    maleName.MaleMeaning = txtMeaning.Text;
            //                    context.MaleNames.InsertOnSubmit(maleName);
            //                    break;
            //                case 2:
            //                    Female femaleName = new Female();
            //                    femaleName.FemaleName = txtName.Text.ToUpper();
            //                    femaleName.FemaleMeaning = txtMeaning.Text;
            //                    context.FemaleNames.InsertOnSubmit(femaleName);
            //                    break;
            //                case 3:
            //                    Unisex unisexName = new Unisex();
            //                    unisexName.UnisexName = txtName.Text.ToUpper();
            //                    unisexName.UnisexMeaning = txtMeaning.Text;
            //                    context.UnisexNames.InsertOnSubmit(unisexName);
            //                    break;
            //                default:
            //                    break;
            //            }

            //            var all = context.AllNames.Where(j => j.AllName.Equals(allName.AllName)).Select(j => j);
            //            foreach (var item in all)
            //            {
            //                item.AllGender = lpGender.Items[lpGender.SelectedIndex].ToString();
            //                item.AllMeaning = txtMeaning.Text.TrimEnd().TrimStart();
            //                item.AllNameMeaning = item.AllName + " " + item.AllMeaning;
            //            }
            //            var myUpdateTemp = context.MyUpdates.Where(j => j.MyUpdateName.Equals(allName.AllName)).SingleOrDefault() as MyUpdate;
            //            if (myUpdateTemp == null)
            //            {
            //                MyUpdate myUpdate = new MyUpdate();
            //                myUpdate.MyUpdateName = allName.AllName;
            //                context.MyUpdates.InsertOnSubmit(myUpdate);
            //            }

            //            context.SubmitChanges();

            //            MessageBox.Show("İsim Bilgisi Başarılı Bir Şekilde Güncellendi");
            //        }
            //    }
            //    this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            //}
        }

        private void lpGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lpGender.SelectedIndex != 0)
            //{
            //    gender = lpGender.Items[lpGender.SelectedIndex].ToString();
            //    pvName.SelectedIndex = 1;
            //    txtName.Focus();
            //}
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}