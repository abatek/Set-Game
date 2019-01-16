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

        public MainForm()
        {
            InitializeComponent();
            AddToListBox.AddItem += AddToListBox_AddItem;
        }

        void AddToListBox_AddItem(string strItem)
        {
            lbReceive.Items.Add(strItem);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
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
            SetLogic test = new SetLogic();
            test.generateDeck();
            test.shuffleDeck();
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
