using System;
using System.Windows.Forms;
using JumpNRunShared.WorldObjects.Level;

namespace Editor
{
    public partial class MapProperties : Form
    {
        public delegate void CloseHandeler();
        public event CloseHandeler OnClose;

        public MapProperties(Level level)
        {
            InitializeComponent();
            NameTextBox  = level.Name;
        }

        public string NameTextBox
        {
            get { return nameTextBox.Text; }
            set { nameTextBox.Text = value; }
        }

        private void MapProperties_Load(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            CloseHandeler close = OnClose;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
