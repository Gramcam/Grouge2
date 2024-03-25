using System.Xml;

namespace GrogueTheSecondOne
{
    public partial class Form1 : Form
    {
        //Lists to store the Playfield
        List<List<char>> playSpaceList = new List<List<char>>();
        //TODO create a loop to copy nested lists.
        List<List<char>> OrigplaySpaceList = new List<List<char>>();

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
                x = rnd.Next(1, playSpaceList[1].Count - 1);
                y = rnd.Next(1, playSpaceList.Count - 1);

            } while (playSpaceList[y][x] == 'N' || playSpaceList[y][x] == '#');

            playSpaceEnemy = new mobEnemy(y, x);

            playSpaceList[y][x] = playSpaceEnemy.Sprite;
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

            //Assign to nested lists to create a playgrid
            foreach (string line in lstPlayArea.Items)
            {
                //Store each char within each line in a list
                List<char> lineChars = new List<char>();
                for (int col = 0; col < line.Length; col++)
                {
                    char readChar = line.ToString()[col];
                    lineChars.Add(readChar);
                }
                //Add that row of chars to the PlaySpace list
                playSpaceList.Add(lineChars);

            }

            //TODO create a loop to copy the Lists
            OrigplaySpaceList = playSpaceList;

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