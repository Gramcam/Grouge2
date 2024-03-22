namespace GrogueTheSecondOne
{
    public partial class Form1 : Form
    {
        List<List<char>> playSpaceList = new List<List<char>>();
        List<List<char>> OrigplaySpaceList = new List<List<char>>();

        private mobEnemy playspaceEnemy;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Level Data From Textfile

            LoadLevel("Level_1.txt");


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
            OrigplaySpaceList = playSpaceList;

        }

        private void btnTestChange_Click(object sender, EventArgs e)
        {
            int x, y;
            Random rnd = new Random();

            x = rnd.Next(1, playSpaceList[1].Count-1);
            y = rnd.Next(1, playSpaceList.Count - 1);

            playspaceEnemy = new mobEnemy('N', y, x);

            playSpaceList[y][x] = playspaceEnemy.Sprite;

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
    }
}