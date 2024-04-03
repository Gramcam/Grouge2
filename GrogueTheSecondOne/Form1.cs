using System.Xml;

namespace GrogueTheSecondOne
{
    public partial class Form1 : Form
    {
        //Map Fields
        const int PLAYSPACEROWS = 12;
        const int PLAYSPACECOL = 49;
        public char[,] charOriginalMapState = new char[PLAYSPACEROWS, PLAYSPACECOL];
        char[,] charPlayArea = new char[PLAYSPACEROWS, PLAYSPACECOL];

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

            int x, y;
            Random rnd = new Random();

            //Choose random location that isnt populated
            do
            {
                x = rnd.Next(1, PLAYSPACECOL - 1);
                y = rnd.Next(1, PLAYSPACEROWS - 1);

            } while (charPlayArea[y, x] == 'N' || charPlayArea[y, x] == '#');

            playSpaceEnemy = new mobEnemy(y, x);

            charPlayArea[y, x] = playSpaceEnemy.Sprite;
            enemyList.Add(playSpaceEnemy);

            string newline = "";
            //Get the Chars from the playspace, rebuild into a new string, 
            //Place string into the listbox
            for (x = 0; x < PLAYSPACECOL; x++)
            {
                char readChar = charPlayArea[y, x];
                newline += readChar;
            }

            lstPlayArea.Items[y] = newline;
        }

        void LoadLevel(string lvl)
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
                M.moveUp(M);
                UpdateEnemyRows(M);
            }
        }

        void UpdateEnemyRows(mobEnemy Enemy)
        {
            string newline = "";

            //Change the chars stored in the playspace
            //Restore the previous tile to the environment
            charPlayArea[Enemy.YLoc, Enemy.Xloc] = Enemy.Sprite;
            charPlayArea[Enemy.prevYLoc, Enemy.prevXloc] = charOriginalMapState[Enemy.prevYLoc, Enemy.prevXloc];

            //Redraw Original Map
            for (int g = 0; g < charOriginalMapState.GetLength(1); g++)
            {
                newline += charOriginalMapState[Enemy.prevYLoc, g];
            }
            lstPlayArea.Items[Enemy.prevYLoc] = newline;

            newline = "";
            //Rebuild listbox to display the active map
            //Place string into the listbox
            for (int x = 0; x < charPlayArea.GetLength(1); x++)
            {
                newline += charPlayArea[Enemy.YLoc, x].ToString();
            }
            lstPlayArea.Items[Enemy.YLoc] = newline;
        }
    }
}