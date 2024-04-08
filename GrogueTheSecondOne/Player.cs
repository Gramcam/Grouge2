using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GrogueTheSecondOne.mobEnemy;

namespace GrogueTheSecondOne
{
    internal class Player
    {
        private char asciiSprite;
        private int rowNum, colNum;
        private int prevRowNum, prevColNum;
        private Random rnd = new Random();
        //Movement Enums for 9 directions
        private enum direction
        {
            H, NW, N, NE,
            W, E,
            SW, S, SE
        }

        private int[,] directionManipulations = new int[,]
        {
            {0, 0 }, //H
            { -1, -1}, //NW
            { -1, 0 }, //N
            { -1, 1 }, // NE
            { 0, -1 }, // W
            { 0, 1 }, // E
            { 1, -1 }, // SW
            { 1, 0 }, // S
            { 1, 1 } // SE
        };

        public int XLoc { get { return colNum; } }
        public int YLoc { get { return rowNum; } }
        public int prevXLoc { get { return prevColNum; } }
        public int prevYLoc { get { return prevRowNum; } }
        public char Sprite { get { return asciiSprite; } }

        public Player(int row, int col)
        {
            asciiSprite = (char)Form1.asciiTiles.alivePlayer;
            rowNum = row;
            colNum = col;
        }
        public void moveUp(char[,] mapArr, int directionIndex)
        {
            prevColNum = colNum;
            prevRowNum = rowNum;
            direction chosenDirection = (direction)directionIndex;
            int i = (int)chosenDirection;
            //looping through directionmanipulation arrays. Array i, index 0 and 1.
            int rowChange = directionManipulations[i, 0];
            int colChange = directionManipulations[i, 1];

            //Each direction will be applied to row and col
            int checkRow = rowNum + rowChange;
            int checkCol = colNum + colChange;
            if (mapArr[checkRow, checkCol] == (char)Form1.asciiTiles.wall)
            {
                rowNum = prevRowNum;
                colNum = prevColNum;
            }
            else
            {
            colNum = checkCol;
            rowNum = checkRow;
            }
        }



    }
}
