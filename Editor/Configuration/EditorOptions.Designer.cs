namespace Editor.Configuration
{
    partial class EditorOptions
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contentTab = new System.Windows.Forms.TabPage();
            this.entityDirectoryButton = new System.Windows.Forms.Button();
            this.entityDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.entityDirectoryLabel = new System.Windows.Forms.Label();
            this.selectContentDirectoryButton = new System.Windows.Forms.Button();
            this.graphicsDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.graphicsDirectoryName = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.selectContentDirectoryFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.editorOptionsOKButton = new System.Windows.Forms.Button();
            this.editorOptionsCancelButton = new System.Windows.Forms.Button();
            this.openEntityFile = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.contentTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.contentTab);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(430, 302);
            this.tabControl1.TabIndex = 0;
            // 
            // contentTab
            // 
            this.contentTab.Controls.Add(this.entityDirectoryButton);
            this.contentTab.Controls.Add(this.entityDirectoryTextBox);
            this.contentTab.Controls.Add(this.entityDirectoryLabel);
            this.contentTab.Controls.Add(this.selectContentDirectoryButton);
            this.contentTab.Controls.Add(this.graphicsDirectoryTextBox);
            this.contentTab.Controls.Add(this.graphicsDirectoryName);
            this.contentTab.Location = new System.Drawing.Point(4, 22);
            this.contentTab.Name = "contentTab";
            this.contentTab.Padding = new System.Windows.Forms.Padding(3);
            this.contentTab.Size = new System.Drawing.Size(422, 276);
            this.contentTab.TabIndex = 0;
            this.contentTab.Text = "Content";
            this.contentTab.UseVisualStyleBackColor = true;
            // 
            // entityDirectoryButton
            // 
            this.entityDirectoryButton.Location = new System.Drawing.Point(341, 31);
            this.entityDirectoryButton.Name = "entityDirectoryButton";
            this.entityDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.entityDirectoryButton.TabIndex = 5;
            this.entityDirectoryButton.Text = "Select";
            this.entityDirectoryButton.UseVisualStyleBackColor = true;
            this.entityDirectoryButton.Click += new System.EventHandler(this.entityDirectoryButton_Click);
            // 
            // entityDirectoryTextBox
            // 
            this.entityDirectoryTextBox.Location = new System.Drawing.Point(105, 33);
            this.entityDirectoryTextBox.Name = "entityDirectoryTextBox";
            this.entityDirectoryTextBox.Size = new System.Drawing.Size(230, 20);
            this.entityDirectoryTextBox.TabIndex = 4;
            // 
            // entityDirectoryLabel
            // 
            this.entityDirectoryLabel.AutoSize = true;
            this.entityDirectoryLabel.Location = new System.Drawing.Point(13, 36);
            this.entityDirectoryLabel.Name = "entityDirectoryLabel";
            this.entityDirectoryLabel.Size = new System.Drawing.Size(78, 13);
            this.entityDirectoryLabel.TabIndex = 3;
            this.entityDirectoryLabel.Text = "Entity Directory";
            // 
            // selectContentDirectoryButton
            // 
            this.selectContentDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectContentDirectoryButton.Location = new System.Drawing.Point(341, 5);
            this.selectContentDirectoryButton.Name = "selectContentDirectoryButton";
            this.selectContentDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.selectContentDirectoryButton.TabIndex = 2;
            this.selectContentDirectoryButton.Text = "Select";
            this.selectContentDirectoryButton.UseVisualStyleBackColor = true;
            this.selectContentDirectoryButton.Click += new System.EventHandler(this.selectContentDirectoryButton_Click);
            // 
            // graphicsDirectoryTextBox
            // 
            this.graphicsDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.graphicsDirectoryTextBox.Location = new System.Drawing.Point(105, 7);
            this.graphicsDirectoryTextBox.Name = "graphicsDirectoryTextBox";
            this.graphicsDirectoryTextBox.Size = new System.Drawing.Size(233, 20);
            this.graphicsDirectoryTextBox.TabIndex = 1;
            // 
            // graphicsDirectoryName
            // 
            this.graphicsDirectoryName.AutoSize = true;
            this.graphicsDirectoryName.Location = new System.Drawing.Point(10, 10);
            this.graphicsDirectoryName.Name = "graphicsDirectoryName";
            this.graphicsDirectoryName.Size = new System.Drawing.Size(94, 13);
            this.graphicsDirectoryName.TabIndex = 0;
            this.graphicsDirectoryName.Text = "Graphics Directory";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(422, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // editorOptionsOKButton
            // 
            this.editorOptionsOKButton.Location = new System.Drawing.Point(267, 308);
            this.editorOptionsOKButton.Name = "editorOptionsOKButton";
            this.editorOptionsOKButton.Size = new System.Drawing.Size(75, 23);
            this.editorOptionsOKButton.TabIndex = 1;
            this.editorOptionsOKButton.Text = "OK";
            this.editorOptionsOKButton.UseVisualStyleBackColor = true;
            this.editorOptionsOKButton.Click += new System.EventHandler(this.editorOptionsOKButton_Click);
            // 
            // editorOptionsCancelButton
            // 
            this.editorOptionsCancelButton.Location = new System.Drawing.Point(351, 308);
            this.editorOptionsCancelButton.Name = "editorOptionsCancelButton";
            this.editorOptionsCancelButton.Size = new System.Drawing.Size(75, 23);
            this.editorOptionsCancelButton.TabIndex = 2;
            this.editorOptionsCancelButton.Text = "Cancel";
            this.editorOptionsCancelButton.UseVisualStyleBackColor = true;
            this.editorOptionsCancelButton.Click += new System.EventHandler(this.editorOptionsCancelButton_Click);
            // 
            // openEntityFile
            // 
            this.openEntityFile.FileName = "openFileDialog1";
            // 
            // EditorOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 343);
            this.Controls.Add(this.editorOptionsCancelButton);
            this.Controls.Add(this.editorOptionsOKButton);
            this.Controls.Add(this.tabControl1);
            this.Name = "EditorOptions";
            this.Text = "EditorOptions";
            this.tabControl1.ResumeLayout(false);
            this.contentTab.ResumeLayout(false);
            this.contentTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage contentTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button selectContentDirectoryButton;
        private System.Windows.Forms.TextBox graphicsDirectoryTextBox;
        private System.Windows.Forms.Label graphicsDirectoryName;
        private System.Windows.Forms.FolderBrowserDialog selectContentDirectoryFolderDialog;
        private System.Windows.Forms.Button editorOptionsOKButton;
        private System.Windows.Forms.Button editorOptionsCancelButton;
        private System.Windows.Forms.Button entityDirectoryButton;
        private System.Windows.Forms.TextBox entityDirectoryTextBox;
        private System.Windows.Forms.Label entityDirectoryLabel;
        private System.Windows.Forms.OpenFileDialog openEntityFile;
    }
}