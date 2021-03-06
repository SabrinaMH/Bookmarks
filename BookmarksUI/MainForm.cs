﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using BookmarksBLL;
using BookmarksDomain;

namespace BookmarksUI
{
    public partial class MainForm : Form, IBookmarksView
    {
        private BookmarksBLL.Operations operations;
        private BindingSource bs;
        private BindingList<Bookmark> results;

        public MainForm()
        { 
            InitializeComponent();
            operations = BookmarksBLL.Operations.Instance;
            operations.View = this;
            bs = new BindingSource();
            lboxResults.DisplayMember = "Title";
            lboxResults.ValueMember = "Id";
            lboxResults.DataSource = bs; /* Set after DisplayMember and ValueMember to avoid errors 
                                          * (SelectedIndexChanged may be raised otherwise, causing errors).
                                          * Source: http://www.codeproject.com/Articles/8390/Best-Practice-for-Binding-WinForms-ListControls
                                          */
            KeyPreview = true; // Enables shortcuts on form level, no matter which control has focus
        }
        
        public string PartOfTitle
        {
            get { return this.txtInput.Text; }
        }

        public BindingList<Bookmark> Results
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
                bs.DataSource = results;
            }
        }

        public new void Click(object sender, EventArgs e)
        {
            Results = new BindingList<Bookmark>(operations.PerformSearch(txtInput.Text));
            if (Results.Count > 0)
            {
                lboxResults.Focus();
                lboxResults_SelectedIndexChanged(null, null);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && ActiveControl == txtInput)
            {
                Click(null, null);
            }
            else if (keyData == (Keys.Control | Keys.O))
            {
                btnOpen_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void lboxResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lboxResults.SelectedIndex != -1)
            {
                string[] data = operations.GetDataForEntry((Bookmark)lboxResults.SelectedItem);
                txtUrl.Text = data[0];
                txtComment.Text = data[1];
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Returns an instance of Process, but no need to dispose the object, as it's null when fed a URL.
            Process.Start(txtUrl.Text);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            operations.Close();
        }
    }
}
