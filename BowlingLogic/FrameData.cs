using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingLogic
{
    public class FrameData
    {
        public int Roll1 { get; set; }
        public int Roll2 { get; set; }
        public int Roll3 { get; set; }
        public string RollONE { get; set; }
        public string RollTWO { get; set; }
        public string RollTHREE { get; set; }
        public string Total { get; set; }
        public bool IsCompleted { get; set; }

        public FrameData()
        {
            IsCompleted = false;
            Total = String.Empty;
            RollONE = String.Empty;
            RollTWO = String.Empty;
            RollTHREE = String.Empty;
        }
    }
}
