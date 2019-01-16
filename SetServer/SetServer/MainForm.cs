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

        private void pb1_1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
        }

        #region select picturebox
        private void pb1_1_Click(object sender, EventArgs e)
        {
            if (pb1_1_select.Visible == false)
            {
                pb1_1_select.Visible = true;
            }
            else
                pb1_1_select.Visible = false;
        }


        #endregion



        private void pb3_1_Click(object sender, EventArgs e)
        {

        }

        private void pb2_1_Click_1(object sender, EventArgs e)
        {
            if (pb_2_1_select.Visible == false)
            {
                pb_2_1_select.Visible = true;
            }
            else
                pb_2_1_select.Visible = false;
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
