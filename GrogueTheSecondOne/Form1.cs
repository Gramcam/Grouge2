using System;

namespace GrogueTheSecondOne
{
    public partial class Form1 : Form
    {
        //Map Fields
        private int MapYCount;
        private int mapXCount;
        private char[,] charOriginalMapState;
        private char[,] charPlayArea;

        private Random rnd = new Random();

        //List to store EnemyMobs
        List<mobEnemy> enemyList = new List<mobEnemy>();
        private mobEnemy playSpaceEnemy;

        private Player playerCharacter;

        public enum asciiTiles
        {
            empty = '.',
            wall = '#',
            alivePlayer = '@',
            aliveEnemy = 'N'
        }
        public enum mobSprites
        {
            dead = '.',
        }
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Level Data From Textfile
            LoadLevel("Level_1.txt");
            SpawnEnemies(3);
            playerCharacter = new Player(10, 46);
            UpdateEnemyRows(playerCharacter.Sprite, playerCharacter.YLoc, playerCharacter.XLoc, playerCharacter.prevYLoc, playerCharacter.prevXLoc);

        }


        private void btnTestChange_Click(object sender, EventArgs e)
        {
            //RemoveEnemyMob(enemyList[rnd.Next(0, enemyList.Count())]);
            playerCharacter.moveUp(charPlayArea, 2);
            UpdateEnemyRows(playerCharacter.Sprite, playerCharacter.YLoc, playerCharacter.XLoc, playerCharacter.prevYLoc, playerCharacter.prevXLoc);
            foreach (mobEnemy N in enemyList)
            {
                N.MobMoveArrManip(charPlayArea, playerCharacter);
                UpdateEnemyRows(N.Sprite, N.YLoc, N.XLoc, N.prevYLoc, N.prevXLoc);
            }

        }

        private void LoadLevel(string lvl)
        {
            //Load from textfile and initialize the map arrays
            try
            {
                StreamReader inputFile = File.OpenText(lvl);

                while (!inputFile.EndOfStream)
                {
                    lstPlayArea.Items.Add(inputFile.ReadLine());
                }
                inputFile.Close();
                MapYCount = lstPlayArea.Items.Count;
                mapXCount = lstPlayArea.Items[0].ToString().Length;
    }
            catch
            {
                MessageBox.Show("Failed to retrieve level data!");
            }
            charOriginalMapState = new char[ MapYCount, mapXCount];
            charPlayArea = new char[MapYCount, mapXCount];

            //Nested loop to load level into two 2d arrays
            for (int i = 0; i < MapYCount; i++)
            {
                string line = (lstPlayArea.Items[i].ToString());
                for (int j = 0; j < mapXCount; j++)
                {
                    charOriginalMapState[i, j] = line[j];
                    charPlayArea[i, j] = line[j];
                }
            }

        }
        private void SpawnEnemies(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                int x, y;

                //Choose random location that isnt populated
                do
                {
                    x = rnd.Next(1, mapXCount - 1);
                    y = rnd.Next(1, MapYCount - 1);

                } while (charPlayArea[y, x] != (char)asciiTiles.empty);

                playSpaceEnemy = new mobEnemy(y, x);

                enemyList.Add(playSpaceEnemy);
            }
            foreach (mobEnemy N in enemyList)
                UpdateEnemyRows(N.Sprite, N.YLoc, N.XLoc, N.prevYLoc, N.prevXLoc);
        }

       private void RemoveEnemyMob(mobEnemy N)
        {
            N.Die();
            UpdateEnemyRows(N.Sprite, N.YLoc, N.XLoc, N.prevYLoc, N.prevXLoc);
            enemyList.RemoveAt(enemyList.IndexOf(N));
        }

        private void UpdateEnemyRows(char sprite, int YLoc, int XLoc, int prevYLoc, int prevXLoc )
        {
           
            //Change the chars stored in the playspace
            //Restore the previous tile to the environment
            charPlayArea[prevYLoc, prevXLoc] = charOriginalMapState[prevYLoc, prevXLoc];
            charPlayArea[YLoc, XLoc] = sprite;

            //Redraw Original Map
            //Use the GetLength method to select array dimension
            string newline = "";
            for (int g = 0; g < charOriginalMapState.GetLength(1); g++)
            {
                newline += charPlayArea[prevYLoc, g];
            }
            lstPlayArea.Items[prevYLoc] = newline;

            //Rebuild listbox to display the active map
            //Place string into the listbox
            newline = "";
            for (int x = 0; x < charPlayArea.GetLength(1); x++)
            {
                newline += charPlayArea[YLoc, x].ToString();
            }
            lstPlayArea.Items[YLoc] = newline;
        }
    }
}