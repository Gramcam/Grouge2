using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrogueTheSecondOne
{
    internal class Treasure
    {
        private char asciiSprite;
        private int rowNum, colNum;
        private bool inActive = false;
        public int XLoc { get { return colNum; } }
        public int YLoc { get { return rowNum; } }
        public bool nonActive { get { return inActive; } }
        public char Sprite { get { return asciiSprite; } }
        public Treasure(int row, int col)
        {
            asciiSprite = (char)Form1.asciiTiles.treasure;
            rowNum = row;
            colNum = col;
        }

        public void TreasureCollect(Player playerCharacter, List<Treasure> mapTreasures)
        {
            int playerXDiff, playerYDiff;
            playerXDiff = playerCharacter.XLoc - colNum;
            playerYDiff = playerCharacter.YLoc - rowNum;
            //if ((playerXDiff <= 1 || playerXDiff <= -1) && (playerYDiff <= 1 || playerYDiff <= -1))
            if (playerXDiff == 0 && playerYDiff == 0)
            {
                playerCharacter.playerTreasure++;
                Die(mapTreasures);
            }

        }

        public void Die(List<Treasure> mapTreasures)
        {
            asciiSprite = (char)Form1.asciiTiles.empty;
            inActive = true;
        }
    }
}