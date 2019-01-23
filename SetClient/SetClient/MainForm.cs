using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SetClient;


namespace SetClient
{
    public partial class MainForm : Form
    {
        private Threading client = new Threading();
        private SetLogic logic = new SetLogic();
        public List<PictureBox> pBoxes = new List<PictureBox>();
        public List<PictureBox> pBoxesSelect = new List<PictureBox>();
        public int cardsDealt = 12;
        public List<int> cardPosition = new List<int>();

        private int numCardsSelected = 0;
        private List<Card> selectedCards = new List<Card>();

        public MainForm()
        {
            InitializeComponent();
            AddToListBox.AddItem += AddToListBox_AddItem;
            pBoxes.Add(pb1_1);
            pBoxes.Add(pb2_1);
            pBoxes.Add(pb3_1);
            pBoxes.Add(pb1_2);
            pBoxes.Add(pb2_2);
            pBoxes.Add(pb3_2);
            pBoxes.Add(pb1_3);
            pBoxes.Add(pb2_3);
            pBoxes.Add(pb3_3);
            pBoxes.Add(pb1_4);
            pBoxes.Add(pb2_4);
            pBoxes.Add(pb3_4);
            pBoxes.Add(pb1_5);
            pBoxes.Add(pb2_5);
            pBoxes.Add(pb3_5);
            pBoxes.Add(pb1_6);
            pBoxes.Add(pb2_6);
            pBoxes.Add(pb3_6);
            pBoxes.Add(pb1_7);
            pBoxes.Add(pb2_7);
            pBoxes.Add(pb3_7);

            pBoxesSelect.Add(pb1_1_select);
            pBoxesSelect.Add(pb_2_1_select);
            pBoxesSelect.Add(pb3_1_select);
            pBoxesSelect.Add(pb1_2_select);
            pBoxesSelect.Add(pb2_2_select);
            pBoxesSelect.Add(pb3_2_select);
            pBoxesSelect.Add(pb1_3_select);
            pBoxesSelect.Add(pb2_3_select);
            pBoxesSelect.Add(pb3_3_select);
            pBoxesSelect.Add(pb1_4_select);
            pBoxesSelect.Add(pb2_4_select);
            pBoxesSelect.Add(pb3_4_select);
            pBoxesSelect.Add(pb1_5_select);
            pBoxesSelect.Add(pb2_5_select);
            pBoxesSelect.Add(pb3_5_select);
            pBoxesSelect.Add(pb1_6_select);
            pBoxesSelect.Add(pb2_6_select);
            pBoxesSelect.Add(pb3_6_select);
            pBoxesSelect.Add(pb1_7_select);
            pBoxesSelect.Add(pb2_7_select);
            pBoxesSelect.Add(pb3_7_select);

            for (int i = 0; i < 21; ++i)
            {
                pBoxes[i].Image = new Bitmap(pBoxes[i].Size.Width, pBoxes[i].Size.Height);
                pBoxes[i].Enabled = false;
            }

        }

        void AddToListBox_AddItem(string strItem)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 21; ++i)
            {
                Graphics g = Graphics.FromImage(pBoxes[i].Image);
                g.Clear(Color.White);
                g.Dispose();
            }
            for (int i = 12; i < 21; ++i)
            {
                pBoxes[i].Visible = false;
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Sent cards - see card class for number value list
            Card a = new Card(0, 0, 2, 0);
            Card b = new Card(1, 2, 1, 0);
            Card c = new Card(2, 1, 0, 0);

            string str = a.toString() + "," + b.toString() + "," + c.toString();
            client.WriteToServer(str);
        }

        private void selectImage(int position)
        {
            if (selectedCards.Count >= 3 && !pBoxesSelect[position - 1].Visible)
            {
                lblStatus.Text = ("Too many cards selected. Select at most 3.");
                return;
            }

            if (!pBoxesSelect[position - 1].Visible && numCardsSelected < 3)
            {
                pBoxesSelect[position - 1].Visible = true;
                selectedCards.Add(client.dealtCards[position - 1]);
                cardPosition.Add(position - 1);
                //deck.deckOfCards[position - 1].showFeatures();
                if (selectedCards.Count == 3)
                {
                    btnSelectSet.Enabled = true;
                    btnSelectSet.Select();
                }
            }
            else
            {
                pBoxesSelect[position - 1].Visible = false;
                selectedCards.Remove(client.dealtCards[position - 1]);
                cardPosition.Remove(position - 1);
                btnSelectSet.Enabled = false;
            }
        }

