using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddressRequest.Demo
{
    public partial class frmRequestAddress : Form
    {
        public frmRequestAddress()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                var data = new AddressService(ServiceEnum.Postmon, true).GetAddress(textBoxZipCode.Text);
                dataGridView1.DataSource = new List<AddressData>() { data };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
