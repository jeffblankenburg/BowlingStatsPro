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
using BowlingLogic;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using System.Globalization;
using Windows.UI.Popups;
using System.Runtime.Serialization;
using Windows.Storage;
using Windows.Data.Xml.Dom;

namespace BowlingScorer
{

    public sealed partial class Game : Page
    {
        List<Player> Players = new List<Player>();
        int CurrentPlayer = 0;
        int CurrentRoll = 1;
        bool IsGameEnded = true;
        
        public Game()
        {
            this.InitializeComponent();
            this.SizeChanged += Game_SizeChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ResizeBoard();
            BottomBar.IsOpen = true;
            SetGameDate();
            PopulatePlayerList();
        }

        private void PopulatePlayerList()
        {
            PlayerList.ItemsSource = GetPlayerList(); 
        }

        private List<Player> GetPlayerList()
        {
            IEnumerable<Player> WholeListEnumerable = from p in App.PlayerHistory orderby p.Name select p;
            List<Player> WholeList = new List<Player>();

            for (int i = 0; i < WholeListEnumerable.Count(); i++)
            {
                WholeList.Add((Player)WholeListEnumerable.ElementAt(i));
            }

            for (int i = 0; i < Players.Count; i++)
            {
                WholeList.Remove(Players[i]);
            }

            switch (WholeList.Count)
            {
                case 0:
                    PlayerScrollViewer.Visibility = Visibility.Collapsed;
                    PlayerScrollViewer.Margin = new Thickness(1, 0, 0, 35);
                    AddPlayerPopup.VerticalOffset = -190;
                    break;
                case 1:
                    PlayerScrollViewer.Visibility = Visibility.Visible;
                    PlayerScrollViewer.Margin = new Thickness(1, 0, 0, 20);
                    AddPlayerPopup.VerticalOffset = -277;
                    break;
                case 2:
                    PlayerScrollViewer.Visibility = Visibility.Visible;
                    PlayerScrollViewer.Margin = new Thickness(1, 0, 0, 35);
                    AddPlayerPopup.VerticalOffset = -327;
                    break;
                case 3:
                    PlayerScrollViewer.Visibility = Visibility.Visible;
                    PlayerScrollViewer.Margin = new Thickness(1, 0, 0, 35);
                    AddPlayerPopup.VerticalOffset = -377;
                    break;
                default:
                    PlayerScrollViewer.Visibility = Visibility.Visible;
                    PlayerScrollViewer.Margin = new Thickness(1, 0, 0, 35);
                    AddPlayerPopup.VerticalOffset = -427;
                    break;
            }

            return WholeList;
        }

        private void SetGameDate()
        {
            pageTitle.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy  h:mm tt").Replace("PM", "pm").Replace("AM", "am");
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void ResizeBoard()
        {
            double w = Windows.UI.Xaml.Window.Current.Bounds.Width;

            if (w <= 1024)
            {
                Grid.SetColumn(GameGridContainer, 0);
                GameGridContainer.Margin = new Thickness(40, 0, 0, 0);
            }
            else
            {
                Grid.SetColumn(GameGridContainer, 1);
                GameGridContainer.Margin = new Thickness(0, 0, 0, 0);
            }
        }

        void Game_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeBoard();
        }

        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePlayerPopupState();
        }

        private void ChangePlayerPopupState()
        {
            if (AddPlayerPopup.IsOpen)
            {
                AddPlayerPopup.IsOpen = false;
            }
            else
            {
                AddPlayerPopup.IsOpen = true;
                //AddNewPlayerText.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            }
        }

        private void AddNewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewPlayer();
        }

