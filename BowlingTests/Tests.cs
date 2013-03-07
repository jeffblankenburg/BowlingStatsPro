using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingLogic;

namespace BowlingTests
{
    [TestClass]
    public class Tests
    {
        private Player p;

        [TestInitialize]
        public void SetUp()
        {
            p = new Player();
        }

        [TestMethod]
        public void AllGutterBalls()
        {
            p.Game.Roll(0, 1, 0);
            p.Game.Roll(0, 2, 0);
            p.Game.Roll(1, 1, 0);
            p.Game.Roll(1, 2, 0);
            p.Game.Roll(2, 1, 0);
            p.Game.Roll(2, 2, 0);
            p.Game.Roll(3, 1, 0);
            p.Game.Roll(3, 2, 0);
            p.Game.Roll(4, 1, 0);
            p.Game.Roll(4, 2, 0);
            p.Game.Roll(5, 1, 0);
            p.Game.Roll(5, 2, 0);
            p.Game.Roll(6, 1, 0);
            p.Game.Roll(6, 2, 0);
            p.Game.Roll(7, 1, 0);
            p.Game.Roll(7, 2, 0);
            p.Game.Roll(8, 1, 0);
            p.Game.Roll(8, 2, 0);
            p.Game.Roll(9, 1, 0);
            p.Game.Roll(9, 2, 0);

            Assert.AreEqual(0, p.Game.CalculateTotalScore());
        }

        [TestMethod]
        public void AllOnes()
        {
            p.Game.Roll(0, 1, 1);
            p.Game.Roll(0, 2, 1);
            p.Game.Roll(1, 1, 1);
            p.Game.Roll(1, 2, 1);
            p.Game.Roll(2, 1, 1);
            p.Game.Roll(2, 2, 1);
            p.Game.Roll(3, 1, 1);
            p.Game.Roll(3, 2, 1);
            p.Game.Roll(4, 1, 1);
            p.Game.Roll(4, 2, 1);
            p.Game.Roll(5, 1, 1);
            p.Game.Roll(5, 2, 1);
            p.Game.Roll(6, 1, 1);
            p.Game.Roll(6, 2, 1);
            p.Game.Roll(7, 1, 1);
            p.Game.Roll(7, 2, 1);
            p.Game.Roll(8, 1, 1);
            p.Game.Roll(8, 2, 1);
            p.Game.Roll(9, 1, 1);
            p.Game.Roll(9, 2, 1);
            Assert.AreEqual(20, p.Game.CalculateTotalScore());
        }

        [TestMethod]
        public void OneSpareAndTheRestGutterBalls()
        {
            p.Game.Roll(0, 1, 5);
            p.Game.Roll(0, 2, 5);
            p.Game.Roll(1, 1, 8);
            p.Game.Roll(1, 2, 0);
            p.Game.Roll(2, 1, 0);
            p.Game.Roll(2, 2, 0);
            p.Game.Roll(3, 1, 0);
            p.Game.Roll(3, 2, 0);
            p.Game.Roll(4, 1, 0);
            p.Game.Roll(4, 2, 0);
            p.Game.Roll(5, 1, 0);
            p.Game.Roll(5, 2, 0);
            p.Game.Roll(6, 1, 0);
            p.Game.Roll(6, 2, 0);
            p.Game.Roll(7, 1, 0);
            p.Game.Roll(7, 2, 0);
            p.Game.Roll(8, 1, 0);
            p.Game.Roll(8, 2, 0);
            p.Game.Roll(9, 1, 0);
            p.Game.Roll(9, 2, 0);
            Assert.AreEqual(26, p.Game.CalculateTotalScore());
        }

        [TestMethod]
        public void OneStrikeAndTheRestGutterBalls()
        {
            p.Game.Roll(0, 1, 10);
            p.Game.Roll(1, 1, 8);
            p.Game.Roll(1, 2, 1);
            p.Game.Roll(2, 1, 0);
            p.Game.Roll(2, 2, 0);
            p.Game.Roll(3, 1, 0);
            p.Game.Roll(3, 2, 0);
            p.Game.Roll(4, 1, 0);
            p.Game.Roll(4, 2, 0);
            p.Game.Roll(5, 1, 0);
            p.Game.Roll(5, 2, 0);
            p.Game.Roll(6, 1, 0);
            p.Game.Roll(6, 2, 0);
            p.Game.Roll(7, 1, 0);
            p.Game.Roll(7, 2, 0);
            p.Game.Roll(8, 1, 0);
            p.Game.Roll(8, 2, 0);
            p.Game.Roll(9, 1, 0);
            p.Game.Roll(9, 2, 0);
            Assert.AreEqual(28, p.Game.CalculateTotalScore());
        }

        [TestMethod]
        public void AllGutterballsExceptForLastFrameStrikeSpare()
        {
            p.Game.Roll(0, 0, 0);
            p.Game.Roll(0, 1, 0);
            p.Game.Roll(1, 1, 0);
            p.Game.Roll(1, 2, 0);
            p.Game.Roll(2, 1, 0);
            p.Game.Roll(2, 2, 0);
            p.Game.Roll(3, 1, 0);
            p.Game.Roll(3, 2, 0);
            p.Game.Roll(4, 1, 0);
            p.Game.Roll(4, 2, 0);
            p.Game.Roll(5, 1, 0);
            p.Game.Roll(5, 2, 0);
            p.Game.Roll(6, 1, 0);
            p.Game.Roll(6, 2, 0);
            p.Game.Roll(7, 1, 0);
            p.Game.Roll(7, 2, 0);
            p.Game.Roll(8, 1, 0);
            p.Game.Roll(8, 2, 0);
            p.Game.Roll(9, 1, 10);
            p.Game.Roll(9, 2, 7);
            p.Game.Roll(9, 3, 3);
            Assert.AreEqual(20, p.Game.CalculateTotalScore());
        }

        [TestMethod]
        public void RandomUnfinishedGame()
        {
            p.Game.Roll(0, 1, 8);
            p.Game.Roll(0, 2, 0);
            p.Game.Roll(1,1,10);
            p.Game.Roll(2,1,8);
            p.Game.Roll(2,2,1);
            Assert.AreEqual(36, p.Game.CalculateTotalScore());
        }

        [TestMethod]
        public void PerfectGame()
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
            Assert.AreEqual(300, p.Game.CalculateTotalScore());
        }

        //[TestMethod]
        //public void CurrentFrameCheck()
        //{
        //    p.Game.Roll(8);
        //    p.Game.Roll(0);
        //    p.Game.Roll(10);
        //    p.Game.Roll(8);
        //    p.Game.Roll(1);
        //    Assert.AreEqual(3, p.Game.CurrentFrame);
        //}

        //private void RollMany(int rolls, int pins)
        //{
        //    for (int i = 0; i < rolls; i++)
        //    {
        //        p.Game.Roll(pins);
        //    }
        //}
    }
}
