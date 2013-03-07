using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingLogic
{
    public class Game
    {
        public DateTime DateTime { get; set; }
        public FrameData[] Frames;
        public int CurrentFrame = 0;
        public int CurrentRoll = 1;
        public int Score = 0;

        public Game()
        {
            DateTime = DateTime.Now;
            Frames = new FrameData[10] { new FrameData(), new FrameData(), new FrameData(), new FrameData(), new FrameData(), new FrameData(), new FrameData(), new FrameData(), new FrameData(), new FrameData() };
        }

        public bool Roll(int frame, int roll, int pins)
        {
            switch (CurrentRoll)
            {
                case 1:
                    Frames[frame].Roll1 = pins;
                    Frames[frame].RollONE = pins.ToString();
                    CurrentRoll = 2;
                    if (pins == 10)
                    {
                        if (frame != 9)
                        {
                            Frames[frame].RollONE = "";
                            Frames[frame].RollTWO = "X";
                            Frames[frame].IsCompleted = true;
                            CurrentRoll = 1;
                            CurrentFrame++;
                        }
                        else
                        {
                            Frames[frame].RollONE = "X";
                        }
                    }
                    else if (pins == 0)
                    {
                        Frames[frame].RollONE = "-";   
                    }

                    break;
                case 2:
                    Frames[frame].Roll2 = pins;
                    Frames[frame].RollTWO = pins.ToString();
                    if (IsSpare(frame)) Frames[frame].RollTWO = "/";
                    else if (pins == 0) Frames[frame].RollTWO = "-";


                    if ((frame < 9) || ((!IsStrike(frame)) && (!IsSpare(frame)) && (frame == 9)))
                    {
                        Frames[frame].IsCompleted = true;
                        CurrentFrame++;
                        CurrentRoll = 1;
                    }
                    else
                    {
                        if ((pins == 10) && (IsStrike(frame))) Frames[frame].RollTWO = "X";
                        CurrentRoll = 3;   
                    }
                    break;
                case 3:
                    Frames[frame].Roll3 = pins;
                    Frames[frame].RollTHREE = pins.ToString();
                    if ((IsStrike(frame)) && (Frames[frame].Roll2 != 10) && (Frames[frame].Roll2 + Frames[frame].Roll3 == 10))
                    {
                        Frames[frame].RollTHREE = "/";
                    }
                    else if ((Frames[frame].Roll1 == 10) && (Frames[frame].Roll2 == 10) && (pins == 10)) Frames[frame].RollTHREE = "X";
                    Frames[frame].IsCompleted = true;
                    CurrentFrame++;
                    break;
            }

            ScoreFrames();
            return Frames[frame].IsCompleted;
        }

        public void ScoreFrames()
        {
            int score = 0;
            
            for (int i = 0; i < Frames.Length; i++)
            {
                if (Frames[i].IsCompleted)
                {
                    if (IsStrike(i))
                    {
                        if (i != 9)
                        {
                            if ((i + 1) == 9)
                            {
                                if ((CurrentFrame >= 9) && (CurrentRoll == 3))
                                {
                                    Frames[i].Total = (score + (10 + Frames[i + 1].Roll1 + Frames[i + 1].Roll2)).ToString();
                                }
                            }
                            else if (!Frames[i + 1].IsCompleted)
                            {
                                Frames[i].Total = String.Empty;
                            }
                            else if (Frames[i + 1].Roll1 == 10)
                            {
                                if (i == 8)
                                {
                                    Frames[i].Total = (score + (10 + Frames[i + 1].Roll1 + Frames[i + 1].Roll3)).ToString();
                                }
                                else if ((i == 7) && (CurrentFrame >= 9) && (CurrentRoll > 1))
                                {
                                    Frames[i].Total = (score + (10 + Frames[8].Roll1 + Frames[9].Roll1)).ToString();
                                }
                                else
                                {
                                    if (!Frames[i + 2].IsCompleted)
                                    {
                                        Frames[i].Total = String.Empty;
                                    }
                                    else
                                    {
                                        Frames[i].Total = (score + (10 + Frames[i + 1].Roll1 + Frames[i + 2].Roll1)).ToString();
                                    }
                                }
                            }
                            else
                            {
                                Frames[i].Total = (score + (10 + Frames[i + 1].Roll1 + Frames[i + 1].Roll2)).ToString();
                            }
                        }
                        else
                        {
                            Frames[i].Total = (score + (10 + Frames[i].Roll2 + Frames[i].Roll3)).ToString();
                        }
                    }
                    else if (IsSpare(i))
                    {
                        if (i != 9)
                        {
                            if ((i + 1) == 9)
                            {
                                if ((CurrentFrame >= 9) && (CurrentRoll >= 2))
                                {
                                    Frames[i].Total = (score + (10 + Frames[i + 1].Roll1)).ToString();
                                }
                            }
                            else if (Frames[i + 1].IsCompleted)
                            {
                                Frames[i].Total = (score + (10 + Frames[i + 1].Roll1)).ToString();
                            }
                            else if ((CurrentFrame >= i) && (CurrentRoll == 2))
                            {
                                Frames[i].Total = (score + (10 + Frames[i + 1].Roll1)).ToString();
                            }
                            else
                            {
                                Frames[i].Total = String.Empty;
                            }
                        }
                        else
                        {
                            Frames[i].Total = (score + (10 + Frames[i].Roll3)).ToString();
                        }
                        
                    }
                    else
                    {
                        if (i != 9)
                        {
                            Frames[i].Total = (score + (Frames[i].Roll1 + Frames[i].Roll2)).ToString();
                        }
                        else
                        {
                            Frames[i].Total = (score + (Frames[i].Roll1 + Frames[i].Roll2 + Frames[i].Roll3)).ToString();
                        }
                    }
                    //TALLY THE SCORE
                    if (Frames[i].Total != String.Empty)
                    {
                        score = Int32.Parse(Frames[i].Total);
                        Score = score;
                    }
                }
            }
        }

        public int CalculateTotalScore()
        {
            int score = 0;
            
            for (int i = 0; i < 10; i++)
            {
                if (Frames[i].Total != String.Empty)
                {
                    score = Int32.Parse(Frames[i].Total);
                }
            }

            return score;
        }

        public FrameData GetFrame(int frame)
        {
            FrameData f = new FrameData();

            return new FrameData();
        }

        private bool IsSpare(int frameIndex)
        {
            return Frames[frameIndex].Roll1 + Frames[frameIndex].Roll2 == 10;
        }

        private bool IsStrike(int frameIndex)
        {
            return Frames[frameIndex].Roll1 == 10;
        }
    }
}
