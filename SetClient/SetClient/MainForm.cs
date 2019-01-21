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
        public List<PictureBox> pBoxes = new List<PictureBox>();
        public List<PictureBox> pBoxesSelect = new List<PictureBox>();

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

        void AddToListBox_AddItem(string strItem) {
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

        //private void selectImage(int position)
        //{
        //    if (selectedCards.Count >= 3 && !pBoxesSelect[position - 1].Visible)
        //    {
        //        MessageBox.Show("Too many cards selected. Select at most 3.");
        //        return;
        //    }

        //    if (!pBoxesSelect[position - 1].Visible && numCardsSelected < 3)
        //    {
        //        pBoxesSelect[position - 1].Visible = true;
        //        selectedCards.Add(deckOfCards[position - 1]);
        //        //deck.deckOfCards[position - 1].showFeatures();
        //        if (selectedCards.Count == 3)
        //        {
        //            btnSelectSet.Enabled = true;
        //            btnSelectSet.Select();
        //        }
        //    }
        //    else
        //    {
        //        pBoxesSelect[position - 1].Visible = false;
        //        selectedCards.Remove(deckOfCards[position - 1]);
        //        btnSelectSet.Enabled = false;
        //    }
        //}

        private void txtConnect_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("Attempting to load");
                        client.Connect();
        }
        
        //private void refreshCards()
        //{
        //    CardDisplay cardDisplay = new CardDisplay();
        //    //draw cards on table
        //    for (int i = 0; i < cardsShown; ++i)
        //    {
        //        Graphics g = Graphics.FromImage(pBoxes[i].Image);
        //        cardDisplay.drawCard(deckOfCards[i], g);
        //        g.Dispose();
        //        pBoxes[i].Refresh();
        //    }
        //    //make sure all unneeded boxes are invisible
        //    for (int i = cardsShown; i < 21; ++i)
        //    {
        //        pBoxes[i].Visible = false;
        //    }

        //}
        /*
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

        private void pb3_2_Click(object sender, EventArgs e)
        {
            selectImage(4);
        }

        private void pb2_2_Click(object sender, EventArgs e)
        {
            selectImage(5);
        }

        private void pb1_2_Click(object sender, EventArgs e)
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

        private void pb3_4_Click(object sender, EventArgs e)
        {
            selectImage(10);
        }

        private void pb3_5_Click(object sender, EventArgs e)
        {
            selectImage(11);
        }

        private void pb2_4_Click(object sender, EventArgs e)
        {
            selectImage(12);
        }

        private void pb2_5_Click(object sender, EventArgs e)
        {
            selectImage(13);
        }

        private void pb1_4_Click(object sender, EventArgs e)
        {
            selectImage(14);
        }

        private void pb1_5_Click(object sender, EventArgs e)
        {
            selectImage(15);
        }*/
    }

    //use AddToListBox.UpdateListBox(string) to add to listbox anywhere from in code

    //SECTION ALLOWS INTERACTION WITH LISTBOX
    public delegate void AddToListBoxDelegate(string strAdd);

    public static class AddToListBox{
        public static Form MainForm;

        public static event AddToListBoxDelegate AddItem;

        public static void UpdateListBox(string strItem) {
            ThreadItemAdd(strItem);
        }

        public static void ThreadItemAdd(string strItem) {
            if (MainForm != null && MainForm.InvokeRequired)
                MainForm.Invoke(new AddToListBoxDelegate(ThreadItemAdd), new object[] { strItem });
            else
                AddItem(strItem);
        }
    }
}
