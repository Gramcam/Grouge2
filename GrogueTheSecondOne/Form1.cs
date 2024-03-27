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
            for (x = 0; x < playSpaceList[y].Count; x++)
            {
                char readChar = playSpaceList[y][x];
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

            //Assign to nested lists to create a playgrid //CHANGE THIS TO A FOR LOOP!!!!!
            for (int i = 0; i<lstPlayArea.Items.Count; i++)
            {
                string line = lstPlayArea.Items[i].ToString();
                List<char> readChars = new List<char>();
                for (int j = 0; j < line.Length; j++)
                {
                    readChars.Add(line[j]);
                }
                OrigplaySpaceList.Add(readChars);
                playSpaceList.Add(readChars);

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
            char readchar;
            string newline = "";
            playSpaceList[Enemy.YLoc][Enemy.Xloc] = Enemy.Sprite;
            playSpaceList[Enemy.prevYLoc][Enemy.prevXloc] = OrigplaySpaceList[Enemy.prevYLoc][Enemy.prevXloc];

            //Redraw Original Map
            for (int g = 0; g < OrigplaySpaceList[Enemy.YLoc].Count; g++)
            {
                newline += OrigplaySpaceList[Enemy.YLoc][g];
            }
            lstPlayArea.Items[Enemy.YLoc] = newline;
                lstPlayArea.Items[Enemy.prevYLoc] = OrigplaySpaceList[Enemy.prevYLoc];

            int x, y;
            x = Enemy.Xloc;
            y = Enemy.YLoc;

            newline = "";
            //Get the Chars from the playspace, rebuild into a new string, 
            //Place string into the listbox
            for (x = 0; x < playSpaceList[y].Count; x++)
            {
                char readChar = playSpaceList[y][x];
                newline += readChar;
            }

            lstPlayArea.Items[y] = newline;

        }
    }
}