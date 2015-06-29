using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReparatieSysteem
{
    public partial class MainForm : Form
    {
        private ReparatieSysteem reparatieSysteem;

        public MainForm()
        {
            InitializeComponent();

            reparatieSysteem = new ReparatieSysteem();
        }
    }
}
