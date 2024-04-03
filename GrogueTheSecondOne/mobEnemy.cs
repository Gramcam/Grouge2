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
        int prevRowNum, prevColNum;

        public int Xloc { get { return colNum; } }
        public int YLoc { get { return rowNum; } }
        public int prevXloc { get { return prevColNum; } }
        public int prevYLoc { get { return prevRowNum; } }
        public char Sprite { get { return asciiSprite; } }

        public mobEnemy(int row, int col)
        {
            asciiSprite = 'N';
            rowNum = row;
            colNum = col;
        }

        public void moveUp(mobEnemy M)
        {
            prevColNum = colNum;
            prevRowNum = rowNum;
            M.rowNum -= 1;
        }
    }

   
}
