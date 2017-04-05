using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neo
{
    public partial class NeoHomePage : Form
    {
        public NeoHomePage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manage ManageForm = new Manage("select * from Children");
            ManageForm.Show();
        }
    }
}