        private void AddNewPlayer()
        {
            if ((AddNewPlayerText.Text != String.Empty) && (AddNewPlayerText.Text != ""))
            {
                AddPlayerPopup.IsOpen = false;
                BottomBar.IsOpen = false;
                Player p = new Player(AddNewPlayerText.Text);
                Players.Add(p);
                AddNewPlayerCleanup();               
            }
            else
            {
                AddNewPlayerError.Visibility = Visibility.Visible;
                //AddNewPlayerText.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            }
        }

        private void AddNewPlayerCleanup()
        {
            HideResetGameButton();
            ShowStartGameButton();
            ShowDeleteGameButton();
            BuildGameBoard();
            AddNewPlayerText.Text = String.Empty; 
        }

        private void ShowDeleteGameButton()
        {
            DeleteGameButton.Visibility = Visibility.Visible;
            BottomBar.IsOpen = true;
        }

        private void HideDeleteGameButton()
        {
            DeleteGameButton.Visibility = Visibility.Collapsed;
        }

        private void BuildRandomGame(Player p)
        {
            p.Game.Roll(0, 1, 7);
            p.Game.Roll(0, 2, 2);
            p.Game.Roll(1, 1, 2);
            p.Game.Roll(1, 2, 8);
            p.Game.Roll(2, 1, 2);
            p.Game.Roll(2, 2, 8);
            p.Game.Roll(3, 1, 9);
            p.Game.Roll(3, 2, 0);
            p.Game.Roll(4, 1, 0);
        }

        private void BuildPerfectGame(Player p)
        {
            p.Game.Roll(0, 1, 10);
            p.Game.Roll(1, 1, 10);
            p.Game.Roll(2, 1, 10);
            p.Game.Roll(3, 1, 10);
            p.Game.Roll(4, 1, 10);
            p.Game.Roll(5, 1, 10);
            p.Game.Roll(6, 1, 10);
            p.Game.Roll(7, 1, 10);
            p.Game.Roll(8, 1, 10);
            p.Game.Roll(9, 1, 10);
            p.Game.Roll(9, 2, 10);
            p.Game.Roll(9, 3, 10);
        }

        private void AddNewPlayer_CheckForEnter(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                AddNewPlayer();
            }
            else
            {
                AddNewPlayerError.Visibility = Visibility.Collapsed;
            }
        }

        SolidColorBrush White = new SolidColorBrush(Colors.White);

