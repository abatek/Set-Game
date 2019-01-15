using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SetClient
{
    public partial class MainForm : Form
    {
        private Threading client = new Threading();

        public MainForm()
        {
            InitializeComponent();
            AddToListBox.AddItem += AddToListBox_AddItem;
        }

        void AddToListBox_AddItem(string strItem) {
            lbReceive.Items.Add(strItem);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
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

        private void txtConnect_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Attempting to load");
            client.Connect();
        }
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
