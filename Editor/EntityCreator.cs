using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Editor.Configuration;

namespace Editor
{
    public partial class EntityCreator : Form
    {
        private Dictionary<string, Entity> entityDictionary;
        private string entityDirectory;

        public EntityCreator(TreeNodeCollection nodes)
        {
            InitializeComponent();

            entityDictionary = new Dictionary<string, Entity>();

            entityDirectory = IniFile.Configuration.IniReadValue("Content", "EntityDirectory");

            if(entityDirectory != string.Empty)
            {
                LoadEntities(entityDirectory);
            }

            
        }

        public TreeNodeCollection Nodes
        {
            get { return hierarchyTreeView.Nodes; }
        }

        public Dictionary<string, Entity> EntityDictionary
        {
            get { return entityDictionary; }
        }

        #region Form Events

        

        #region Buttons

        private void addEntityButton_Click(object sender, EventArgs e)
        {
            TreeNode node;


            if (hierarchyTreeView.SelectedNode != null)
            {
                node = hierarchyTreeView.SelectedNode.Nodes.Add(nameTextBox.Text, nameTextBox.Text);
            }
            else
            {
                if(hierarchyTreeView.TopNode == null)
                {
                    hierarchyTreeView.Nodes.Add("Entities");
                }

                node = hierarchyTreeView.TopNode.Nodes.Add(nameTextBox.Text, nameTextBox.Text);
            }


            Entity newEntity = new Entity();
            newEntity.Name = node.Name;

            List<Entity.Parameter> parameters = new List<Entity.Parameter>();

            foreach (ListViewItem item in parameterListView.Items)
            {
                parameters.Add(createParameter(item.Text, item.SubItems[1].Name));
            }

            newEntity.Parameters = parameters;

            if (EntityDictionary.ContainsKey(newEntity.Name))
            {
                EntityDictionary[newEntity.Name] = newEntity;
            }
            else
            {
                EntityDictionary.Add(newEntity.Name, newEntity);
            }

        }

        private void removeEntityButton_Click(object sender, EventArgs e)
        {
            if (hierarchyTreeView.SelectedNode != null && hierarchyTreeView.SelectedNode != hierarchyTreeView.TopNode)
            {
                EntityDictionary.Remove(hierarchyTreeView.SelectedNode.Name);
                hierarchyTreeView.SelectedNode.Remove();
            }
        }

        private void addParameterButton_Click(object sender, EventArgs e)
        {
            if (parameterNameTextBox.Text == string.Empty)
            {
                MessageBox.Show("No Parameter name given");
                return;
            }

            if (EntityDictionary.ContainsKey(nameTextBox.Text))
            {

                EntityDictionary[nameTextBox.Text].Parameters.Add(createParameter(parameterNameTextBox.Text, typeDropDown.Text));
            }

            addParameterToList(parameterNameTextBox.Text, typeDropDown.Text);


            parameterNameTextBox.Text = string.Empty;


        }

        private void removeParameterButton_Click(object sender, EventArgs e)
        {
            if (parameterListView.SelectedItems.Count != 0)
            {
                //do the remove here
                for (int i = 0; i < parameterListView.SelectedItems.Count; i++)
                {
                    parameterListView.SelectedItems[i].Remove();
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (entityDirectory == string.Empty)
            {
                entityDirectory = Environment.CurrentDirectory;
            }

            Save(entityDirectory);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            this.Close();
        }

        #endregion Buttons

        #region Treeview

        private void hierarchyTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            parameterListView.Items.Clear();

            nameTextBox.Text = e.Node.Name;

            if (EntityDictionary.ContainsKey(e.Node.Text))
            {
                Entity entity = EntityDictionary[e.Node.Text];

                foreach (Entity.Parameter parameter in entity.Parameters)
                {
                    addParameterToList(parameter.Name, parameter.Type.ToString());
                }
            }
        }

        private void parameterListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (parameterListView.SelectedIndices.Count == 1)
            {
                ListViewItem item = parameterListView.Items[parameterListView.SelectedIndices[0]];

                parameterNameTextBox.Text = item.Text;

                typeDropDown.Text = item.SubItems[1].Text;
            }

        }

        #endregion Treeview

        #endregion Form Events

        #region Loading

        public void LoadEntities(string path)
        {
            XmlDocument document = new XmlDocument();
            document.Load(path);

            XmlNode topNode = document.DocumentElement;

            if (topNode.ChildNodes.Count > 0)
            {
                TreeNode treeNode = new TreeNode("Entities");

                foreach (XmlNode childNode in topNode.ChildNodes)
                {
                    ReadEntity(childNode, treeNode);
                }

                hierarchyTreeView.Nodes.Add(treeNode);
            }



        }

