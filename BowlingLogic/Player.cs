using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingLogic
{
    public class Player
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string EmailAddress { get; set; }
        public Game Game { get; set; }
        public List<Game> GameHistory { get; set; }
        public double Average { get; set; }
        public int StrikeTotal {get; set;}
        public int SpareTotal { get; set; }
        public int GutterballTotal { get; set; }
        public double StrikePercentage { get; set; }
        public double SparePercentage { get; set; }
        public int TotalPins { get; set; }

        public Player()
        {
            Game = new Game();
            GameHistory = new List<Game>();
        }

        public Player(string name)
        {
            Name = name;
            if (name.Length > 10) Nickname = name.Substring(0, 10);
            else Nickname = name;
            EmailAddress = "";
            Game = new Game();
            GameHistory = new List<Game>();
            UpdateStatistics();
        }

        public void UpdateStatistics()
        {
            int TotalScore = 0;
            int StrikeCount = 0;
            int SpareCount = 0;
            int GutterballCount = 0;
            for (int i = 0; i < GameHistory.Count; i++)
            {
                TotalScore += GameHistory[i].Score;

                for (int j = 0; j < 10; j++)
                {
                    if (GameHistory[i].Frames[j].RollONE == "X") StrikeCount++;
                    else if (GameHistory[i].Frames[j].RollONE == "-") GutterballCount++;
                    if (GameHistory[i].Frames[j].RollTWO == "X") StrikeCount++;
                    else if (GameHistory[i].Frames[j].RollTWO == "/") SpareCount++;
                    else if (GameHistory[i].Frames[j].RollTWO == "-") GutterballCount++;
                    if (GameHistory[i].Frames[j].RollTHREE == "X") StrikeCount++;
                    else if (GameHistory[i].Frames[j].RollTHREE == "/") SpareCount++;
                    else if (GameHistory[i].Frames[j].RollTHREE == "-") GutterballCount++;
                }
            }

            StrikeTotal = StrikeCount;
            SpareTotal = SpareCount;
            GutterballTotal = GutterballCount;
            TotalPins = TotalScore;

            if (GameHistory.Count != 0)
            {
                Average = (double)TotalScore / (double)GameHistory.Count;
                StrikePercentage = ((double)StrikeTotal / (double)GameHistory.Count * 10) * 100;
                SparePercentage = ((double)SpareTotal / (double)GameHistory.Count * 10) * 100;
            }
            else
            {
                Average = 0;
                StrikePercentage = 0;
                SparePercentage = 0;
            }
        }
    }
}
