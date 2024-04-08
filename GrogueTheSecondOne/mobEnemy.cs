using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private bool shouldDie;
        private Random rnd = new Random();

        //Movement Enums for 9 directions
        private enum direction
        {
            H, NW, N, NE,
            W, E,
            SW, S, SE
        }
        private direction chosenDir;

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
        public bool removeState { get { return shouldDie; } }

        public mobEnemy(int row, int col)
        {
            asciiSprite = (char)Form1.asciiTiles.aliveEnemy;
            rowNum = row;
            colNum = col;
        }

        public void moveUp(mobEnemy M)
        {
            prevColNum = colNum;
            prevRowNum = rowNum;
            M.rowNum -= 1;
        }

        private List<direction> GetAvailableDirections(char[,] mapChars)
        {
            List<direction> availableDirections = new List<direction>();

            //use a loop to check each possible direction 
            //Start at 1 to avoid checking current position
            for (int i = 0; i < directionManipulations.GetLength(0); i++)
            {
                //looping through directionmanipulation arrays. Array i, index 0 and 1.
                int rowChange = directionManipulations[i, 0];
                int colChange = directionManipulations[i, 1];

                //Each direction will be applied to row and col
                int checkRow = rowNum + rowChange;
                int checkCol = colNum + colChange;

                if (mapChars[checkRow, checkCol] != (char)Form1.asciiTiles.wall && mapChars[checkRow, checkCol] != (char)Form1.asciiTiles.aliveEnemy) /*&& mapChars[checkRow, checkCol] != (char)Form1.asciiTiles.alivePlayer)*/
                {
                    availableDirections.Add((direction)i);
                }

            }
            return availableDirections;
        }



        private List<direction> BasicPathing(Player playerCharacter)
        {
            List<direction> optimalDirections = new List<direction>();
            //Add list of Y directions towards the player.
            //If negative enemy needs to move north
            if (playerCharacter.YLoc - rowNum < 0)
            {
                for (int i = 1; i <= 3; i++)
                {
                    optimalDirections.Add((direction)i);
                }
            }
            else if (playerCharacter.YLoc - rowNum > 0)
            {
                for (int i = 6; i <= 8; i++)
                {
                    optimalDirections.Add((direction)i);
                }
            }
            else
            {
                for (int i = 4; i <= 5; i++)
                    optimalDirections.Add((direction)i);
            }
            //Check for the x direction
            //Remove counter directions from optimal directions
            if (playerCharacter.XLoc - colNum < 0)
            {
                optimalDirections.Remove(direction.E);
                optimalDirections.Remove(direction.NE);
                optimalDirections.Remove(direction.SE);
            }
            else if (playerCharacter.XLoc - colNum > 0)
            {
                optimalDirections.Remove(direction.W);
                optimalDirections.Remove(direction.NW);
                optimalDirections.Remove(direction.SW);
            }
            else
            {
                optimalDirections.Remove(direction.N);
                optimalDirections.Remove(direction.S);
            }
            return optimalDirections;
        }
        private bool DetectionRadius(Player playerCharacter, int detectRange)
        {
            int playerXDiff, playerYDiff;
            playerXDiff = playerCharacter.XLoc - colNum;
            playerYDiff = playerCharacter.YLoc - rowNum;
            if ((playerXDiff <= detectRange || playerXDiff <= detectRange * -1) && (playerYDiff <= detectRange || playerYDiff <= detectRange * -1))
                return true;
            else
                return false;
        }

        public void MobMoveArrManip(char[,] mapChars, Player playerCharacter)
        {
            List<direction> availableDirections = GetAvailableDirections(mapChars);

            if (DetectionRadius(playerCharacter, 2))
            {
                List<direction> advantageMove = new List<direction>();
                List<direction> pathedmoves = BasicPathing(playerCharacter).ToList();
                advantageMove = availableDirections.Intersect(pathedmoves).ToList();

                if (advantageMove.Count >= 1)
                {
                    //Random to select direction from availabledirections
                    int pickedDir = rnd.Next(0, advantageMove.Count);

                    chosenDir = advantageMove[pickedDir];
                }
            }
            else if (availableDirections.Count >= 1)
            {
                chosenDir = availableDirections[rnd.Next(0, availableDirections.Count)];
            }

            //Get the manipulation values from the 2d direction array
            int[] manipulationOperators = new int[2];
            manipulationOperators[0] = directionManipulations[(int)chosenDir, 0];
            manipulationOperators[1] = directionManipulations[(int)chosenDir, 1];

            //Apply the changes
            prevColNum = colNum;
            prevRowNum = rowNum;
            rowNum += manipulationOperators[0];
            colNum += manipulationOperators[1];
            // Check if the new position is within the screen boundaries, rest. Bug Fix
            if (rowNum <= 0 || rowNum >= mapChars.GetLength(0) - 1 || colNum <= 0 || colNum >= mapChars.GetLength(1) - 1)
            {
                rowNum = prevRowNum;
                colNum = prevColNum;
            }
        }

        public void AttackPlayer(Player playerCharacter, List<mobEnemy> mobEnemies)
        {
            int playerXDiff, playerYDiff;
            playerXDiff = playerCharacter.XLoc - colNum;
            playerYDiff = playerCharacter.YLoc - rowNum;
            //if ((playerXDiff <= 1 || playerXDiff <= -1) && (playerYDiff <= 1 || playerYDiff <= -1))
            if (playerXDiff == 0 && playerYDiff == 0)
            {

                playerCharacter.playerHealth--;
                Die(mobEnemies);
            }

        }

        
        public void Die(List<mobEnemy> mobEnemies)
        {
            asciiSprite = (char)Form1.asciiTiles.empty;
            shouldDie = true;
        }

        //private void RemoveEnemyMob(mobEnemy N)
        //{
        //    N.Die();
        //    UpdateEnemyRows(N.Sprite, N.YLoc, N.XLoc, N.prevYLoc, N.prevXLoc);
        //    enemyList.RemoveAt(enemyList.IndexOf(N));
        //}
    }
}