        private void ReadEntity(XmlNode nodeToRead, TreeNode parent)
        {
            Entity entity = new Entity();

            entity.Name = nodeToRead.Attributes["Name"].Value;

            TreeNode treeNode = new TreeNode(entity.Name);
            treeNode.Text = entity.Name;

            parent.Nodes.Add(treeNode);

            if (nodeToRead.HasChildNodes)
            {
                foreach (XmlNode childNode in nodeToRead.ChildNodes)
                {
                    switch (childNode.Name)
                    {
                        case "Parameters":
                            ReadParameters(childNode, ref entity);
                            break;
                        case "SubEntities":
                            foreach (XmlNode subEntityNode in childNode.ChildNodes)
                            {
                                ReadEntity(subEntityNode, treeNode);
                            }
                            break;
                    }
                }
            }

            EntityDictionary.Add(entity.Name, entity);
        }

        private void ReadParameters(XmlNode parameterContainerNode, ref Entity entity)
        {
            foreach (XmlNode parameterNode in parameterContainerNode.ChildNodes)
            {
                Entity.Parameter parameter = new Entity.Parameter();

                parameter.Name = parameterNode.InnerText;
                switch (parameterNode.Attributes["Type"].Value)
                {
                    case "Object":
                        parameter.Type = ParameterType.Object;
                        break;
                    case "Numeric":
                        parameter.Type = ParameterType.Numeric;
                        break;
                    case "String":
                        parameter.Type = ParameterType.String;
                        break;
                }



                entity.Parameters.Add(parameter);
            }
        }

        #endregion Loading

        #region Saving

        public void Save(string path)
        {
            XmlDocument document = new XmlDocument();


            document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", "yes"));

            XmlNode root = document.CreateNode(XmlNodeType.Element, "EntitiesStructure", null);

            document.AppendChild(root);
            
            if(hierarchyTreeView.TopNode != null)
            {
                foreach (TreeNode node in hierarchyTreeView.TopNode.Nodes)
                {
                    root.AppendChild(AddSubEntity(node, document));
                }
            }

            


            document.Save(path);

            if (entityDirectory == string.Empty)
            {
                entityDirectory = Environment.CurrentDirectory + @"\Entities.xml";

                IniFile.Configuration.IniWriteValue("Content", "EntityDirectory", entityDirectory);
            }

        }

        public XmlNode AddSubEntity(TreeNode treeNode, XmlDocument document)
        {

            Entity entity = EntityDictionary[treeNode.Text];

            XmlNode xmlNode = document.CreateNode(XmlNodeType.Element, "Item", null);
            XmlAttribute attribute = document.CreateAttribute("Name");
            attribute.Value = entity.Name;

            xmlNode.Attributes.Append(attribute);

            if (entity.Parameters.Count > 0)
            {
                XmlNode parameterContainerNode = document.CreateNode(XmlNodeType.Element, "Parameters", null);


                foreach (Entity.Parameter parameter in entity.Parameters)
                {
                    //Create Node for the parameter
                    XmlNode parameterNode = document.CreateNode(XmlNodeType.Element, "Parameter", null);

                    //Create an attribute containing the type
                    XmlAttribute parameterType = document.CreateAttribute("Type");
                    parameterType.Value = parameter.Type.ToString();

                    //Set the inner text to the name of the parameter
                    parameterNode.InnerText = parameter.Name;

                    //add the attribute to the node
                    parameterNode.Attributes.Append(parameterType);

                    //add the parameter node to the parameterContainer
                    parameterContainerNode.AppendChild(parameterNode);

                }

                xmlNode.AppendChild(parameterContainerNode);
            }


            if (treeNode.Nodes.Count > 0)
            {
                XmlNode subEntities = document.CreateNode(XmlNodeType.Element, "SubEntities", null);

                foreach (TreeNode subTreeNode in treeNode.Nodes)
                {
                    subEntities.AppendChild(AddSubEntity(subTreeNode, document));
                }

                xmlNode.AppendChild(subEntities);
            }

            return xmlNode;
        }

        #endregion Saving

        #region Helper Methods

        private void addParameterToList(string parameterName, string type)
        {
            ListViewItem listViewItem = new ListViewItem(new string[] { parameterName, type });

            if (parameterListView.Items.Contains(listViewItem))
            {
                parameterListView.Items.Remove(listViewItem);
            }

            parameterListView.Items.Add(listViewItem);
        }

        private Entity.Parameter createParameter(string name, string type)
        {
            Entity.Parameter parameter = new Entity.Parameter();

            parameter.Name = name;

            switch (type)
            {
                case "Object":
                    parameter.Type = ParameterType.Object;
                    break;
                case "Numeric":
                    parameter.Type = ParameterType.Numeric;
                    break;
                case "String":
                    parameter.Type = ParameterType.String;
                    break;
            }

            return parameter;
        }

        #endregion Helper Methods

        private void EntityCreator_Load(object sender, EventArgs e)
        {
            entityDictionary = new Dictionary<string, Entity>();

            entityDirectory = IniFile.Configuration.IniReadValue("Content", "EntityDirectory");

            if (entityDirectory != string.Empty)
            {
                if(File.Exists(entityDirectory))
                {
                    LoadEntities(entityDirectory);
                } else
                {
                    MessageBox.Show("Woah, where did the entities go? I sure they were around here somewhere");
                }
                
            }
        }

    }
}
