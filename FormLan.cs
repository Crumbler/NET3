using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace NET3
{
    public partial class FormLan : Form
    {
        public Lan chosenLan;
        private List<Lan> lans;
        public FormLan()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(List<Lan> Lans)
        {
            LsbxMain.Items.AddRange(Lans.Select(e => e.addr).ToArray());
            LsbxMain.SelectedIndex = 0;

            lans = Lans;

            return ShowDialog();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            chosenLan = lans[LsbxMain.SelectedIndex];
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
