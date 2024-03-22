using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrogueTheSecondOne
{
    internal class mobEnemy
    {
        char asciiSprite;
        int rowNum, colNum;

        public int Xloc { get { return colNum; } }
        public int YLoc { get { return rowNum; } }
        public char Sprite { get { return asciiSprite; } }

        public mobEnemy(char Sprite, int row, int col)
        {
            asciiSprite = Sprite;
            rowNum = row;
            colNum = col;
        }
    }

   
}
