//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;



namespace org.auroracoin.desktop.ui
{
    public partial class FxInput : Form
    {
       
        public FxInput()
        {
            InitializeComponent();
        }

        public FxInput( string psCaption, string psInputBoxCaption )
        {
            InitializeComponent();
            //
            this.Text = psCaption;
            this.lbInput.Text = psInputBoxCaption;
        }
        
        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string getInput()
        {
            return txValue.Text;
        }


    } // EOC
}
