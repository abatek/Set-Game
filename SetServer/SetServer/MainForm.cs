using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetServer
{
    public partial class MainForm : Form
    {
        private Threading server = new Threading();

        public List<PictureBox> pBoxes = new List<PictureBox>();
        public Bitmap[] DrawAreas = new Bitmap[12];



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

            for (int i = 0; i < 12; ++i)
            {
                pBoxes[i].Image = new Bitmap(pBoxes[i].Size.Width, pBoxes[i].Size.Height);
            }
        }

        void AddToListBox_AddItem(string strItem)
        {
            lbReceive.Items.Add(strItem);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; ++i)
            {
                Graphics g = Graphics.FromImage(pBoxes[i].Image);
                g.Clear(Color.White);
                g.Dispose();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            server.WriteToClient(txtSend.Text);
        }

        private void txtConnect_Click(object sender, EventArgs e)
        {

            server.Connect();
        }

        SetLogic deck = new SetLogic();

        private void btnGenerateDeck_Click(object sender, EventArgs e)
        {
            deck.generateDeck();
            deck.shuffleDeck();
            
        }

        #region select picturebox






        #endregion

        private void btnStartGame_Click(object sender, EventArgs e)
        {

        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            deck.dealCards();
            refreshCards();
        }

        private void btnCheckForSets_Click(object sender, EventArgs e)
        {
            deck.checkForSets();
        }

        private void refreshCards()
        {
            CardDisplay cardDisplay = new CardDisplay();
            for (int i = 0; i < 12; ++i)
            {

                Graphics g = Graphics.FromImage(pBoxes[i].Image);
                cardDisplay.drawCard(deck.dealtCards[i], g);
                g.Dispose();
                pBoxes[i].Refresh();
            }
            
        }/*
        private void pb1_1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb1_1 = new CardDisplay();
            pb1_1.drawCard(g, deck.deckOfCards[0]);
        }

        private void pb2_1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb2_1 = new CardDisplay();
            pb2_1.drawCard(g, deck.deckOfCards[1]);
        }

        private void pb3_1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb3_1 = new CardDisplay();
            pb3_1.drawCard(g, deck.deckOfCards[2]);
        }

        private void pb1_2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb1_2 = new CardDisplay();
            pb1_2.drawCard(g, deck.deckOfCards[3]);
        }

        private void pb2_2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb2_2 = new CardDisplay();
            pb2_2.drawCard(g, deck.deckOfCards[4]);
        }

        private void pb3_2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb3_2 = new CardDisplay();
            pb3_2.drawCard(g, deck.deckOfCards[5]);
        }

        private void pb1_3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb1_3 = new CardDisplay();
            pb1_3.drawCard(g, deck.deckOfCards[6]);
        }

        private void pb2_3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb2_3 = new CardDisplay();
            pb2_3.drawCard(g, deck.deckOfCards[7]);
        }

        private void pb3_3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb3_3 = new CardDisplay();
            pb3_3.drawCard(g, deck.deckOfCards[8]);
        }

        private void pb1_4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb1_4 = new CardDisplay();
            pb1_4.drawCard(g, deck.deckOfCards[9]);
        }

        private void pb2_4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb2_4 = new CardDisplay();
            pb2_4.drawCard(g, deck.deckOfCards[10]);
        }

        private void pb3_4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CardDisplay pb3_4 = new CardDisplay();
            pb3_4.drawCard(g, deck.deckOfCards[11]);
        }*/
    }

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


}
