using BowlingLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace BowlingScorer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static List<Player> PlayerHistory = new List<Player>();
        public static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
            GrabPlayerHistory();
            
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand command = new SettingsCommand("about", "About This App", (handler) =>
            {
                Popup popup = BuildSettingsItem(new AboutPage(), 343);
                popup.DataContext = this;
                popup.IsOpen = true;
            });

            SettingsCommand command2 = new SettingsCommand("reset", "Reset Bowling Data", (handler) =>
            {
                Popup popup = BuildSettingsItem(new ResetAllData(), 343);
                popup.IsOpen = true;
            });
 
            args.Request.ApplicationCommands.Add(command);
            args.Request.ApplicationCommands.Add(command2);

        }

        private Popup BuildSettingsItem(UserControl page, int width)
        {
            Popup p = new Popup();
            p.IsLightDismissEnabled = true;
            p.ChildTransitions = new TransitionCollection();
            p.ChildTransitions.Add(new PaneThemeTransition()
            {
                Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ?
                        EdgeTransitionLocation.Right :
                        EdgeTransitionLocation.Left
            });

            page.Width = width;
            page.Height = Window.Current.Bounds.Height;
            p.Child = page;

            p.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (Window.Current.Bounds.Width - width) : 0);
            p.SetValue(Canvas.TopProperty, 0);

            return p;

        }

        private async void GrabPlayerHistory()
        {

            StorageFile localFile = await localFolder.CreateFileAsync("playerhistory.txt", CreationCollisionOption.OpenIfExists);
            string s = await FileIO.ReadTextAsync(localFile);
            if (s != "")
            {
                PlayerHistory = DeserializeFromString<List<Player>>(s.ToString());
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SavePlayerData();
            
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        public static async void SavePlayerData()
        {
            string s = SerializeToString(PlayerHistory);
            StorageFile localFile = await localFolder.CreateFileAsync("playerhistory.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, s);
        }

        public static string SerializeToString(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public static T DeserializeFromString<T>(string xml)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                return (T)deserializer.Deserialize(reader);
            }
        }
    }
}
