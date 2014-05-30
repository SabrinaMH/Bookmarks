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
        private FormData formData;

        public MainForm()
        {
            InitializeComponent();
            formData = FormData.Instance;
        }

        public Object LboxResults
        {
            set { lboxResults.DataSource = value; }
        }

        public string TxtUrl
        {
            set { txtUrl.Text = value; }
        }

        public string TxtComment
        {
            set { txtComment.Text = value; }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            formData.ListResults(this, txtInput.Text);
        }

        private void txtInput_Enter(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = btnSearch;
        }

        private void txtInput_Leave(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = null;
        }

        private void lboxResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            formData.ListData(this, (string)lboxResults.SelectedItem);
        }
    }
}
