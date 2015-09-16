namespace Editor
{
    partial class MapEditor
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
            this.pnlOutput = new System.Windows.Forms.Panel();
            this.mousePositionLable = new System.Windows.Forms.Label();
            this.mousePositionLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zoomLabel = new System.Windows.Forms.Label();
            this.zoomUpDown = new System.Windows.Forms.NumericUpDown();
            this.objectTree = new System.Windows.Forms.TreeView();
            this.objectLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newEntityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BlockProperties = new System.Windows.Forms.Panel();
            this.layerLabel = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.PropertiesName = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textureBox = new System.Windows.Forms.FlowLayoutPanel();
            this.contentTabContainer = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlOutput.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomUpDown)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.BlockProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.contentTabContainer.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOutput
            // 
            this.pnlOutput.AllowDrop = true;
            this.pnlOutput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOutput.Controls.Add(this.mousePositionLable);
            this.pnlOutput.Controls.Add(this.mousePositionLabel);
            this.pnlOutput.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pnlOutput.Location = new System.Drawing.Point(116, 24);
            this.pnlOutput.Name = "pnlOutput";
            this.pnlOutput.Size = new System.Drawing.Size(476, 377);
            this.pnlOutput.TabIndex = 0;
            this.pnlOutput.Click += new System.EventHandler(this.pnlOutput_Click);
            this.pnlOutput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlOutput_MouseDown);
            this.pnlOutput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlOutput_MouseMove);
            this.pnlOutput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlOutput_MouseUp);
            // 
            // mousePositionLable
            // 
            this.mousePositionLable.AutoSize = true;
            this.mousePositionLable.BackColor = System.Drawing.Color.Transparent;
            this.mousePositionLable.Location = new System.Drawing.Point(12, 23);
            this.mousePositionLable.Name = "mousePositionLable";
            this.mousePositionLable.Size = new System.Drawing.Size(0, 13);
            this.mousePositionLable.TabIndex = 4;
            // 
            // mousePositionLabel
            // 
            this.mousePositionLabel.AutoSize = true;
            this.mousePositionLabel.BackColor = System.Drawing.Color.Transparent;
            this.mousePositionLabel.Location = new System.Drawing.Point(3, 0);
            this.mousePositionLabel.Name = "mousePositionLabel";
            this.mousePositionLabel.Size = new System.Drawing.Size(78, 13);
            this.mousePositionLabel.TabIndex = 3;
            this.mousePositionLabel.Text = "Mouseposition:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.zoomLabel);
            this.panel1.Controls.Add(this.zoomUpDown);
            this.panel1.Controls.Add(this.objectTree);
            this.panel1.Controls.Add(this.objectLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(116, 529);
            this.panel1.TabIndex = 3;
            // 
            // zoomLabel
            // 
            this.zoomLabel.AutoSize = true;
            this.zoomLabel.Location = new System.Drawing.Point(4, 191);
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(34, 13);
            this.zoomLabel.TabIndex = 4;
            this.zoomLabel.Text = "Zoom";
            // 
            // zoomUpDown
            // 
            this.zoomUpDown.DecimalPlaces = 1;
            this.zoomUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.zoomUpDown.Location = new System.Drawing.Point(71, 190);
            this.zoomUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.zoomUpDown.Name = "zoomUpDown";
            this.zoomUpDown.Size = new System.Drawing.Size(40, 20);
            this.zoomUpDown.TabIndex = 3;
            this.zoomUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zoomUpDown.ValueChanged += new System.EventHandler(this.zoomUpDown_ValueChanged);
            // 
            // objectTree
            // 
            this.objectTree.Location = new System.Drawing.Point(6, 18);
            this.objectTree.Name = "objectTree";
            this.objectTree.Size = new System.Drawing.Size(105, 166);
            this.objectTree.TabIndex = 2;
            this.objectTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectTree_NodeMouseClick);
            // 
            // objectLabel
            // 
            this.objectLabel.AutoSize = true;
            this.objectLabel.Location = new System.Drawing.Point(3, 2);
            this.objectLabel.Name = "objectLabel";
            this.objectLabel.Size = new System.Drawing.Size(43, 13);
            this.objectLabel.TabIndex = 1;
            this.objectLabel.Text = "Objects";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.entitiesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(759, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLevelToolStripMenuItem,
            this.loadLevelToolStripMenuItem,
            this.saveLevelToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newLevelToolStripMenuItem
            // 
            this.newLevelToolStripMenuItem.Name = "newLevelToolStripMenuItem";
            this.newLevelToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.newLevelToolStripMenuItem.Text = "New Level";
            this.newLevelToolStripMenuItem.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // loadLevelToolStripMenuItem
            // 
            this.loadLevelToolStripMenuItem.Name = "loadLevelToolStripMenuItem";
            this.loadLevelToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.loadLevelToolStripMenuItem.Text = "Load Level";
            this.loadLevelToolStripMenuItem.Click += new System.EventHandler(this.loadLevelToolStripMenuItem_Click);
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.saveLevelToolStripMenuItem.Text = "Save Level";
            this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.saveLevelToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapPropertiesToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // mapPropertiesToolStripMenuItem
            // 
            this.mapPropertiesToolStripMenuItem.Name = "mapPropertiesToolStripMenuItem";
            this.mapPropertiesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.mapPropertiesToolStripMenuItem.Text = "Map Properties";
            this.mapPropertiesToolStripMenuItem.Click += new System.EventHandler(this.mapPropertiesToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // entitiesToolStripMenuItem
            // 
            this.entitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newEntityToolStripMenuItem});
            this.entitiesToolStripMenuItem.Name = "entitiesToolStripMenuItem";
            this.entitiesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.entitiesToolStripMenuItem.Text = "Entities";
            // 
            // newEntityToolStripMenuItem
            // 
            this.newEntityToolStripMenuItem.Name = "newEntityToolStripMenuItem";
            this.newEntityToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.newEntityToolStripMenuItem.Text = "New Entity";
            this.newEntityToolStripMenuItem.Click += new System.EventHandler(this.newEntityToolStripMenuItem_Click);
            // 
            // BlockProperties
            // 
            this.BlockProperties.Controls.Add(this.layerLabel);
            this.BlockProperties.Controls.Add(this.numericUpDown1);
            this.BlockProperties.Controls.Add(this.PropertiesName);
            this.BlockProperties.Controls.Add(this.textBox1);
            this.BlockProperties.Dock = System.Windows.Forms.DockStyle.Right;
            this.BlockProperties.Location = new System.Drawing.Point(593, 24);
            this.BlockProperties.Name = "BlockProperties";
            this.BlockProperties.Size = new System.Drawing.Size(166, 529);
            this.BlockProperties.TabIndex = 5;
            // 
            // layerLabel
            // 
            this.layerLabel.AutoSize = true;
            this.layerLabel.Location = new System.Drawing.Point(3, 52);
            this.layerLabel.Name = "layerLabel";
            this.layerLabel.Size = new System.Drawing.Size(33, 13);
            this.layerLabel.TabIndex = 3;
            this.layerLabel.Text = "Layer";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(50, 50);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(104, 20);
            this.numericUpDown1.TabIndex = 2;
            // 
            // PropertiesName
            // 
            this.PropertiesName.AutoSize = true;
            this.PropertiesName.Location = new System.Drawing.Point(3, 26);
            this.PropertiesName.Name = "PropertiesName";
            this.PropertiesName.Size = new System.Drawing.Size(35, 13);
            this.PropertiesName.TabIndex = 1;
            this.PropertiesName.Text = "Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(50, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(104, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textureBox
            // 
            this.textureBox.AutoScroll = true;
            this.textureBox.BackColor = System.Drawing.Color.CadetBlue;
            this.textureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textureBox.Location = new System.Drawing.Point(3, 3);
            this.textureBox.Name = "textureBox";
            this.textureBox.Size = new System.Drawing.Size(463, 122);
            this.textureBox.TabIndex = 6;
            // 
            // contentTabContainer
            // 
            this.contentTabContainer.Controls.Add(this.tabPage1);
            this.contentTabContainer.Controls.Add(this.tabPage2);
            this.contentTabContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.contentTabContainer.Location = new System.Drawing.Point(116, 399);
            this.contentTabContainer.Name = "contentTabContainer";
            this.contentTabContainer.SelectedIndex = 0;
            this.contentTabContainer.Size = new System.Drawing.Size(477, 154);
            this.contentTabContainer.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textureBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(469, 128);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(469, 128);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 553);
            this.Controls.Add(this.contentTabContainer);
            this.Controls.Add(this.BlockProperties);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlOutput);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MapEditor";
            this.Text = "MapEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.pnlOutput.ResumeLayout(false);
            this.pnlOutput.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomUpDown)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.BlockProperties.ResumeLayout(false);
            this.BlockProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.contentTabContainer.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlOutput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label mousePositionLable;
        private System.Windows.Forms.Label mousePositionLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapPropertiesToolStripMenuItem;
        private System.Windows.Forms.Label objectLabel;
        private System.Windows.Forms.TreeView objectTree;
        private System.Windows.Forms.Panel BlockProperties;
        private System.Windows.Forms.Label PropertiesName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label layerLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel textureBox;
        private System.Windows.Forms.Label zoomLabel;
        private System.Windows.Forms.NumericUpDown zoomUpDown;
        private System.Windows.Forms.TabControl contentTabContainer;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem entitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newEntityToolStripMenuItem;
    }
}