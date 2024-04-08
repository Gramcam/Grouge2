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
        private bool gamerun;

        private Random rnd = new Random();

        //List to store EnemyMobs
        List<mobEnemy> enemyList = new List<mobEnemy>();
        List<Treasure> treasureList = new List<Treasure>();
        private mobEnemy playSpaceEnemy;
        private Treasure mapTreasure;

        private Player playerCharacter;

        public enum asciiTiles
        {
            empty = '.',
            wall = '#',
            alivePlayer = '@',
            aliveEnemy = 'N',
            treasure = '$'
        }
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GameStartHandling();

        }

        private void GameStartHandling()
        {
            lstPlayArea.Items.Clear();
            treasureList.Clear();
            enemyList.Clear();

            LoadLevel("Level_1.txt");
            SpawnEnemies(10);
            playerCharacter = new Player(10, 46);
            SpawnTreasure(8);
            UpdateEnemyRows(playerCharacter.Sprite, playerCharacter.YLoc, playerCharacter.XLoc, playerCharacter.prevYLoc, playerCharacter.prevXLoc);
            gamerun = true;
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
            charOriginalMapState = new char[MapYCount, mapXCount];
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
        private void SpawnTreasure(int quantity)
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

                mapTreasure = new Treasure(y, x);

                treasureList.Add(mapTreasure);
            }
            foreach (Treasure T in treasureList)
            {
                PopulateTreasure(T.Sprite, T.YLoc, T.XLoc);
                DrawChangedMap(T.Sprite, T.YLoc, T.XLoc);
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

        private void PopulateTreasure(char sprite, int YLoc, int XLoc)
        {
            charPlayArea[YLoc, XLoc] = sprite;
        }
        private void ChangeCharArrays(char sprite, int YLoc, int XLoc, int prevYLoc, int prevXLoc)
        {
            //Change the chars stored in the playspace
            //Restore the previous tile to the environment
            charPlayArea[prevYLoc, prevXLoc] = charOriginalMapState[prevYLoc, prevXLoc];
            charPlayArea[YLoc, XLoc] = sprite;

        }
        private void RedrawOriginalRow(char sprite, int YLoc, int XLoc, int prevYLoc, int prevXLoc)
        {
            string newline = "";

            //Redraw Original Map
            //Use the GetLength method to select array dimension
            for (int g = 0; g < charOriginalMapState.GetLength(1); g++)
            {
                newline += charPlayArea[prevYLoc, g];
            }
            lstPlayArea.Items[prevYLoc] = newline;

        }
        private void DrawChangedMap(char sprite, int YLoc, int XLoc)
        {
            charPlayArea[YLoc, XLoc] = sprite;
            string newline = "";
            for (int x = 0; x < charPlayArea.GetLength(1); x++)
            {
                newline += charPlayArea[YLoc, x].ToString();
            }
            lstPlayArea.Items[YLoc] = newline;
        }

        private void UpdateEnemyRows(char sprite, int YLoc, int XLoc, int prevYLoc, int prevXLoc)
        {
            ChangeCharArrays(sprite, YLoc, XLoc, prevYLoc, prevXLoc);
            RedrawOriginalRow(sprite, YLoc, XLoc, prevYLoc, prevXLoc);
            DrawChangedMap(sprite, YLoc, XLoc);
        }

        private void TurnEndBehaviour()
        {
            UpdateEnemyRows(playerCharacter.Sprite, playerCharacter.YLoc, playerCharacter.XLoc, playerCharacter.prevYLoc, playerCharacter.prevXLoc);
            foreach (Treasure T in treasureList)
            {
                PopulateTreasure(T.Sprite, T.YLoc, T.XLoc);
                DrawChangedMap(T.Sprite, T.YLoc, T.XLoc);
            }
            List<mobEnemy> enemiesToRemove = new List<mobEnemy>();
            List<Treasure> treasureToRemove = new List<Treasure>();
            foreach (Treasure T in treasureList)
            {
                T.TreasureCollect(playerCharacter, treasureList);
            }
            foreach (Treasure T in treasureList)
            {
                if (T.nonActive)
                {
                    // Add the enemy that collided with the player to the removal list
                    treasureToRemove.Add(T);
                }

            }
            foreach (Treasure T in treasureToRemove)
            {
                treasureList.Remove(T);

                DrawChangedMap('@', T.YLoc, T.XLoc);

            }
            foreach (mobEnemy N in enemyList)
            {
                N.MobMoveArrManip(charPlayArea, playerCharacter);
                UpdateEnemyRows(N.Sprite, N.YLoc, N.XLoc, N.prevYLoc, N.prevXLoc);
            }
            foreach (mobEnemy N in enemyList)
            {
                N.AttackPlayer(playerCharacter, enemyList);
                if (N.removeState)
                {
                    // Add the enemy that collided with the player to the removal list
                    enemiesToRemove.Add(N);
                }
            }
            foreach (mobEnemy N in enemiesToRemove)
            {
                enemyList.Remove(N);
                UpdateEnemyRows(N.Sprite, N.YLoc, N.XLoc, N.prevYLoc, N.prevXLoc);
            }
            if (playerCharacter.playerHealth <= 0 && treasureList.Count > 0)
            {
                MessageBox.Show($"You got {playerCharacter.playerTreasure} treasures!");
                gamerun = false;
            }
            if (treasureList.Count == 0)
            {
                MessageBox.Show($"You got {playerCharacter.playerTreasure} treasures! Thats all of them!");
                gamerun = false;

            }
            lblHealth.Text = $"HP: {playerCharacter.playerHealth}";
            lblTreasures.Text = $"Treasure: {playerCharacter.playerTreasure}";

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gamerun)
            {
                if (e.KeyCode == Keys.NumPad5)
                    playerCharacter.playerMove(charPlayArea, 0);
                if (e.KeyCode == Keys.NumPad7)
                    playerCharacter.playerMove(charPlayArea, 1);
                if (e.KeyCode == Keys.NumPad8)
                    playerCharacter.playerMove(charPlayArea, 2);
                if (e.KeyCode == Keys.NumPad9)
                    playerCharacter.playerMove(charPlayArea, 3);
                if (e.KeyCode == Keys.NumPad4)
                    playerCharacter.playerMove(charPlayArea, 4);
                if (e.KeyCode == Keys.NumPad6)
                    playerCharacter.playerMove(charPlayArea, 5);
                if (e.KeyCode == Keys.NumPad1)
                    playerCharacter.playerMove(charPlayArea, 6);
                if (e.KeyCode == Keys.NumPad2)
                    playerCharacter.playerMove(charPlayArea, 7);
                if (e.KeyCode == Keys.NumPad3)
                    playerCharacter.playerMove(charPlayArea, 8);
                TurnEndBehaviour();
                lstPlayArea.ClearSelected();
            }
            else
                GameStartHandling();
        }
    }
}