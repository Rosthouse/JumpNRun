namespace Editor
{
    partial class EntityCreator
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
            this.hierarchyTreeView = new System.Windows.Forms.TreeView();
            this.entityNameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.parameterListView = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addEntityButton = new System.Windows.Forms.Button();
            this.removeEntityButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.parameterNameLabel = new System.Windows.Forms.Label();
            this.parameterNameTextBox = new System.Windows.Forms.TextBox();
            this.typeDropDown = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.parameterLabel = new System.Windows.Forms.Label();
            this.removeParameterButton = new System.Windows.Forms.Button();
            this.addParameterButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hierarchyTreeView
            // 
            this.hierarchyTreeView.Location = new System.Drawing.Point(4, 3);
            this.hierarchyTreeView.Name = "hierarchyTreeView";
            this.hierarchyTreeView.Size = new System.Drawing.Size(98, 507);
            this.hierarchyTreeView.TabIndex = 0;
            this.hierarchyTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.hierarchyTreeView_NodeMouseClick);
            // 
            // entityNameLabel
            // 
            this.entityNameLabel.AutoSize = true;
            this.entityNameLabel.Location = new System.Drawing.Point(3, 12);
            this.entityNameLabel.Name = "entityNameLabel";
            this.entityNameLabel.Size = new System.Drawing.Size(35, 13);
            this.entityNameLabel.TabIndex = 1;
            this.entityNameLabel.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(65, 9);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(281, 20);
            this.nameTextBox.TabIndex = 2;
            // 
            // parameterListView
            // 
            this.parameterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.typeHeader});
            this.parameterListView.FullRowSelect = true;
            this.parameterListView.GridLines = true;
            this.parameterListView.LabelEdit = true;
            this.parameterListView.Location = new System.Drawing.Point(6, 87);
            this.parameterListView.Name = "parameterListView";
            this.parameterListView.Size = new System.Drawing.Size(340, 405);
            this.parameterListView.TabIndex = 3;
            this.parameterListView.UseCompatibleStateImageBehavior = false;
            this.parameterListView.View = System.Windows.Forms.View.Details;
            this.parameterListView.SelectedIndexChanged += new System.EventHandler(this.parameterListView_SelectedIndexChanged);
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Name";
            this.nameHeader.Width = 108;
            // 
            // typeHeader
            // 
            this.typeHeader.DisplayIndex = global::Editor.Properties.Settings.Default.Object;
            this.typeHeader.Text = "Typ";
            this.typeHeader.Width = 78;
            // 
            // addEntityButton
            // 
            this.addEntityButton.Location = new System.Drawing.Point(108, 209);
            this.addEntityButton.Name = "addEntityButton";
            this.addEntityButton.Size = new System.Drawing.Size(64, 23);
            this.addEntityButton.TabIndex = 4;
            this.addEntityButton.Text = "Add";
            this.addEntityButton.UseVisualStyleBackColor = true;
            this.addEntityButton.Click += new System.EventHandler(this.addEntityButton_Click);
            // 
            // removeEntityButton
            // 
            this.removeEntityButton.Location = new System.Drawing.Point(108, 238);
            this.removeEntityButton.Name = "removeEntityButton";
            this.removeEntityButton.Size = new System.Drawing.Size(64, 23);
            this.removeEntityButton.TabIndex = 5;
            this.removeEntityButton.Text = "Remove";
            this.removeEntityButton.UseVisualStyleBackColor = true;
            this.removeEntityButton.Click += new System.EventHandler(this.removeEntityButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(385, 531);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(466, 531);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // parameterNameLabel
            // 
            this.parameterNameLabel.AutoSize = true;
            this.parameterNameLabel.Location = new System.Drawing.Point(184, 150);
            this.parameterNameLabel.Name = "parameterNameLabel";
            this.parameterNameLabel.Size = new System.Drawing.Size(55, 13);
            this.parameterNameLabel.TabIndex = 8;
            this.parameterNameLabel.Text = "Parameter";
            // 
            // parameterNameTextBox
            // 
            this.parameterNameTextBox.Location = new System.Drawing.Point(65, 34);
            this.parameterNameTextBox.Name = "parameterNameTextBox";
            this.parameterNameTextBox.Size = new System.Drawing.Size(162, 20);
            this.parameterNameTextBox.TabIndex = 9;
            // 
            // typeDropDown
            // 
            this.typeDropDown.FormattingEnabled = true;
            this.typeDropDown.Items.AddRange(new object[] {
            "Entity",
            "Numeric",
            "String"});
            this.typeDropDown.Location = new System.Drawing.Point(65, 61);
            this.typeDropDown.Name = "typeDropDown";
            this.typeDropDown.Size = new System.Drawing.Size(162, 21);
            this.typeDropDown.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Type";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.hierarchyTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.addEntityButton);
            this.splitContainer1.Panel1.Controls.Add(this.removeEntityButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.parameterLabel);
            this.splitContainer1.Panel2.Controls.Add(this.removeParameterButton);
            this.splitContainer1.Panel2.Controls.Add(this.addParameterButton);
            this.splitContainer1.Panel2.Controls.Add(this.entityNameLabel);
            this.splitContainer1.Panel2.Controls.Add(this.parameterNameTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.nameTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.typeDropDown);
            this.splitContainer1.Panel2.Controls.Add(this.parameterListView);
            this.splitContainer1.Size = new System.Drawing.Size(529, 513);
            this.splitContainer1.SplitterDistance = 175;
            this.splitContainer1.TabIndex = 12;
            // 
            // parameterLabel
            // 
            this.parameterLabel.AutoSize = true;
            this.parameterLabel.Location = new System.Drawing.Point(4, 37);
            this.parameterLabel.Name = "parameterLabel";
            this.parameterLabel.Size = new System.Drawing.Size(55, 13);
            this.parameterLabel.TabIndex = 14;
            this.parameterLabel.Text = "Parameter";
            // 
            // removeParameterButton
            // 
            this.removeParameterButton.Location = new System.Drawing.Point(233, 59);
            this.removeParameterButton.Name = "removeParameterButton";
            this.removeParameterButton.Size = new System.Drawing.Size(113, 23);
            this.removeParameterButton.TabIndex = 13;
            this.removeParameterButton.Text = "Remove";
            this.removeParameterButton.UseVisualStyleBackColor = true;
            this.removeParameterButton.Click += new System.EventHandler(this.removeParameterButton_Click);
            // 
            // addParameterButton
            // 
            this.addParameterButton.Location = new System.Drawing.Point(233, 32);
            this.addParameterButton.Name = "addParameterButton";
            this.addParameterButton.Size = new System.Drawing.Size(113, 23);
            this.addParameterButton.TabIndex = 12;
            this.addParameterButton.Text = "Add Parameter";
            this.addParameterButton.UseVisualStyleBackColor = true;
            this.addParameterButton.Click += new System.EventHandler(this.addParameterButton_Click);
            // 
            // EntityCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 566);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.parameterNameLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "EntityCreator";
            this.Text = "EntityCreator";
            this.Load += new System.EventHandler(this.EntityCreator_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView hierarchyTreeView;
        private System.Windows.Forms.Label entityNameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ListView parameterListView;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.Button addEntityButton;
        private System.Windows.Forms.Button removeEntityButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ColumnHeader typeHeader;
        private System.Windows.Forms.Label parameterNameLabel;
        private System.Windows.Forms.TextBox parameterNameTextBox;
        private System.Windows.Forms.ComboBox typeDropDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button removeParameterButton;
        private System.Windows.Forms.Button addParameterButton;
        private System.Windows.Forms.Label parameterLabel;
    }
}