        private void txtConnect_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("Attempting to load");
            lblStatus.Text = "Attempting to connect";
            client.Connect(txtIP.Text);
            btnConnect.Enabled = false;
            txtIP.Enabled = false;
            lblStatus.Text = "Connected. Waiting for game to begin.";
        }

        public void refreshCards()
        {
            CardDisplay cardDisplay = new CardDisplay();
            //draw cards on table
            for (int i = 0; i < client.dealtCards.Count; ++i)
            {
                Graphics g = Graphics.FromImage(pBoxes[i].Image);
                cardDisplay.drawCard(client.dealtCards[i], g);
                g.Dispose();
                pBoxes[i].Refresh();
            }
            //make sure all unneeded boxes are invisible
            for (int i = client.dealtCards.Count; i < 21; ++i)
            {
                pBoxes[i].Visible = false;
            }

        }

        //use AddToListBox.UpdateListBox(string) to add to listbox anywhere from in code

        //SECTION ALLOWS INTERACTION WITH LISTBOX
        public delegate void AddToListBoxDelegate(string strAdd);

        public static class AddToListBox
        {
            public static Form MainForm;

            public static event AddToListBoxDelegate AddItem;

            public static void UpdateListBox(string strItem)
            {
                ThreadItemAdd(strItem);
            }

            public static void ThreadItemAdd(string strItem)
            {
                if (MainForm != null && MainForm.InvokeRequired)
                    MainForm.Invoke(new AddToListBoxDelegate(ThreadItemAdd), new object[] { strItem });
                else
                    AddItem(strItem);
            }
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {

        }

        private void cardUpdate_Tick(object sender, EventArgs e)
        {
            refreshCards();
            for (int i = 0; i < cardsDealt; i++) {
                pBoxes[i].Enabled = true;
            }
            lblServerSets.Text = client.threadServerScore.ToString();
            lblClientSets.Text = client.threadClientScore.ToString();
        }

        private void pb1_1_Click(object sender, EventArgs e)
        {
            selectImage(1);
        }

        private void pb2_1_Click(object sender, EventArgs e)
        {
            selectImage(2);
        }

        private void pb3_1_Click(object sender, EventArgs e)
        {
            selectImage(3);
        }

        private void pb1_2_Click(object sender, EventArgs e)
        {
            selectImage(4);
        }

        private void pb2_2_Click(object sender, EventArgs e)
        {
            selectImage(5);
        }

        private void pb3_2_Click(object sender, EventArgs e)
        {
            selectImage(6);
        }

        private void pb1_3_Click(object sender, EventArgs e)
        {
            selectImage(7);
        }

        private void pb2_3_Click(object sender, EventArgs e)
        {
            selectImage(8);
        }

        private void pb3_3_Click(object sender, EventArgs e)
        {
            selectImage(9);
        }

        private void pb1_4_Click(object sender, EventArgs e)
        {
            selectImage(10);
        }

        private void pb2_4_Click(object sender, EventArgs e)
        {
            selectImage(11);
        }

        private void pb3_4_Click(object sender, EventArgs e)
        {
            selectImage(12);
        }

        private void pb1_5_Click(object sender, EventArgs e)
        {
            selectImage(13);
        }

        private void pb2_5_Click(object sender, EventArgs e)
        {
            selectImage(14);
        }

        private void pb3_5_Click(object sender, EventArgs e)
        {
            selectImage(15);
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            lblStatus.Text = "Waiting to connect.";
        }

        private void gameStarted_Tick(object sender, EventArgs e)
        {
            if (client.dealtCards.Count > 0) {
                refreshCards();
                lblStatus.Text = "Game started!";
                cardUpdate.Enabled = true;
                gameStarted.Enabled = false;
            }
        }

        public void resetSelected() {
            selectedCards.Clear();
            for (int i = 0; i < 12; i++) {
                pBoxesSelect[i].Visible = false;
            }
        }

        private void btnSelectSet_Click(object sender, EventArgs e)
        {
            if (selectedCards.Count == 3)
            {
                client.WriteToServer("*All 3 cards selected");
                if (logic.isSet(selectedCards[0], selectedCards[1], selectedCards[2]))
                {
                    client.WriteToServer("*Set found");
                    string send = "";

                    for (int i = 0; i < 3; i++)
                    {
                        send += (cardPosition[i]).ToString() + " ";
                    }

                    send += "\n";

                    client.WriteToServer(send);
                    resetSelected();
                }
                else
                {
                    lblStatus.Text = "Not a set!";
                }
            }
            else
            {
                lblStatus.Text = "Not enough cards selected!";
            }
        }

        private void isEndGame_Tick(object sender, EventArgs e)
        {
            if (client.endGameString != null) {
                MessageBox.Show(client.endGameString);
            }
        }
    }
}

