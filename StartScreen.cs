using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewGame1
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            GameManager gm = new GameManager();
            Form1 frm1 = new Form1(gm.getStart());
            frm1.Location = this.Location;
            frm1.FormClosed += (s, args) => this.Close();
            frm1.Show();
            this.Hide();
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
