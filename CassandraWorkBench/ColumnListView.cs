using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Apache.Cassandra;

namespace CassandraWorkBench
{
    public partial class ColumnListView : Form
    {
        public ColumnListView()
        {
            InitializeComponent();
        }

        public List<KeySlice> data;

        private void ColumnListView_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data;
        }
    }
}
