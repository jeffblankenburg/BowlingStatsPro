using BowlingLogic;
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
    public sealed partial class Statistics : Page
    {
        int SortType = 0;
        bool sortDirection = true;
        
        public Statistics()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            for (int i = 0; i < App.PlayerHistory.Count; i++)
            {
                App.PlayerHistory[i].UpdateStatistics();
            }
            LoadPlayers();
        }

        private void LoadPlayers()
        {
            switch (SortType)
            {
                case 0:
                    StatsList.ItemsSource = from p in App.PlayerHistory orderby p.Name ascending select p;
                    break;
                case 1:
                    StatsList.ItemsSource = from p in App.PlayerHistory orderby p.Average descending select p;
                    break;
                case 2:
                    StatsList.ItemsSource = from p in App.PlayerHistory orderby p.StrikeTotal descending select p;
                    break;
                case 3:
                    StatsList.ItemsSource = from p in App.PlayerHistory orderby p.SpareTotal descending select p;
                    break;
                case 4:
                    StatsList.ItemsSource = from p in App.PlayerHistory orderby p.GutterballTotal ascending select p;
                    break;
                case 5:
                    StatsList.ItemsSource = from p in App.PlayerHistory orderby p.StrikePercentage descending select p;
                    break;
                case 6:
                    StatsList.ItemsSource = from p in App.PlayerHistory orderby p.SparePercentage descending select p;
                    break;
                case 7:
                    StatsList.ItemsSource = from p in App.PlayerHistory orderby p.TotalPins descending select p;
                    break;
            }
            
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Average_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SortType = 1;
            LoadPlayers();
        }

        private void TotalPins_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SortType = 7;
            LoadPlayers();
        }

        private void SparePercentage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SortType = 6;
            LoadPlayers();
        }

        private void StrikePercentage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SortType = 5;
            LoadPlayers();
        }

        private void Gutterballs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SortType = 4;
            LoadPlayers();
        }

        private void Spares_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SortType = 3;
            LoadPlayers();
        }

        private void Strikes_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SortType = 2;
            LoadPlayers();
        }

        private void Name_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SortType = 0;
            LoadPlayers();
        }
    }
}
