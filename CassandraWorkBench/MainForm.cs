using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Security.Principal;

using Apache.Cassandra;

using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Collections;


namespace CassandraWorkBench
{
    public partial class MainForm : Form
    {
        System.Text.Encoding utf8Encoding = System.Text.Encoding.UTF8; 

        TSocket socket socket;
        Cassandra.Client client;

        private string host = "localhost";
        private int port = 9160;
        private THashSet<string> keySpaces;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            socket = new TSocket(host, port);
            socket.Open();

            TBinaryProtocol protocol = new TBinaryProtocol(socket);

            client = new Cassandra.Client(protocol);

            keySpaces = client.describe_keyspaces();
            Dictionary<string, Dictionary<string, string>> familyColumns;

            foreach (string keySpace in keySpaces)
            {
                TreeNode currentNode = (TreeNode)KeySpaceTree.Nodes.Add(keySpace);
                currentNode.Tag = "keyspace";
                
                TreeNode configNode = (TreeNode)currentNode.Nodes.Add("Configuration");
                configNode.Tag = "config";
                List<TokenRange> tokenRanges = client.describe_ring(keySpace);
                foreach (TokenRange tokenRange in tokenRanges)
                {
                    configNode.Nodes.Add("Version: " + client.describe_version());
                    configNode.Nodes.Add("Partitioner: " + client.describe_partitioner());
                    configNode.Nodes.Add("ClusterName: " + client.describe_cluster_name());
                    configNode.Nodes.Add("Start Token: " + tokenRange.Start_token);
                    configNode.Nodes.Add("End Token: " + tokenRange.End_token);
                    TreeNode endpointsNode = configNode.Nodes.Add("EndPoints");
                    foreach (string endpoint in tokenRange.Endpoints)
                    {
                        endpointsNode.Nodes.Add(endpoint);
                    }
                }

                familyColumns = client.describe_keyspace(keySpace);
                foreach (KeyValuePair<string, Dictionary<string, string>> familyColumn in familyColumns)
                {
                    if (familyColumn.Value["Type"] == "Super")
                    {
                        currentNode.Nodes.Add(familyColumn.Key).Tag = "super";
                        //Super icon
                    } 
                    else if (familyColumn.Value["Type"] == "Standard")
                    {
                        currentNode.Nodes.Add(familyColumn.Key).Tag = "column";
                        //Standard icon
                    }

                }
            }
 
        }

        private void KeySpaceTree_Click(object sender, EventArgs e)
        {
            TreeNode node = (sender as TreeView).SelectedNode;

            if (node != null)
            {
                if(node.Tag == "super" || node.Tag == "column")
                {
                    long timeStamp = DateTime.Now.Millisecond;

                    ColumnPath nameColumnPath = new ColumnPath()
                    {
                        Column_family = "Standard1",
                        Column = utf8Encoding.GetBytes("name")
                    };

                    //Insert the data into the column 'name' 
                    client.insert("Keyspace1",
                                  "1",
                                  nameColumnPath,
                                  utf8Encoding.GetBytes("Joe Bloggs"),
                                  timeStamp,
                                  ConsistencyLevel.ONE);

                    client.insert("Keyspace1",
                                  "2",
                                  nameColumnPath,
                                  utf8Encoding.GetBytes("Joe Soap"),
                                  timeStamp,
                                  ConsistencyLevel.ONE);



                    //Read an entire row
                    SlicePredicate predicate = new SlicePredicate()
                    {
                        Slice_range = new SliceRange()
                        {
                            //Start and Finish cannot be null
                            Start = new byte[0],
                            Finish = new byte[0],
                            Count = 10,
                            Reversed = false
                        }
                    };

                    ColumnParent parent = new ColumnParent()
                    {
                        Column_family = node.Text
                    };

                    List<KeySlice> keyslices = client.get_range_slice("Keyspace1", parent, predicate, "", "", 100, ConsistencyLevel.ONE);

                    int count = client.get_count("Keyspace1","1", parent, ConsistencyLevel.ONE);
                    List<string> splits = client.describe_splits("", "", count);
                    Dictionary<string, List<ColumnOrSuperColumn>> test = client.multiget_slice("Keyspace1", splits, parent, predicate, ConsistencyLevel.ONE);

                    //client.get("Keyspace1", "", nameColumnPath, ConsistencyLevel.ONE);
                    //client.multiget_slice("", "", nameColumnPath, predicate, ConsistencyLevel.ONE);

                    ColumnListView columnListView = new ColumnListView();
                    columnListView.MdiParent = this;
                    columnListView.data = keyslices;
                    columnListView.Show();
                }
            }
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
           this.socket.Close();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
             this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
             this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void addKeySpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.
        }



    }
}
