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
            lbReceive.Items.Add(strItem);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 21; ++i)
            {
                Graphics g = Graphics.FromImage(pBoxes[i].Image);
                g.Clear(Color.White);
                g.Dispose();
            }
            for (int i = 12; i < 21; ++i) {
                pBoxes[i].Visible = false;
            }

            lblCurIndex.Text = "Current Card: " + deck.curIndexInDeck.ToString();
            
            

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
            Console.WriteLine(deck.deckOfCards.Count());

        }

        public void noSetFound() {
            
            cardsAdded = true;
            cardsOnTable = cardsOnTable+3;
            for (int i = 0; i < cardsOnTable; ++i)
            {
                pBoxes[i].Visible = true;
                pBoxesSelect[i].Visible = false;
                pBoxes[i].Enabled = true;
            }

            MessageBox.Show("No sets found, add more cards");

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
                while(deck.checkForSets() == 0)
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
            if (deck.curIndexInDeck == 72) {
                cardsOnTable = 9;
            }
            if (deck.curIndexInDeck == 75)
            {
                cardsOnTable = 6;
            }
            if (deck.curIndexInDeck == 78)
            {
                cardsOnTable = 3;
            }
            if (deck.curIndexInDeck == 81)
            {
                //implement game over
            }


            if (selectedCards.Count == 3)
            {
                if (deck.isSet(selectedCards[0], selectedCards[1], selectedCards[2]))
                {
                    if (cardsAdded)
                    {
                        deck.dealtCards[selectedPositions[0]] = null;
                        deck.dealtCards[selectedPositions[1]] = null;
                        deck.dealtCards[selectedPositions[2]] = null;

                        
                        deck.dealtCards.Remove(null);
                        deck.dealtCards.Remove(null);
                        deck.dealtCards.Remove(null);

                        for (int i = cardsOnTable - 3; i < cardsOnTable; ++i)
                        {
                            pBoxes[i].Visible = false;
                            pBoxesSelect[i].Visible = false;
                        }



                        cardsOnTable -= 3;
                        if (cardsOnTable == 12) {
                            cardsAdded = false;
                        }
                    }
                    else
                    {
                        deck.dealtCards[selectedPositions[0]] = deck.deckOfCards[deck.curIndexInDeck];
                        deck.curIndexInDeck++;
                        deck.dealtCards[selectedPositions[1]] = deck.deckOfCards[deck.curIndexInDeck];
                        deck.curIndexInDeck++;
                        deck.dealtCards[selectedPositions[2]] = deck.deckOfCards[deck.curIndexInDeck];
                        deck.curIndexInDeck++;
                    }

                    refreshCards();

                    while (deck.checkForSets() == 0)
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
            deck.curIndexInDeck = 69;

        }

        private void pb1_6_Click(object sender, EventArgs e)
        {
            selectImage(16);
        }

        private void pb_2_6_Click(object sender, EventArgs e)
        {
            selectImage(17);
        }

        private void pb_2_7_Click(object sender, EventArgs e)
        {
            selectImage(18);
        }

        private void pb1_7_Click(object sender, EventArgs e)
        {
            selectImage(19);
        }

        private void pb2_7_Click(object sender, EventArgs e)
        {
            selectImage(20);
        }

        private void pb3_7_Click(object sender, EventArgs e)
        {
            selectImage(21);
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
