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

        SetLogic deck = new SetLogic();

        public List<PictureBox> pBoxes = new List<PictureBox>();
        public List<PictureBox> pBoxesSelect = new List<PictureBox>();
        public Bitmap[] DrawAreas = new Bitmap[12];

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


        private void btnGenerateDeck_Click(object sender, EventArgs e)
        {
            deck.generateDeck();
            Console.WriteLine("Done generating");
            btnDeal.Enabled = true;
            Console.WriteLine(deck.deckOfCards.Count());
            //foreach(Card c in deck.deckOfCards)
            //{
            //    c.showFeatures();
            //}
            deck.shuffleDeck();

        }

        public void noSetFound() {

            if (deck.deckOfCards.Count <= 12)
            {
                Console.WriteLine("No sets in last 12");
                endGame();
                return;
            }

            if (deck.cardsShown < 15)
            {
                deck.cardsShown += 3;
            }
            else
            {
                deck.moveToEnd();
            }
            //enable and make visible extra picture boxes
            for (int i = 0; i < deck.cardsShown; ++i)
            {
                pBoxes[i].Visible = true;
                pBoxesSelect[i].Visible = false;
                pBoxes[i].Enabled = true;
            }

            //inform player
            MessageBox.Show("No sets found, adding more cards");

            refreshCards();

            if (deck.checkForSets() == 0)
            {
                noSetFound();
            }
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            refreshCards();
            Threading sendCard = new Threading();
            string send = "";
            for (int i = 0; i < 12; ++i) {
                send += deck.deckOfCards[i].toString() + ",";
            }

            sendCard.WriteToClient(send);
            
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

        private void refreshCards()
        {
            CardDisplay cardDisplay = new CardDisplay();
            //draw cards on table
            for (int i = 0; i < deck.cardsShown; ++i)
            {
                Graphics g = Graphics.FromImage(pBoxes[i].Image);
                cardDisplay.drawCard(deck.deckOfCards[i], g);

                g.Dispose();
                pBoxes[i].Refresh();
            }
            //make sure all unneeded boxes are invisible
            for (int i = deck.cardsShown; i < 21; ++i) {
                pBoxes[i].Visible = false;
            }

        }


        private void selectImage(int position)
        {
            if (selectedCards.Count >= 3 && !pBoxesSelect[position - 1].Visible)
            {
                MessageBox.Show("Too many cards selected. Select at most 3.");
                return;
            }

            if (!pBoxesSelect[position - 1].Visible && numCardsSelected < 3)
            {
                pBoxesSelect[position - 1].Visible = true;
                selectedCards.Add(deck.deckOfCards[position - 1]);
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
                selectedCards.Remove(deck.deckOfCards[position - 1]);
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

                    if (deck.deckOfCards.Count  == 12)
                    {
                        Console.WriteLine("We're in the endgame now");
                    }

                    if (deck.deckOfCards.Count == 0)
                    {
                        endGame();
                    }
                    else
                    {
                        //remove cards from table
                        if (deck.cardsShown > 12 || deck.deckOfCards.Count <= deck.cardsShown)
                        {
                            //make extra row of cards not visible
                            for (int i = deck.cardsShown - 3; i < deck.cardsShown; ++i)
                            {
                                pBoxes[i].Visible = false;
                                pBoxesSelect[i].Visible = false;
                            }
                            deck.cardsShown -= 3;

                        }

                        //remove the cards in the set taken 
                        deck.deckOfCards.RemoveAll((Card c) => {
                            if (selectedCards.Contains(c))
                            {
                                Console.WriteLine("Removed: {0}",c.toString());
                                c.showFeatures();
                                return true;
                            }
                            return false;
                        });

                        selectedCards.Clear();

                        //update cards on table
                        refreshCards();

                        for (int i = 0; i < deck.cardsShown; ++i)
                        {
                            pBoxesSelect[i].Visible = false;
                        }

                        //if no sets exist on the table 
                        if (deck.checkForSets() == 0)
                        {
                            noSetFound();
                        }

                        numCardsSelected = 0;
                        btnSelectSet.Enabled = false;

                        txtCheat.Clear();
                        txtCheat.Text = deck.cheat();
                        }
                }
                else
                    MessageBox.Show("Not a set");
            }
            else
                MessageBox.Show("Not enough cards selected");
        }

        public bool isEndGame = false;

        private void endGame()
        {
            if (deck.serverSets > deck.clientSets)
            {
                MessageBox.Show("Server Wins");
            }
            else if (deck.serverSets < deck.clientSets)
            {
                MessageBox.Show("Client Wins");
            }
            else
            {
                MessageBox.Show("Tie");
            }
        }

        private void btnCheat_Click(object sender, EventArgs e)
        {
            deck.cheat();
            Console.WriteLine("-----");
        }

        private void btnPosSelected_Click(object sender, EventArgs e)
        {
            Console.WriteLine(selectedCards[0].ToString());
            Console.WriteLine(selectedCards[1].ToString());
            Console.WriteLine(selectedCards[2].ToString());
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
