using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BowlingScorer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.SizeChanged += MainPage_SizeChanged;
        }

        void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double w = Windows.UI.Xaml.Window.Current.Bounds.Width;
            if (w == 320)
            {
                Frame.Navigate(typeof(SnappedView));
            }
            else ResizeBoard();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ResizeBoard();
            CircleAnimation.Begin();
        }

        private void ResizeBoard()
        {
            double w = Windows.UI.Xaml.Window.Current.Bounds.Width;
            double h = Windows.UI.Xaml.Window.Current.Bounds.Height;

            double remaining = w - h;

            if (remaining >= 0)
            {
                Left.Width = new GridLength(remaining / 2);
                Right.Width = new GridLength(remaining / 2);
                Center.Width = new GridLength(h);
            }
        }

        private void NewGameButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game));
        }

        private void ManagePeople_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(People));
        }

        private void StatisticsButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Statistics));
        }

        public void ShowAd()
        {
            AdBox.Visibility = Visibility.Visible;
        }

        public void HideAd()
        {
            AdBox.Visibility = Visibility.Collapsed;
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {
            ShowAd();
        }
    }
}
