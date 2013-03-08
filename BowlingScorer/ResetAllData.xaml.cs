using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BowlingScorer
{
    public sealed partial class ResetAllData : UserControl
    {
        public ResetAllData()
        {
            this.InitializeComponent();
        }

        private void ResetData_Click(object sender, RoutedEventArgs e)
        {
            ResetData();
        }

        private async void ResetData()
        {
            MessageDialog m = new MessageDialog("Are you sure you want to delete all of your data?  This cannot be undone.\n\nThis is your last chance to change your mind.", "Delete Everything and Reset This App?");
            m.Commands.Add(new UICommand("Yes"));
            m.Commands.Add(new UICommand("No"));
            m.DefaultCommandIndex = 1;

            var command = await m.ShowAsync();

            if (command.Label == "Yes")
            {
                App.PlayerHistory.Clear();
                App.SavePlayerData();
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MainPage));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Popup parent = this.Parent as Popup;
            if (parent != null)
            {
                parent.IsOpen = false;
            }

            // If the app is not snapped, then the back button shows the Settings pane again.
            if (Windows.UI.ViewManagement.ApplicationView.Value != Windows.UI.ViewManagement.ApplicationViewState.Snapped)
            {
                SettingsPane.Show();
            }
        }
    }
}
