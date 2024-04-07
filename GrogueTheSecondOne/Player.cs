using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrogueTheSecondOne
{
    internal class Player
    {
        private char asciiSprite;
        private int rowNum, colNum;
        private int prevRowNum, prevColNum;
        private Random rnd = new Random();

        public int XLoc { get { return colNum; } }
        public int YLoc { get { return rowNum; } }
        public int prevXLoc { get { return prevColNum; } }
        public int prevYLoc { get { return prevRowNum; } }
        public char Sprite { get { return asciiSprite; } }
        private enum direction
        { 
        }
    }
}
