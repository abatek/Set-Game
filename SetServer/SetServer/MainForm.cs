﻿using System;
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
        public List<PictureBox> pBoxesSelect = new List<PictureBox>();
        public Bitmap[] DrawAreas = new Bitmap[12];
        public bool cardsAdded = false;


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

            for (int i = 0; i < 12; ++i)
            {
                pBoxes[i].Image = new Bitmap(pBoxes[i].Size.Width, pBoxes[i].Size.Height);
                pBoxes[i].Enabled = false;
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
            lblCurIndex.Text = "Current Card: 0";
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
            Console.WriteLine("Done generating");
            btnDeal.Enabled = true;

        }

        public void noSetFound() {
            MessageBox.Show("No sets found, add more cards");
            cardsAdded = true;
            cardsOnTable = 15;
            for (int i = 0; i < 15; ++i)
            {
                pBoxes[i].Image = new Bitmap(pBoxes[i].Size.Width, pBoxes[i].Size.Height);
                pBoxes[i].Enabled = true;
            }

            for (int i = 0; i < 3; ++i)
            {
                deck.dealtCards.Add(deck.deckOfCards[deck.curIndexInDeck]);
                deck.curIndexInDeck++;
            }

            refreshCards();
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            deck.dealCards();
            refreshCards();
            if (deck.checkForSets() == 0)
            {
                noSetFound();
            }
            else
            {
                for (int i = 0; i < 12; ++i)
                {
                    pBoxes[i].Enabled = true;
                    btnCheckForSets.Enabled = true;
                    btnCheat.Enabled = true;
                }
            }
            txtCheat.Text = deck.cheat();
            lblCurIndex.Text = "Current Card: " + deck.curIndexInDeck.ToString();
        }

        private void btnCheckForSets_Click(object sender, EventArgs e)
        {
            Console.WriteLine(deck.checkForSets().ToString());
        }

        private int cardsOnTable = 12;

        private void refreshCards()
        {
            CardDisplay cardDisplay = new CardDisplay();
            for (int i = 0; i < cardsOnTable; ++i)
            {

                Graphics g = Graphics.FromImage(pBoxes[i].Image);
                cardDisplay.drawCard(deck.dealtCards[i], g);
                g.Dispose();
                pBoxes[i].Refresh();
            }

        }

        #region hide commented code
        /*
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
        #endregion

        List<Card> selectedCards = new List<Card>();
        private int numCardsSelected = 0;
        private int[] selectedPositions = new int[3];

        private void selectImage(int position)
        {
            
            
            if (!pBoxesSelect[position - 1].Visible && numCardsSelected < 3)
            {
                pBoxesSelect[position - 1].Visible = true;
                selectedCards.Add(deck.dealtCards[position - 1]);
                selectedPositions[numCardsSelected] = position - 1;
                numCardsSelected++;
                if (numCardsSelected == 3)
                    btnSelectSet.Enabled = true;
            }
            else if (numCardsSelected >= 3 && !pBoxesSelect[position - 1].Visible)
            {
                MessageBox.Show("Too many cards selected. Select at most 3.");
            }
            else
            {
                pBoxesSelect[position - 1].Visible = false;
                selectedCards.Remove(deck.dealtCards[position - 1]);
                numCardsSelected--;
                btnSelectSet.Enabled = false;
            }
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

        private void btnSelectSet_Click(object sender, EventArgs e)
        {
            lblCurIndex.Text = "Current Card: " + deck.curIndexInDeck.ToString();
            if (selectedCards.Count == 3)
            {
                if (deck.isSet(selectedCards[0], selectedCards[1], selectedCards[2]))
                {
                    //need to shift cards back into place once certain ones are selected
                    MessageBox.Show("Set found");
                    cardsOnTable = 12;
                    for (int i = 12; i < 15; ++i)
                    {
                        pBoxes[i].Image = null;
                        pBoxes[i].Enabled = false;
                    }
                    deck.dealtCards[selectedPositions[0]] = deck.deckOfCards[deck.curIndexInDeck];
                    deck.curIndexInDeck++;
                    deck.dealtCards[selectedPositions[1]] = deck.deckOfCards[deck.curIndexInDeck];
                    deck.curIndexInDeck++;
                    deck.dealtCards[selectedPositions[2]] = deck.deckOfCards[deck.curIndexInDeck];
                    deck.curIndexInDeck++;
                    refreshCards();

                    if (deck.checkForSets() == 0)
                    {
                        noSetFound();
                    }

                    numCardsSelected = 0;
                    selectedCards.Clear();
                    btnSelectSet.Enabled = false;

                    txtCheat.Clear();
                    txtCheat.Text = deck.cheat();

                    for (int i = 0; i < cardsOnTable; ++i)
                    {
                        pBoxesSelect[i].Visible = false;
                    }

                    

                }

                else
                    MessageBox.Show("Not a set");
            }
            else
                MessageBox.Show("Not enough cards selected");
        }

        private void btnCheat_Click(object sender, EventArgs e)
        {
            deck.cheat();
            Console.WriteLine("-----");
        }

        private void btnPosSelected_Click(object sender, EventArgs e)
        {
            Console.WriteLine(selectedPositions[0].ToString());
            Console.WriteLine(selectedPositions[1].ToString());
            Console.WriteLine(selectedPositions[2].ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
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
