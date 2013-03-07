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
        public Game Game { get; set; }
        public List<Game> GameHistory { get; set; }

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
            Game = new Game();
            GameHistory = new List<Game>();
        }
    }
}