        private void BuildGameBoard()
        {
            StackPanel s = new StackPanel();

            if (Players.Count > 0)
            {
                //IntroText.Visibility = Visibility.Collapsed;
                GameGridContainer.Visibility = Visibility.Visible;
                
                for (int i = 0; i < Players.Count; i++)
                {
                    StackPanel s1 = new StackPanel { Orientation = Orientation.Horizontal };
                    s1.DataContext = Players[i];
                    s.Children.Add(s1);
                    Grid g1 = new Grid { Width = 148, Height = 75 };
                    s1.Children.Add(g1);
                    TextBlock t1 = new TextBlock { Text = Players[i].Nickname, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 24 };
                    t1.Tapped += PlayerName_Tapped;
                    g1.Children.Add(t1);
                    Rectangle r1 = new Rectangle { Width = 1, Height = 75, Fill = White };
                    s1.Children.Add(r1);
                    for (int j = 0; j < 10; j++)
                    {
                        Grid GameFrame = new Grid { Width = 75, Height = 75 };
                        GameFrame.Tapped += GameFrame_Tapped;
                        ColumnDefinition c1 = new ColumnDefinition { Width = new GridLength(37) };
                        ColumnDefinition c2 = new ColumnDefinition { Width = new GridLength(1) };
                        ColumnDefinition c3 = new ColumnDefinition { Width = new GridLength(37) };
                        GameFrame.ColumnDefinitions.Add(c1);
                        GameFrame.ColumnDefinitions.Add(c2);
                        GameFrame.ColumnDefinitions.Add(c3);
                        if (j == 9)
                        {
                            GameFrame.Width = 112;
                            ColumnDefinition c4 = new ColumnDefinition { Width = new GridLength(1) };
                            ColumnDefinition c5 = new ColumnDefinition { Width = new GridLength(37) };
                            GameFrame.ColumnDefinitions.Add(c4);
                            GameFrame.ColumnDefinitions.Add(c5);
                        }
                        RowDefinition row1 = new RowDefinition { Height = new GridLength(37) };
                        RowDefinition row2 = new RowDefinition { Height = new GridLength(1) };
                        RowDefinition row3 = new RowDefinition { Height = new GridLength(37) };
                        GameFrame.RowDefinitions.Add(row1);
                        GameFrame.RowDefinitions.Add(row2);
                        GameFrame.RowDefinitions.Add(row3);
                        s1.Children.Add(GameFrame);
                        Rectangle r2 = new Rectangle {Fill = White };
                        Grid.SetColumn(r2, 1);
                        GameFrame.Children.Add(r2);
                        Rectangle r3 = new Rectangle { Fill = White };
                        Grid.SetColumn(r3, 1);
                        Grid.SetRow(r3, 1);
                        Grid.SetColumnSpan(r3, 2);
                        if (j == 9) Grid.SetColumnSpan(r3, 4);
                        GameFrame.Children.Add(r3);

                        //FIRST ROLL IN THIS FRAME
                        TextBlock Ball1 = new TextBlock { Text = Players[i].Game.Frames[j].RollONE, FontSize = 24, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        GameFrame.Children.Add(Ball1);
                        //SECOND ROLL IN THIS FRAME
                        TextBlock Ball2 = new TextBlock { Text = Players[i].Game.Frames[j].RollTWO, FontSize = 24, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        Grid.SetColumn(Ball2, 2);
                        GameFrame.Children.Add(Ball2);
                        //THIRD BALL FOR THE TENTH FRAME
                        if (j == 9)
                        {
                            Rectangle r6 = new Rectangle { Fill = White };
                            Grid.SetColumn(r6, 3);
                            GameFrame.Children.Add(r6);
                            TextBlock Ball3 = new TextBlock { Text = Players[i].Game.Frames[j].RollTHREE, FontSize = 24, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                            Grid.SetColumn(Ball3, 4);
                            GameFrame.Children.Add(Ball3);
                        }
                        //TOTAL FOR THIS FRAME

                        TextBlock Total = new TextBlock { Text = Players[i].Game.Frames[j].Total, FontSize = 24, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        Grid.SetRow(Total, 2);
                        Grid.SetColumnSpan(Total, 3);
                        if (j == 9) Grid.SetColumnSpan(Total, 5);
                        GameFrame.Children.Add(Total);
                        
                        Rectangle r4 = new Rectangle { Width = 1, Height = 75, Fill = White };
                        s1.Children.Add(r4);
                    }
                    Rectangle Bottom = new Rectangle { Width = 946, Height = 1, Fill = White };
                    s.Children.Add(Bottom);
                }
            }
            else
            {
                GameGridContainer.Visibility = Visibility.Collapsed;
                //IntroText.Visibility = Visibility.Visible;
            }
            GameBorder.Child = s;
            if (!IsGameEnded) HighlightCurrentFrame();
        }

        private void ShowStartGameButton()
        {
            StartGameButton.Visibility = Visibility.Visible;
            BottomBar.IsOpen = true;
        }

        private void HideStartGameButton()
        {
            StartGameButton.Visibility = Visibility.Collapsed;
        }

        private void ShowResetGameButton()
        {
            ResetGameButton.Visibility = Visibility.Visible;
        }

        private void HideResetGameButton()
        {
            ResetGameButton.Visibility = Visibility.Collapsed;
        }

        Grid OldGrid = new Grid();
        StackPanel OldStackPanel = new StackPanel();

        void GameFrame_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //OldGrid.Background = ClearBrush;
            //Grid g = sender as Grid;
            //g.Background = GrayBrush;
            //OldGrid = g;
        }

        static Color Gray = Color.FromArgb(50,255,255,255);
        SolidColorBrush GrayBrush = new SolidColorBrush(Gray);
        SolidColorBrush ClearBrush = new SolidColorBrush(Colors.Transparent);

        void PlayerName_Tapped(object sender, TappedRoutedEventArgs e)
        {
            OldStackPanel.Background = ClearBrush;
            TextBlock t = sender as TextBlock;
            Grid g = t.Parent as Grid;
            StackPanel s = g.Parent as StackPanel;
            if (s != OldStackPanel)
            {
                s.Background = GrayBrush;
                OldStackPanel = s;
                ShowRemoveButton();
            }
            else
            {
                OldStackPanel = new StackPanel();
                HideRemoveButton();
            }
        }

        private void ShowRemoveButton()
        {
            RemovePlayerButton.Visibility = Visibility.Visible;
            BottomBar.IsOpen = true;
        }

        private void HideRemoveButton()
        {
            RemovePlayerButton.Visibility = Visibility.Collapsed;
        }

        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Game = new BowlingLogic.Game();
            }
            IsGameEnded = true;
            CurrentPlayer = 0;
            HideResetGameButton();
            ShowStartGameButton();
            ShowAddPlayerButton();
            HideScoringButtons();
            ResetAllFrameBackgrounds();
            ResetScoringButtons();
            BuildGameBoard();
        }

        private void ShowScoringButtons()
        {
            ScoringButtons.Visibility = Visibility.Visible;
            AdBox.Visibility = Visibility.Visible;
        }

        private void HideScoringButtons()
        {
            AdBox.Visibility = Visibility.Collapsed;
            ScoringButtons.Visibility = Visibility.Collapsed;
        }

        private void DeleteGameButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteGame();
        }

