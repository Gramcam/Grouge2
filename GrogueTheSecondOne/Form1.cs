using System.Xml;

namespace GrogueTheSecondOne
{
    public partial class Form1 : Form
    {
        //Map Fields
        private const int PLAYSPACEROWS = 12;
        private const int PLAYSPACECOL = 49;
        private char[,] charOriginalMapState = new char[PLAYSPACEROWS, PLAYSPACECOL];
        private char[,] charPlayArea = new char[PLAYSPACEROWS, PLAYSPACECOL];

        private Random rnd = new Random();

        //List to store EnemyMobs
        List<mobEnemy> enemyList = new List<mobEnemy>();
        private mobEnemy playSpaceEnemy;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Level Data From Textfile
            LoadLevel("Level_1.txt");
            for (int i = 0; i < 10; i++)
            {
                int x, y;
                
                //Choose random location that isnt populated
                do
                {
                    x = rnd.Next(1, PLAYSPACECOL - 1);
                    y = rnd.Next(1, PLAYSPACEROWS - 1);

                } while (charPlayArea[y, x] != '.');

                playSpaceEnemy = new mobEnemy(y, x);

                enemyList.Add(playSpaceEnemy);
            }

            foreach (mobEnemy mob in enemyList)
                UpdateEnemyRows(mob);
        }

        private void LoadLevel(string lvl)
        {
            //Load from textfile
            try
            {
                StreamReader inputFile = File.OpenText(lvl);

                while (!inputFile.EndOfStream)
                {
                    lstPlayArea.Items.Add(inputFile.ReadLine());
                }
                inputFile.Close();
            }
            catch
            {
                MessageBox.Show("Failed to retrieve level data!");
            }

            //Nested loop to load level into two 2d arrays
            for (int i = 0; i < lstPlayArea.Items.Count; i++)
            {
                string line = lstPlayArea.Items[i].ToString();
                List<char> readChars = new List<char>();
                for (int j = 0; j < line.Length; j++)
                {
                    charOriginalMapState[i, j] = line[j];
                    charPlayArea[i, j] = line[j];
                }
            }

        }

        private void btnTestChange_Click(object sender, EventArgs e)
        {
            foreach (mobEnemy M in enemyList)
            {
                M.MobMoveArrManip(charPlayArea);
                UpdateEnemyRows(M);
            }
        }

        private void UpdateEnemyRows(mobEnemy Enemy)
        {
           

            //Change the chars stored in the playspace
            //Restore the previous tile to the environment
            charPlayArea[Enemy.YLoc, Enemy.Xloc] = Enemy.Sprite;
            charPlayArea[Enemy.prevYLoc, Enemy.prevXloc] = charOriginalMapState[Enemy.prevYLoc, Enemy.prevXloc];

            //Redraw Original Map
            //Use the GetLength method to select array dimension
            string newline = "";
            for (int g = 0; g < charOriginalMapState.GetLength(1); g++)
            {
                newline += charPlayArea[Enemy.prevYLoc, g];
            }
            lstPlayArea.Items[Enemy.prevYLoc] = newline;

            //Rebuild listbox to display the active map
            //Place string into the listbox
            newline = "";
            for (int x = 0; x < charPlayArea.GetLength(1); x++)
            {
                newline += charPlayArea[Enemy.YLoc, x].ToString();
            }
            lstPlayArea.Items[Enemy.YLoc] = newline;
        }
    }
}