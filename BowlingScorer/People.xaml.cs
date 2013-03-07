using BowlingLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
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
    public sealed partial class People : Page
    {
        Player p;
        
        public People()
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
            PopulatePeopleList();
        }

        private void PopulatePeopleList()
        {
            PeopleList.ItemsSource = null;
            PeopleList.ItemsSource = from p in App.PlayerHistory orderby p.GameHistory.Count descending select p;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        Grid OldGrid = new Grid();
        static Color Gray444 = Color.FromArgb(255, 85, 85, 85);
        SolidColorBrush Gray444Brush = new SolidColorBrush(Gray444);
        static Color Gray666 = Color.FromArgb(255, 102, 102, 102);
        SolidColorBrush Gray666Brush = new SolidColorBrush(Gray666);

        private void Player_Tapped(object sender, TappedRoutedEventArgs e)
        {
            OldGrid.Background = Gray666Brush;
            if (OldGrid.DataContext != null) ((Grid)((Grid)OldGrid.Children[0]).Children[0]).Visibility = Visibility.Collapsed;
            Grid g = sender as Grid;
            ((Grid)((Grid)g.Children[0]).Children[0]).Visibility = Visibility.Visible;
            g.Background = Gray444Brush;
            p = (Player)g.DataContext;
            ShowEditForm();
            OldGrid = g;
        }

        private void ShowEditForm()
        {
            HideAddBowlerButton();
            if (p.Name != null)
            {
                PlayerName.Text = p.Name;
                NameBox.Text = p.Name;
                NicknameBox.Text = p.Nickname;
                LoadGameList();
            }
            else
            {
                PlayerName.Text = "Add New Bowler";
                NameBox.Text = "";
                NicknameBox.Text = "";
            }
            BowlerNameErrorMessage.Visibility = Visibility.Collapsed;
            FormHeader.Visibility = Visibility.Visible;
            FormBody.Visibility = Visibility.Visible;
        }

        private void LoadGameList()
        {
            GameList.ItemsSource = from g in p.GameHistory orderby g.DateTime descending select g;
        }

        private void HideEditForm()
        {
            FormHeader.Visibility = Visibility.Collapsed;
            FormBody.Visibility = Visibility.Collapsed;
            ShowAddBowlerButton();
        }

        private void CloseForm_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HideEditForm();
            OldGrid.Background = Gray666Brush;
        }

        private void SavePlayerData_Click(object sender, RoutedEventArgs e)
        {
            if ((NameBox.Text != "") && (NicknameBox.Text != ""))
            {
                SavePlayerData();
            }
            else
            {
                BowlerNameErrorMessage.Visibility = Visibility.Visible;
            }
        }

        private void SavePlayerData()
        {
            App.PlayerHistory.Remove(p);
            p.Name = NameBox.Text;
            p.Nickname = NicknameBox.Text;
            App.PlayerHistory.Add(p);
            App.SavePlayerData();
            HideEditForm();
            PopulatePeopleList();
        }

        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            p = new Player();
            ShowEditForm();
        }

        private void ShowAddBowlerButton()
        {
            AddBowlerButton.Visibility = Visibility.Visible;
        }

        private void HideAddBowlerButton()
        {
            AddBowlerButton.Visibility = Visibility.Collapsed;
        }

        private void AddBowlerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            p = new Player();
            ShowEditForm();
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            BowlerNameErrorMessage.Visibility = Visibility.Collapsed;
        }

        private void NicknameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            BowlerNameErrorMessage.Visibility = Visibility.Collapsed;
        }

        private void RemoveScoreButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveScore();
        }

        private async void RemoveScore()
        {
            if (OldScore.DataContext != null)
            {
                BowlingLogic.Game g = (BowlingLogic.Game)OldScore.DataContext;
                
                MessageDialog m = new MessageDialog("Are you sure you want to delete the selected game?  This cannot be undone.", "Delete This Game From " + String.Format("{0:MMMM d, yyyy}", g.DateTime) + "?");
                m.Commands.Add(new UICommand("Yes"));
                m.Commands.Add(new UICommand("No"));
                m.DefaultCommandIndex = 1;

                var command = await m.ShowAsync();

                if (command.Label == "Yes")
                {
                    p.GameHistory.Remove(g);
                    LoadGameList();
                    PopulatePeopleList();
                    App.SavePlayerData();
                }
            }
        }

        Grid OldScore = new Grid();

        private void GameScore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Grid g = sender as Grid;
            OldScore.Background = Gray666Brush;
            g.Background = Gray444Brush;
            ShowRemoveGameButton();
            OldScore = g;
        }

        private void ShowRemoveGameButton()
        {
            //RemoveScoreButton.Visibility = Visibility.Visible;
        }

        private void HideRemoveGameButton()
        {
            //RemoveScoreButton.Visibility = Visibility.Collapsed;
        }

        private void RemovePlayerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RemovePlayer();
        }

        private async void RemovePlayer()
        {
            MessageDialog m;
            if (p.GameHistory.Count >= 2) m = new MessageDialog("Are you sure you want to delete " + p.Name + " and all " + p.GameHistory.Count + " of their bowled games?  This cannot be undone.", "Delete " + p.Name + "?");
            else m = new MessageDialog("Are you sure you want to delete " + p.Name + "?  This cannot be undone.", "Delete " + p.Name + "?");
            m.Commands.Add(new UICommand("Yes"));
            m.Commands.Add(new UICommand("No"));
            m.DefaultCommandIndex = 1;

            var command = await m.ShowAsync();

            if (command.Label == "Yes")
            {
                App.PlayerHistory.Remove(p);
                App.SavePlayerData();
                HideEditForm();
                PopulatePeopleList();
            }
        }
    }
}
