using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookmarksUI
{
    public partial class MainForm : Form
    {
        private Results results;

        public MainForm()
        {
            InitializeComponent();
            results = Results.Instance;
        }

        public Object LboxResults
        {
            //get { return lboxResults.DataSource; }
            set { lboxResults.DataSource = value; }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            results.ListResults(this, txtInput.Text);
        }

        private void txtInput_Enter(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = btnSearch;
        }

        private void txtInput_Leave(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = null;
        }


    }
}
