using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Apache.Cassandra;
using Thrift.Protocol;
using Thrift.Transport;

namespace CassandraWorkBench
{
    public partial class LoginForm : Form
    {
        private MainForm mainForm;

        public LoginForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            TSocket socket;
            Cassandra.Client client;

            socket = new TSocket(hostNameTextBox.Text, int.Parse(portTextBox.Text));
            //socket.Open();

            TFramedTransport transport = new TFramedTransport(socket);
            TBinaryProtocol protocol = new TBinaryProtocol(transport);
            client = new Cassandra.Client(protocol);

            transport.Open();

            mainForm.Client = client;
            this.DialogResult = DialogResult.OK;

        }
    }
}
