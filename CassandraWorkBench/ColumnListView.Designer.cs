namespace CassandraWorkBench
{
    partial class ColumnListView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.searchResultDataGridView = new System.Windows.Forms.DataGridView();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchSplitter = new System.Windows.Forms.Splitter();
            this.columnContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.searchResultDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // searchResultDataGridView
            // 
            this.searchResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchResultDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResultDataGridView.Location = new System.Drawing.Point(0, 0);
            this.searchResultDataGridView.Name = "searchResultDataGridView";
            this.searchResultDataGridView.Size = new System.Drawing.Size(703, 541);
            this.searchResultDataGridView.TabIndex = 0;
            // 
            // searchPanel
            // 
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(703, 100);
            this.searchPanel.TabIndex = 1;
            // 
            // searchSplitter
            // 
            this.searchSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchSplitter.Location = new System.Drawing.Point(0, 100);
            this.searchSplitter.Name = "searchSplitter";
            this.searchSplitter.Size = new System.Drawing.Size(703, 3);
            this.searchSplitter.TabIndex = 2;
            this.searchSplitter.TabStop = false;
            // 
            // columnContextMenuStrip
            // 
            this.columnContextMenuStrip.Name = "columnContextMenuStrip";
            this.columnContextMenuStrip.Size = new System.Drawing.Size(153, 26);
            // 
            // ColumnListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 541);
            this.Controls.Add(this.searchSplitter);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.searchResultDataGridView);
            this.Name = "ColumnListView";
            this.Text = "ColumnListView";
            this.Load += new System.EventHandler(this.ColumnListView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.searchResultDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView searchResultDataGridView;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Splitter searchSplitter;
        private System.Windows.Forms.ContextMenuStrip columnContextMenuStrip;
    }
}