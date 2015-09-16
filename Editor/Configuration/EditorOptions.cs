using System;
using System.Windows.Forms;

namespace Editor.Configuration
{
    public partial class EditorOptions : Form
    {

        public EditorOptions()
        {
            InitializeComponent();

            graphicsDirectoryTextBox.Text = IniFile.Configuration.IniReadValue("Content", "GraphicsDirectory");
            entityDirectoryTextBox.Text = IniFile.Configuration.IniReadValue("Content", "EntityDirectory");

            openEntityFile.InitialDirectory = Environment.CurrentDirectory;
        }

        private void selectContentDirectoryButton_Click(object sender, EventArgs e)
        {
            selectContentDirectoryFolderDialog.ShowDialog();

            graphicsDirectoryTextBox.Text = selectContentDirectoryFolderDialog.SelectedPath;
        }

        public string GraphicsDirectory
        {
            get { return graphicsDirectoryTextBox.Text; }
        }

        public string EntitiesFile
        {
            get { return IniFile.Configuration.IniReadValue("Content", "EntityDirectory"); }
        }

        private void editorOptionsOKButton_Click(object sender, EventArgs e)
        {
            if(graphicsDirectoryTextBox.Text != string.Empty)
            {
                IniFile.Configuration.IniWriteValue("Content", "GraphicsDirectory", graphicsDirectoryTextBox.Text);
            }

            if(entityDirectoryTextBox.Text != string.Empty)
            {
                IniFile.Configuration.IniWriteValue("Content", "EntityDirectory", entityDirectoryTextBox.Text);
            }

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void editorOptionsCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void entityDirectoryButton_Click(object sender, EventArgs e)
        {
            openEntityFile.ShowDialog();

            entityDirectoryTextBox.Text = openEntityFile.FileName;
        }
    }
}
