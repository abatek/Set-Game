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
            //cards have (are going to be) added
            cardsAdded = true;
            deck.cardsOnTable = deck.cardsOnTable + 3;

            //enable and make visible extra picture boxes
            for (int i = 0; i < deck.cardsOnTable; ++i)
            {
                pBoxes[i].Visible = true;
                pBoxesSelect[i].Visible = false;
                pBoxes[i].Enabled = true;
            }

            //inform player
            MessageBox.Show("No sets found, adding more cards");

            //add extra cards to dealt cards list
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
                for (int i = 0; i < deck.cardsOnTable; ++i)
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
            for (int i = 0; i < deck.cardsOnTable; ++i)
            {

                Graphics g = Graphics.FromImage(pBoxes[i].Image);
                cardDisplay.drawCard(deck.dealtCards[i], g);
                g.Dispose();
                pBoxes[i].Refresh();
            }
            //make sure all boxes are visible
            for (int i = deck.cardsOnTable; i < 21; ++i) {
                pBoxes[i].Visible = false;
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
                {
                    btnSelectSet.Enabled = true;
                    btnSelectSet.Select();
                }
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

            if (deck.curIndexInDeck >= 69) {
                endGame();
            }


            if (selectedCards.Count == 3)
            {
                if (deck.isSet(selectedCards[0], selectedCards[1], selectedCards[2]))
                {
                    if (cardsAdded) //if the cards dealt has expanded past 12 cards
                    {
                        //remove the cards in the set taken   
                        for (int i = 0; i < 3; ++i)
                        {
                            deck.dealtCards.Remove(deck.dealtCards[selectedPositions[i]]);
                        }

                        //make extra row of cards not visible
                        for (int i = deck.cardsOnTable - 3; i < deck.cardsOnTable; ++i)
                        {
                            pBoxes[i].Visible = false;
                            pBoxesSelect[i].Visible = false;
                        }


                        //remove cards from table
                        deck.cardsOnTable -= 3;

                        //if you have returned to standard 12 cards no cards have been added
                        if (deck.cardsOnTable == 12) {
                            cardsAdded = false;
                        }
                    }
                    else
                    {
                        //add new cards in positions where cards used to be
                        if (!isEndGame)
                        {
                            for (int i = 0; i < 3; ++i)
                            {
                                deck.dealtCards[selectedPositions[i]] = deck.deckOfCards[deck.curIndexInDeck];
                                deck.curIndexInDeck++;
                            }
                        }
                        else {
                            for (int i = 0; i < 3; ++i) {
                                deck.dealtCards.Remove(deck.dealtCards[selectedPositions[i]]);
                            }
                        }
                    }
                    
                    //update cards on table
                    refreshCards();

                    //if no sets exist on the table 
                    while (deck.checkForSets() == 0)
                    {
                        noSetFound();
                    }

                    numCardsSelected = 0;
                    selectedCards.Clear();
                    btnSelectSet.Enabled = false;

                    txtCheat.Clear();
                    txtCheat.Text = deck.cheat();

                    for (int i = 0; i < deck.cardsOnTable; ++i)
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

        public bool isEndGame = false;

        private void endGame()
        {
            deck.cardsOnTable = 81 - deck.curIndexInDeck;

            if (deck.cardsOnTable == 0) {
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
