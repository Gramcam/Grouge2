using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GrogueTheSecondOne
{
    internal class mobEnemy
    {
        private char asciiSprite;
        private int rowNum, colNum;
        private int prevRowNum, prevColNum;

        //Movement Enums
        public enum direction
        {
            NW, N, NE,
            W, H, E,
            SW, S, SE
        }

        private direction chosenDir;
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

        public void MobMoveArrManip()
        {
            //Store enum names in array, get the length of the array
            int enumCount = Enum.GetNames(typeof(direction)).Length;
            Random rnd = new Random();

            chosenDir = (direction)rnd.Next(0, enumCount++);


            switch (chosenDir)
            {
                case direction.NW:
                    prevColNum = colNum;
                    prevRowNum = rowNum;
                    rowNum--;
                    colNum--;
                    break;
                case direction.N:
                    prevColNum = colNum;
                    prevRowNum = rowNum;
                    rowNum--;
                    break;
                case direction.NE:
                    prevColNum = colNum;
                    prevRowNum = rowNum;
                    rowNum--;
                    colNum++;
                    break;
                case direction.W:
                    prevColNum = colNum;
                    prevRowNum = rowNum;
                    colNum--;
                    break;
                case direction.H:
                    break;
                case direction.E:
                    prevColNum = colNum;
                    prevRowNum = rowNum;
                    colNum++;
                    break;
                case direction.SW:
                    prevColNum = colNum;
                    prevRowNum = rowNum;
                    rowNum++;
                    colNum--;
                    break;
                case direction.S:
                    prevColNum = colNum;
                    prevRowNum = rowNum;
                    rowNum++;
                    break;
                case direction.SE:
                    prevColNum = colNum;
                    prevRowNum = rowNum;
                    rowNum--;
                    colNum++;

                    break;
                default:
                    break;
            }


        }


    }
}
