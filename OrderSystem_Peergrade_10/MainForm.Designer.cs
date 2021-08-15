
namespace OrderSystem_Peergrade_10
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewDir = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csvAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listViewProducts = new System.Windows.Forms.ListView();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.textBoxSumPaid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxOrders = new System.Windows.Forms.ComboBox();
            this.listViewOrders = new System.Windows.Forms.ListView();
            this.listViewUsers = new System.Windows.Forms.ListView();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripDirectories = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addDirInsideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDirOutsideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripProducts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addProdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editProdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteProdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToCartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStripOrders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.placeOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.payForOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.contextMenuStripDirectories.SuspendLayout();
            this.contextMenuStripProducts.SuspendLayout();
            this.contextMenuStripOrders.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewDir);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            this.splitContainer1.Panel1MinSize = 254;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1266, 752);
            this.splitContainer1.SplitterDistance = 418;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewDir
            // 
            this.treeViewDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDir.HideSelection = false;
            this.treeViewDir.Location = new System.Drawing.Point(0, 24);
            this.treeViewDir.Name = "treeViewDir";
            this.treeViewDir.Size = new System.Drawing.Size(418, 728);
            this.treeViewDir.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.csvAnalysisToolStripMenuItem,
            this.randomizeToolStripMenuItem,
            this.changeUserToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(418, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // csvAnalysisToolStripMenuItem
            // 
            this.csvAnalysisToolStripMenuItem.Name = "csvAnalysisToolStripMenuItem";
            this.csvAnalysisToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.csvAnalysisToolStripMenuItem.Text = "Csv Analysis";
            // 
            // randomizeToolStripMenuItem
            // 
            this.randomizeToolStripMenuItem.Name = "randomizeToolStripMenuItem";
            this.randomizeToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.randomizeToolStripMenuItem.Text = "Randomize";
            // 
            // changeUserToolStripMenuItem
            // 
            this.changeUserToolStripMenuItem.Name = "changeUserToolStripMenuItem";
            this.changeUserToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.changeUserToolStripMenuItem.Text = "Change User";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listViewProducts);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(844, 752);
            this.splitContainer2.SplitterDistance = 469;
            this.splitContainer2.TabIndex = 1;
            // 
            // listViewProducts
            // 
            this.listViewProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewProducts.FullRowSelect = true;
            this.listViewProducts.GridLines = true;
            this.listViewProducts.HideSelection = false;
            this.listViewProducts.Location = new System.Drawing.Point(0, 0);
            this.listViewProducts.MultiSelect = false;
            this.listViewProducts.Name = "listViewProducts";
            this.listViewProducts.Size = new System.Drawing.Size(469, 752);
            this.listViewProducts.TabIndex = 0;
            this.listViewProducts.UseCompatibleStateImageBehavior = false;
            this.listViewProducts.View = System.Windows.Forms.View.Details;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.listViewUsers);
            this.splitContainer3.Size = new System.Drawing.Size(371, 752);
            this.splitContainer3.SplitterDistance = 449;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.textBoxSumPaid);
            this.splitContainer4.Panel1.Controls.Add(this.label1);
            this.splitContainer4.Panel1.Controls.Add(this.comboBoxOrders);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.listViewOrders);
            this.splitContainer4.Size = new System.Drawing.Size(371, 449);
            this.splitContainer4.SplitterDistance = 28;
            this.splitContainer4.TabIndex = 1;
            // 
            // textBoxSumPaid
            // 
            this.textBoxSumPaid.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBoxSumPaid.Location = new System.Drawing.Point(246, 0);
            this.textBoxSumPaid.Name = "textBoxSumPaid";
            this.textBoxSumPaid.ReadOnly = true;
            this.textBoxSumPaid.Size = new System.Drawing.Size(125, 23);
            this.textBoxSumPaid.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sum paid:";
            // 
            // comboBoxOrders
            // 
            this.comboBoxOrders.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBoxOrders.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxOrders.FormattingEnabled = true;
            this.comboBoxOrders.Items.AddRange(new object[] {
            "One client\'s order",
            "All orders",
            "All active orders"});
            this.comboBoxOrders.Location = new System.Drawing.Point(0, 0);
            this.comboBoxOrders.Name = "comboBoxOrders";
            this.comboBoxOrders.Size = new System.Drawing.Size(174, 24);
            this.comboBoxOrders.TabIndex = 1;
            this.comboBoxOrders.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrders_SelectedIndexChanged);
            // 
            // listViewOrders
            // 
            this.listViewOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewOrders.FullRowSelect = true;
            this.listViewOrders.GridLines = true;
            this.listViewOrders.HideSelection = false;
            this.listViewOrders.Location = new System.Drawing.Point(0, 0);
            this.listViewOrders.MultiSelect = false;
            this.listViewOrders.Name = "listViewOrders";
            this.listViewOrders.Size = new System.Drawing.Size(371, 417);
            this.listViewOrders.TabIndex = 0;
            this.listViewOrders.UseCompatibleStateImageBehavior = false;
            this.listViewOrders.View = System.Windows.Forms.View.Details;
            // 
            // listViewUsers
            // 
            this.listViewUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewUsers.FullRowSelect = true;
            this.listViewUsers.GridLines = true;
            this.listViewUsers.HideSelection = false;
            this.listViewUsers.Location = new System.Drawing.Point(0, 0);
            this.listViewUsers.MultiSelect = false;
            this.listViewUsers.Name = "listViewUsers";
            this.listViewUsers.Size = new System.Drawing.Size(371, 299);
            this.listViewUsers.TabIndex = 0;
            this.listViewUsers.UseCompatibleStateImageBehavior = false;
            this.listViewUsers.View = System.Windows.Forms.View.Details;
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // contextMenuStripDirectories
            // 
            this.contextMenuStripDirectories.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripDirectories.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDirInsideToolStripMenuItem,
            this.addDirOutsideToolStripMenuItem,
            this.editDirToolStripMenuItem,
            this.deleteDirToolStripMenuItem});
            this.contextMenuStripDirectories.Name = "contextMenuStrip1";
            this.contextMenuStripDirectories.Size = new System.Drawing.Size(159, 92);
            // 
            // addDirInsideToolStripMenuItem
            // 
            this.addDirInsideToolStripMenuItem.Name = "addDirInsideToolStripMenuItem";
            this.addDirInsideToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addDirInsideToolStripMenuItem.Text = "Add Dir Inside";
            // 
            // addDirOutsideToolStripMenuItem
            // 
            this.addDirOutsideToolStripMenuItem.Name = "addDirOutsideToolStripMenuItem";
            this.addDirOutsideToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addDirOutsideToolStripMenuItem.Text = "Add Dir Outside";
            // 
            // editDirToolStripMenuItem
            // 
            this.editDirToolStripMenuItem.Name = "editDirToolStripMenuItem";
            this.editDirToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.editDirToolStripMenuItem.Text = "Edit";
            // 
            // deleteDirToolStripMenuItem
            // 
            this.deleteDirToolStripMenuItem.Name = "deleteDirToolStripMenuItem";
            this.deleteDirToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.deleteDirToolStripMenuItem.Text = "Delete";
            // 
            // contextMenuStripProducts
            // 
            this.contextMenuStripProducts.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripProducts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProdToolStripMenuItem,
            this.editProdToolStripMenuItem,
            this.deleteProdToolStripMenuItem,
            this.addToCartToolStripMenuItem});
            this.contextMenuStripProducts.Name = "contextMenuStrip1";
            this.contextMenuStripProducts.Size = new System.Drawing.Size(136, 92);
            // 
            // addProdToolStripMenuItem
            // 
            this.addProdToolStripMenuItem.Name = "addProdToolStripMenuItem";
            this.addProdToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.addProdToolStripMenuItem.Text = "Add";
            // 
            // editProdToolStripMenuItem
            // 
            this.editProdToolStripMenuItem.Name = "editProdToolStripMenuItem";
            this.editProdToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.editProdToolStripMenuItem.Text = "Edit";
            // 
            // deleteProdToolStripMenuItem
            // 
            this.deleteProdToolStripMenuItem.Name = "deleteProdToolStripMenuItem";
            this.deleteProdToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.deleteProdToolStripMenuItem.Text = "Delete";
            // 
            // addToCartToolStripMenuItem
            // 
            this.addToCartToolStripMenuItem.Name = "addToCartToolStripMenuItem";
            this.addToCartToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.addToCartToolStripMenuItem.Text = "Add to Cart";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "bin files (*.bin)|*.bin";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "bin files (*.bin)|*.bin";
            // 
            // contextMenuStripOrders
            // 
            this.contextMenuStripOrders.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripOrders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.placeOrderToolStripMenuItem,
            this.payForOrderToolStripMenuItem,
            this.addStatusToolStripMenuItem});
            this.contextMenuStripOrders.Name = "contextMenuStripOrders";
            this.contextMenuStripOrders.Size = new System.Drawing.Size(147, 70);
            // 
            // placeOrderToolStripMenuItem
            // 
            this.placeOrderToolStripMenuItem.Name = "placeOrderToolStripMenuItem";
            this.placeOrderToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.placeOrderToolStripMenuItem.Text = "Place Order";
            // 
            // payForOrderToolStripMenuItem
            // 
            this.payForOrderToolStripMenuItem.Name = "payForOrderToolStripMenuItem";
            this.payForOrderToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.payForOrderToolStripMenuItem.Text = "Pay For Order";
            // 
            // addStatusToolStripMenuItem
            // 
            this.addStatusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processToolStripMenuItem,
            this.shipToolStripMenuItem,
            this.executeToolStripMenuItem});
            this.addStatusToolStripMenuItem.Name = "addStatusToolStripMenuItem";
            this.addStatusToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.addStatusToolStripMenuItem.Text = "Add Status";
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.processToolStripMenuItem.Text = "Proceed";
            // 
            // shipToolStripMenuItem
            // 
            this.shipToolStripMenuItem.Name = "shipToolStripMenuItem";
            this.shipToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.shipToolStripMenuItem.Text = "Ship";
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.executeToolStripMenuItem.Text = "Execute";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 752);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(780, 458);
            this.Name = "MainForm";
            this.Text = "61 94 95 94 61";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.contextMenuStripDirectories.ResumeLayout(false);
            this.contextMenuStripProducts.ResumeLayout(false);
            this.contextMenuStripOrders.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewDir;
        private System.Windows.Forms.ListView listViewProducts;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDirectories;
        private System.Windows.Forms.ToolStripMenuItem editDirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDirOutsideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDirInsideToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripProducts;
        private System.Windows.Forms.ToolStripMenuItem addProdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editProdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteProdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem csvAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListView listViewOrders;
        private System.Windows.Forms.ListView listViewUsers;
        private System.Windows.Forms.ToolStripMenuItem changeUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToCartToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOrders;
        private System.Windows.Forms.ToolStripMenuItem placeOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem payForOrderToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ComboBox comboBoxOrders;
        private System.Windows.Forms.TextBox textBoxSumPaid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem addStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
    }
}

