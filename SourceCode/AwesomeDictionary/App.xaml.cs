using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.Generic;
using Microsoft.Phone.Marketplace;
using AwesomeDictionary.Resources;

namespace AwesomeDictionary
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard XAML initialization
            InitializeComponent();

            // ayarlardan teması açık renk bile olsa
            // kapalı gibi çalışmasını sağlayacak bir nuget paketi yüklendi
            // bu sorunu gideriyor
            ThemeManager.ToDarkTheme();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
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

        private static void DilAyariOlustur(AwesomeDictionaryDataContext context)
        {
            var appSettings = new AppSettings()
            {
                //AppLangId = 42,
                AppLangName = "EN",
                AppBackgroundColor = "BLA",
                FontFamily = "Verdana",
                FontSize = "30",
                AppBackgroundImage = null
            };

            context.AppSettings.InsertOnSubmit(appSettings);
            context.SubmitChanges();

            CultureInfo newCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            using (var context = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
            {
                if (!context.DatabaseExists())
                {
                    context.CreateDatabase();
                    DilAyariOlustur(context);

                    // burada metin belgeleri okunuyor ve veritabanındaki tablolara ekleme yapılıyor
                    //var male = ReadFile(@"Sources\ErkekAdlari.txt");
                    //var maleLines = male.Split('\n').ToList() as List<string>;
                    //for (int i = 1; i < maleLines.Count; i++)
                    //{
                    //    var information = maleLines[i].Split('_').ToList() as List<string>;
                    //    Male maleName = new Male();
                    //    maleName.MaleName = information[0].ToUpper().TrimEnd().TrimStart();
                    //    maleName.MaleMeaning = information[1].TrimEnd().TrimStart();

                    //    context.MaleNames.InsertOnSubmit(maleName);
                    //}

                    //var female = ReadFile(@"Sources\KizAdlari.txt");
                    //var femaleLines = female.Split('\n').ToList() as List<string>;
                    //for (int i = 1; i < femaleLines.Count; i++)
                    //{
                    //    var information = femaleLines[i].Split('_').ToList() as List<string>;
                    //    Female femaleName = new Female();
                    //    femaleName.FemaleName = information[0].ToUpper().TrimEnd().TrimStart();
                    //    femaleName.FemaleMeaning = information[1].TrimEnd().TrimStart();

                    //    context.FemaleNames.InsertOnSubmit(femaleName);
                    //}

                    //context.SubmitChanges();

                    //var maleNamesTemp = context.MaleNames.ToList() as List<Male>;
                    //var femaleNamesTemp = context.FemaleNames.ToList() as List<Female>;

                    //for (int i = 0; i < maleNamesTemp.Count; i++)
                    //{
                    //    for (int k = 0; k < femaleNamesTemp.Count; k++)
                    //    {
                    //        if (maleNamesTemp[i].MaleName == femaleNamesTemp[k].FemaleName)
                    //        {
                    //            Unisex unisex = new Unisex();
                    //            unisex.UnisexName = maleNamesTemp[i].MaleName;
                    //            unisex.UnisexMeaning = maleNamesTemp[i].MaleMeaning;
                    //            context.UnisexNames.InsertOnSubmit(unisex);
                    //            var maleNameTemp = context.MaleNames.Where(j => j.MaleId.Equals(maleNamesTemp[i].MaleId)).SingleOrDefault() as Male;
                    //            context.MaleNames.DeleteOnSubmit(maleNameTemp);
                    //            var femaleNameTemp = context.FemaleNames.Where(j => j.FemaleId.Equals(femaleNamesTemp[k].FemaleId)).SingleOrDefault() as Female;
                    //            context.FemaleNames.DeleteOnSubmit(femaleNameTemp);
                    //            break;
                    //        }
                    //    }
                    //}

                    //context.SubmitChanges();

                    //var males = context.MaleNames.ToList() as List<Male>;
                    //for (int i = 0; i < males.Count; i++)
                    //{
                    //    All allName = new All();
                    //    allName.AllName = males[i].MaleName;
                    //    allName.AllMeaning = males[i].MaleMeaning;
                    //    allName.AllNameMeaning = males[i].MaleName + " " + males[i].MaleMeaning;
                    //    allName.AllGender = "Erkek";

                    //    context.AllNames.InsertOnSubmit(allName);
                    //}

                    //var females = context.FemaleNames.ToList() as List<Female>;
                    //for (int i = 0; i < females.Count; i++)
                    //{
                    //    All allName = new All();
                    //    allName.AllName = females[i].FemaleName;
                    //    allName.AllMeaning = females[i].FemaleMeaning;
                    //    allName.AllNameMeaning = females[i].FemaleName + " " + females[i].FemaleMeaning;
                    //    allName.AllGender = "Kadın";

                    //    context.AllNames.InsertOnSubmit(allName);
                    //}

                    //var unisexes = context.UnisexNames.ToList() as List<Unisex>;
                    //for (int i = 0; i < unisexes.Count; i++)
                    //{
                    //    All allName = new All();
                    //    allName.AllName = unisexes[i].UnisexName;
                    //    allName.AllMeaning = unisexes[i].UnisexMeaning;
                    //    allName.AllNameMeaning = unisexes[i].UnisexName + " " + unisexes[i].UnisexMeaning;
                    //    allName.AllGender = "Erkek-Kadın";

                    //    context.AllNames.InsertOnSubmit(allName);
                    //}

                    //context.SubmitChanges();
                }
                else
                {
                    using (var context2 = new AwesomeDictionaryDataContext(AwesomeDictionaryDataContext.ConnectionString))
                    {

                        AppSettings lang =
                            context2.AppSettings.First() as AppSettings;
                        string culture = "";
                        switch (lang.AppLangName)
                        {
                            case "TR":
                                culture = "tr";
                                break;
                            case "EN":
                                culture = "en";
                                break;
                            case "DE":
                                culture = "de";
                                break;
                            case "ES":
                                culture = "es";
                                break;
                            case "FR":
                                culture = "fr";
                                break;
                            case "IT":
                                culture = "it";
                                break;
                            case "AR":
                                culture = "ar";
                                break;
                            case "FA":
                                culture = "fa-IR";
                                break;
                            case "ZH":
                                culture = "zh";
                                break;
                            case "PT":
                                culture = "pt";
                                break;
                            case "RU":
                                culture = "ru";
                                break;
                            case "JA":
                                culture = "ja";
                                break;
                            case "SA":
                                culture = "sa";
                                break;
                            case "TH":
                                culture = "th";
                                break;
                            default:
                                culture = "tr-TR";
                                break;
                        }
                        CultureInfo newCulture = new CultureInfo(culture);
                        Thread.CurrentThread.CurrentCulture = newCulture;
                        Thread.CurrentThread.CurrentUICulture = newCulture;
                    }
                }

                // kullanıcının programla ilgili bilgilendirici notları kendi dilinde görebilmesi için burada ekliyoruz.
            }
        }
    }
}