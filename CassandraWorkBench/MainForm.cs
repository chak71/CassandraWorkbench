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
using System.Collections;


namespace CassandraWorkBench
{
    public partial class MainForm : Form
    {
        System.Text.Encoding utf8Encoding = System.Text.Encoding.UTF8; 

        private Cassandra.Client client;

        public Cassandra.Client Client
        {
           get{ return this.client; }
           set{ this.client = value; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this);
            loginForm.ShowDialog();
            loginForm.Close();

            this.populateTreeView();
        }


        private void populateTreeView()
        {
            populateKeySpaces();
        }

        private void populateKeySpaces()
        {
            THashSet<string> keySpaces;
            keySpaces = client.describe_keyspaces();
            foreach (string keySpace in keySpaces)
            {
                TreeNode keySpaceNode = populateKeySpace(keySpace);
                TreeNode KeySpaceConfig = populateKeySpaceConfig(keySpace);
                TreeNode KeySpaceColumns = populateColumns(keySpace);

                keySpaceNode.Nodes.Add(KeySpaceConfig);
                keySpaceNode.Nodes.Add(KeySpaceColumns);

                KeySpaceTree.Nodes.Add(keySpaceNode);
            }
        }

        private TreeNode populateKeySpace(String keySpace)
        {
            TreeNode currentNode = new TreeNode(keySpace);
            currentNode.Tag = "keyspace";

            return currentNode;
        }

        private TreeNode populateKeySpaceConfig(String keySpace)
        {
            TreeNode configNode =  new TreeNode("Configuration");
            configNode.Tag = "config";
            List<TokenRange> tokenRanges = new List<TokenRange>();
            try
            {
                tokenRanges = client.describe_ring(keySpace);
            }
            catch (InvalidRequestException ex)
            {

            }

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
                    configNode.Nodes.Add(endpoint);
                }
            }

            return configNode;
        }

        private TreeNode populateColumns(String keySpace)
        {
            Dictionary<string, Dictionary<string, string>> familyColumns;

            TreeNode columns = new TreeNode("Columns");

            familyColumns = client.describe_keyspace(keySpace);
            foreach (KeyValuePair<string, Dictionary<string, string>> familyColumn in familyColumns)
            {
                if (familyColumn.Value["Type"] == "Super")
                {
                    columns.Nodes.Add(familyColumn.Key).Tag = "super";
                    //Super icon
                } 
                else if (familyColumn.Value["Type"] == "Standard")
                {
                    columns.Nodes.Add(familyColumn.Key).Tag = "column";
                    //Standard icon
                }

            }

            return columns;
        }

        private void KeySpaceTree_Click(object sender, EventArgs e)
        {
            TreeNode node = (sender as TreeView).SelectedNode;

            if (node != null)
            {
                if(((string)node.Tag) == "super" || ((string)node.Tag) == "column")
                {
                    long timeStamp = DateTime.Now.Millisecond;

                    ColumnPath nameColumnPath = new ColumnPath()
                    {
                        Column_family = "Standard1",
                        Column = utf8Encoding.GetBytes("name")
                    };

                    //Insert the data into the column 'name' 
                    //client.insert("Keyspace1",
                    //              "1",
                    //              nameColumnPath,
                    //              utf8Encoding.GetBytes("Joe Bloggs"),
                    //              timeStamp,
                    //              ConsistencyLevel.ONE);

                    //client.insert("Keyspace1",
                    //              "2",
                    //              nameColumnPath,
                    //              utf8Encoding.GetBytes("Joe Soap"),
                    //              timeStamp,
                    //              ConsistencyLevel.ONE);



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

                    //List<KeySlice> keyslices = client.get_range_slice("Keyspace1", parent, predicate, "", "", 100, ConsistencyLevel.ONE);

                    //int count = client.get_count("Keyspace1","1", parent, ConsistencyLevel.ONE);
                    //List<string> splits = client.describe_splits("", "", count);
                    //Dictionary<string, List<ColumnOrSuperColumn>> test = client.multiget_slice("Keyspace1", splits, parent, predicate, ConsistencyLevel.ONE);

                    //client.get("Keyspace1", "", nameColumnPath, ConsistencyLevel.ONE);
                    //client.multiget_slice("", "", nameColumnPath, predicate, ConsistencyLevel.ONE);

                    ColumnListView columnListView = new ColumnListView();
                    columnListView.MdiParent = this;
                    //columnListView.data = keyslices;
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
          // this.socket.Close();
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
            KeySpaceTree.Nodes.Add("New KeySpace");
        }

        private void removeKeySpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = KeySpaceTree.SelectedNode;
            if (node.Tag.ToString() == "keyspace")
            {
                client.system_drop_keyspace(node.Text);
            }

            // Data bind in future
            populateTreeView();
        }

        private void KeySpaceTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {

            List<CfDef> cfDefs = new List<CfDef>();
            CfDef cfDef = new CfDef();
            cfDef.Name = "Test";
            cfDef.Keyspace = e.Node.Text;
            cfDef.Column_type = "Standard";
            cfDefs.Add(cfDef);

            KsDef keySpaceDef = new KsDef();
            keySpaceDef.Name = e.Node.Text;
            keySpaceDef.Replication_factor = 1;
            keySpaceDef.Strategy_class = "org.apache.cassandra.locator.RackUnawareStrategy";
            keySpaceDef.Cf_defs = cfDefs;
               
            client.system_add_keyspace(keySpaceDef);

            // Data bind in future
            populateTreeView();
        }
    }
}