        private async void DeleteGame()
        {
            MessageDialog m = new MessageDialog("Are you sure you want to delete this entire game?  All players and data will be lost. This cannot be undone.", "Delete This Game?");
            m.Commands.Add(new UICommand("Yes"));
            m.Commands.Add(new UICommand("No"));
            m.DefaultCommandIndex = 1;

            var command = await m.ShowAsync();

            if (command.Label == "Yes")
            {
                GameGridContainer.Visibility = Visibility.Collapsed;
                Players.Clear();
                HideDeleteGameButton();
                HideStartGameButton();
                HideResetGameButton();
                ShowAddPlayerButton();
                BottomBar.IsOpen = true;
                SetGameDate();
            }
        }

        private void ShowPlayerPopup()
        {
            BottomBar.IsOpen = true;
            AddPlayerPopup.IsOpen = true;
            AddNewPlayerError.Visibility = Visibility.Collapsed;
            AddNewPlayerText.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void StartGame()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Game = new BowlingLogic.Game();
            }            
            ShowScoringButtons();
            BottomBar.IsOpen = false;
            IsGameEnded = false;
            HideAddPlayerButton();
            BuildGameBoard();
        }

        private void ShowAddPlayerButton()
        {
            AddPlayerButton.Visibility = Visibility.Visible;
        }

        private void HideAddPlayerButton()
        {
            AddPlayerButton.Visibility = Visibility.Collapsed;
        }

        private async void RemovePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            if (OldStackPanel.DataContext != null)
            {
                Player p = OldStackPanel.DataContext as Player;
                string name = p.Name;
                if (name.Substring(name.Length-1) == "s") name = name + "'";
                else name = name + "'s";
                MessageDialog m = new MessageDialog("Are you sure you want to delete " + name + " game?  This cannot be undone.", "Delete " + name + " Game?");
                m.Commands.Add(new UICommand("Yes"));
                m.Commands.Add(new UICommand("No"));
                m.DefaultCommandIndex = 1;

                var command = await m.ShowAsync();

                if (command.Label == "Yes")
                {
                    RemovePlayer(p);
                }
            }
        }

        private void RemovePlayer(Player p)
        {
            Players.Remove(p);
            HideRemoveButton();
            BuildGameBoard();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            HideStartGameButton();
            ShowResetGameButton();
            StartGame();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int pins = Int32.Parse(b.Name.Substring(6));
            BowlingLogic.Game g = Players[CurrentPlayer].Game;
            if (g.Roll(g.CurrentFrame, CurrentRoll, pins))
            {
                CurrentPlayer++;
                ResetScoringButtons();
                if (g.CurrentFrame == 10)
                {
                    EndGame();
                }
            }
            else
            {
                if (g.CurrentFrame != 9) ChangeNumberButtons(pins);
                else
                {
                    if (pins != 10) ChangeNumberButtons(pins);
                    else
                    {
                        ResetScoringButtons();
                    }
                }
            }

            if (CurrentPlayer >= Players.Count)
                CurrentPlayer = 0;
            BuildGameBoard();
        }

        private void HighlightCurrentFrame()
        {
            if (Players.Count != 0)
            {
                StackPanel s = GameBorder.Child as StackPanel;
                StackPanel s1 = s.Children[(2 * CurrentPlayer)] as StackPanel;
                Grid g = s1.Children[2 * (Players[CurrentPlayer].Game.CurrentFrame + 1)] as Grid;
                g.Background = GrayBrush;
            }
        }

        private void ResetAllFrameBackgrounds()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                StackPanel s = GameBorder.Child as StackPanel;
                StackPanel s1 = s.Children[(2 * i)] as StackPanel;
                for (int j = 0; j < 10; j++)
                {
                    Grid g = s1.Children[2 * (j)] as Grid;
                    g.Background = ClearBrush;
                }
            }
        }

        private void EndGame()
        {
            if (CurrentPlayer == (Players.Count))
            {
                IsGameEnded = true;
                ScoringButtons.Visibility = Visibility.Collapsed;
                ShowAddPlayerButton();
                HideResetGameButton();
                ShowStartGameButton();
            }
            
            bool WasAdded = false;
            for (int i = 0; i < App.PlayerHistory.Count; i++)
            {
                Player p = App.PlayerHistory[i];
                if (p.Name == Players[CurrentPlayer - 1].Name)
                {
                    WasAdded = true;
                    p.GameHistory.Add(Players[CurrentPlayer - 1].Game);
                }

            }
            if (!WasAdded)
            {
                Players[CurrentPlayer - 1].GameHistory.Add(Players[CurrentPlayer - 1].Game);
                App.PlayerHistory.Add(Players[CurrentPlayer - 1]);
            }
            App.SavePlayerData();
        }

        private async void SavePlayerData()
        {
            var applicationData = ApplicationData.Current;
            var storageFolder = applicationData.LocalFolder;

            var file = await storageFolder.CreateFileAsync("playerdata.xml", CreationCollisionOption.OpenIfExists);

            var dataContractSerializer = new DataContractSerializer(typeof(Player));
            var memoryStream = new MemoryStream();
            dataContractSerializer.WriteObject(memoryStream, Players);
            memoryStream.Seek(0, SeekOrigin.Begin);

            string serialized = new StreamReader(memoryStream).ReadToEnd();
            memoryStream.Dispose();

            var doc = new XmlDocument();
            doc.LoadXml(serialized);

            await doc.SaveToFileAsync(file);
        }

        private void ChangeNumberButtons(int roll1)
        {
            for (int i = 0; i <= 10; i++)
            {
                Button b = FindName("Button" + i) as Button;

                if (i > (10 - roll1))
                {
                    b.Visibility = Visibility.Collapsed;
                }
                else if (i == (10 - roll1))
                {
                    b.Content = "/";
                }
            }

            if ((Players[CurrentPlayer].Game.CurrentFrame == 9) && (Players[CurrentPlayer].Game.CurrentRoll == 3) && (!(Players[CurrentPlayer].Game.Frames[9].Roll1 == 10)) && (Players[CurrentPlayer].Game.Frames[9].Roll1 + Players[CurrentPlayer].Game.Frames[9].Roll2 == 10))
            {
                ResetScoringButtons();
            }
        }

        private void ResetScoringButtons()
        {
            for (int i = 0; i <= 10; i++)
            {
                Button b = FindName("Button" + i) as Button;
                b.Visibility = Visibility.Visible;
                b.Content = i.ToString();
            }

            Button10.Content = "x";
        }

        private void ExistingPlayerName_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AddPlayerPopup.IsOpen = false;
            Grid g = sender as Grid;
            Player p = g.DataContext as Player;
            p.Game = new BowlingLogic.Game();
            Players.Add(p);
            AddNewPlayerCleanup();
            PlayerList.ItemsSource = GetPlayerList();
        }
    }
}
