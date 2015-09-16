using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Editor.Configuration;
using Editor.EditorObjects;
using JumpNRunShared;
using JumpNRunShared.WorldObjects;
using JumpNRunShared.WorldObjects.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimplePhysicsAndCollision;
using Rectangle=Microsoft.Xna.Framework.Rectangle;
using XNAColor = Microsoft.Xna.Framework.Color;

namespace Editor
{
    public partial class MapEditor : Form
    {
        #region Members

        //Forms
        private EntityCreator creator;
        private EditorOptions editorOptions;
        private XnaForms xna;
        private static MapProperties mapProperties;
        public MapProperties.CloseHandeler closeHandeler;

        //Xna specific
        private SpriteBatch spriteBatch;
        private Size previousSize;
        private Camera camera;

        //Mouse
        private Vector2 mousePosition;
        private Vector2 prevMousePosition;

        //Level
        private LevelBuilder levelBuilder;
        public List<WorldObject> selectedItems;
        
        //Screen
        private int screenHeight;
        
        //Selection
        private PictureBox currentSelectedTexture;
        
        //Game specific
        private GameStateManager gameStateManager;
        private MovementManager movementManager;

        //Mouse Handeling
        private MouseButtons mouseButtonDown;

        #endregion - Members

        #region Initialization

        public MapEditor()
        {
            InitializeComponent();

            //Find config file
            IniFile.SetConfigurationPath( @".\MapEditor.ini");

            //Load the editor options
            editorOptions = new EditorOptions();

            //Load the entity creator
            creator = new EntityCreator(objectTree.Nodes);
            

            //Create a new xna window to render into
            xna = new XnaForms(pnlOutput.Handle, pnlOutput.Width, pnlOutput.Height);
            xna.Content.RootDirectory = @"Content";

            camera = new Camera();

            spriteBatch = new SpriteBatch(xna.GraphicsDevice);

            //Adjust the current size
            previousSize = Size;

            levelBuilder = new LevelBuilder();
            ScreenHeight = 800;

            gameStateManager = new GameStateManager(xna.Content, false);
            movementManager = new MovementManager();
            gameStateManager.setMovementManager(movementManager);

            selectedItems = new List<WorldObject>();

            closeHandeler += GetMapProperties;

            //Mouse handeling initializing
            mousePosition = Vector2.Zero;
            prevMousePosition = Vector2.Zero;
            mouseButtonDown = MouseButtons.None;


            Level level = new Level();

            gameStateManager.Level = level;


            LoadTextures();


            RenderFrame();
        }

        public void LoadTextures()
        {
            if (editorOptions.GraphicsDirectory == string.Empty)
                return;

            if(!Directory.Exists(editorOptions.GraphicsDirectory))
            {
                MessageBox.Show("The directory you selected doesn't exits. Please choose another directory");
                return;
            }
                
            textureBox.Controls.Clear();

            DirectoryInfo di = new DirectoryInfo(editorOptions.GraphicsDirectory);

            var fileEnumerator = di.EnumerateFiles();


            foreach (FileInfo file in fileEnumerator)
            {
                if (file.Extension == ".jpg" || file.Extension == ".png" || file.Extension == ".gif")
                {
                    PictureBox texture = new PictureBox();
                    texture.ImageLocation = file.FullName;
                    texture.Size = new System.Drawing.Size(50, 50);

                    texture.SizeMode = PictureBoxSizeMode.CenterImage;

                    texture.Click += TextureClick;



                    textureBox.Controls.Add(texture);
                }
            }

            
        }

        #endregion initialization

        #region Accessors

        public void GetMapProperties()
        {
            gameStateManager.Level.Name = mapProperties.NameTextBox;
        }

        public int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }

        #endregion - Accessors

        #region Events

        #region MouseEvents

        private void pnlOutput_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = (MouseEventArgs)e;

            Rectangle mousePosition = new Rectangle((int)(mouseEventArgs.X / camera.Zoom), (int)(mouseEventArgs.Y / camera.Zoom), 1, 1);


            foreach (WorldObject worldObject in gameStateManager.Level.VisibleObjects)
            {
                if (mousePosition.Intersects(worldObject.IntersectRectangle))
                {
                    selectedItems.Add(worldObject);
                }
            }

            if (selectedItems.Count == 0 && mouseEventArgs.Button == MouseButtons.Left)
            {

                selectedItems.Clear();
                Vector2 mouseVector = new Vector2(mouseEventArgs.X + camera.Position.X, mouseEventArgs.Y + camera.Position.Y);

                //HACK: I have no idea why this works. I multiply 30 (the offset of the camera, don't know where that comes from) by the camera Zoom and add that to the X factor of the mouseVector.
                //The fuck?
                mouseVector.X -= 30 * camera.Zoom;

                AddLevelObject(Vector2.Divide(mouseVector, camera.Zoom));
            }

            RenderFrame();
        }

        private void pnlOutput_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = new Vector2(e.X, e.Y); //In order to keep the position of the mouse equal to the XNA position, we have to divide with the cameras zoom
            mousePositionLabel.Text = "X: " + Math.Round((mousePosition.X + camera.Position.X) / camera.Zoom) + "; Y: " + Math.Round((mousePosition.Y + camera.Position.Y) / camera.Zoom);

            switch (mouseButtonDown)
            {
                case MouseButtons.Left:
                    //A selection rectangle should be drawn here. As soon as the user lets go, we use the rectangle to query objets in pnlOutput_MouseUp. The rectangle has to be cached somewhere

                    break;
                case MouseButtons.Right:
                    Vector2 result = mousePosition - prevMousePosition;
                    camera.Move(result);
                    RenderFrame();
                    break;
                default:
                    break;
            }

            prevMousePosition = mousePosition;
        }

        private void pnlOutput_MouseDown(object sender, MouseEventArgs e)
        {
            mouseButtonDown = e.Button;
        }

        private void pnlOutput_MouseUp(object sender, MouseEventArgs e)
        {
            mouseButtonDown = MouseButtons.None;
        }

        #endregion - MouseEvents

        #region ToolStrip

        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameStateManager.Level = new Level();
            RenderFrame();
        }

        private void loadLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();


            string file = openFileDialog.FileName;

            if (file != null)
            {
                gameStateManager.Level = levelBuilder.BuildLevel(xna.Content, file);
            }
        }

        private void saveLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            levelBuilder.SaveLevel(@"C:\", gameStateManager.Level);
        }

        private void mapPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapProperties = new MapProperties(gameStateManager.Level);
            mapProperties.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = editorOptions.ShowDialog();

            if(result == DialogResult.OK)
            {
                LoadTextures();

                SetNodes(creator.Nodes);
            }

            
        }

        private void newEntityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = creator.ShowDialog();

            if (result == DialogResult.OK)
            {
                SetNodes(creator.Nodes);
            }
        }

        #endregion ToolStrip

        #region FormEvents

        private void TextureClick(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;

            if (args.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (currentSelectedTexture != null)
                    currentSelectedTexture.BorderStyle = BorderStyle.None;

                currentSelectedTexture = (PictureBox)sender;

                currentSelectedTexture.Select();
                currentSelectedTexture.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (args.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //Todo: Yeah, we should like totaly add something to edit textures and stuff
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            RenderFrame();
        }

        private void ObjectsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void zoomUpDown_ValueChanged(object sender, EventArgs e)
        {
            camera.Zoom = (float)decimal.ToDouble(zoomUpDown.Value);
            RenderFrame();
        }

        private void objectTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string selectedNodeName = e.Node.Text;

            if (!creator.EntityDictionary.ContainsKey(selectedNodeName))
                return;


            Entity entity = creator.EntityDictionary[selectedNodeName];

            Panel entityPanel = new Panel();

            foreach (Entity.Parameter parameter in entity.Parameters)
            {
                Label parameterLabel = new Label();
                parameterLabel.Text = parameter.Name;

                entityPanel.Controls.Add(parameterLabel);

                switch (parameter.Type)
                {
                    case ParameterType.String:
                        TextBox stringBox = new TextBox();
                        entityPanel.Controls.Add(stringBox);
                        break;
                    case ParameterType.Object:
                        TextBox objectBox = new TextBox();
                        objectBox.ReadOnly = true;
                        entityPanel.Controls.Add(objectBox);
                        break;
                    case ParameterType.Numeric:
                        NumericUpDown upDown = new NumericUpDown();
                        entityPanel.Controls.Add(upDown);
                        break;
                }

            }

            BlockProperties.Controls.Add(entityPanel);
        }

        #endregion - FormEvents


        private void Form1_Resize(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            Size changed = new Size(form.Size.Width - previousSize.Width, form.Size.Height - previousSize.Height);


            pnlOutput.Size = new Size(pnlOutput.Size.Width + changed.Width, pnlOutput.Size.Height + changed.Height);

            if (pnlOutput.Size.Height > ScreenHeight)
            {
                pnlOutput.Size = new Size(pnlOutput.Size.Width, ScreenHeight);
            }

            xna.Resize(pnlOutput.Width, pnlOutput.Height);
            previousSize = this.Size;
            RenderFrame();
        }

        #endregion Events

        #region Rendering

        public void RenderFrame()
        {
            xna.GraphicsDevice.Clear(XNAColor.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.GetTransformation(xna.GraphicsDevice));

            foreach (WorldObject worldObject in gameStateManager.WorldObjects)
            {
                worldObject.Draw(xna.GameTime, spriteBatch, 3);
            }

            if (gameStateManager.Level != null)
            {
                
                gameStateManager.Level.Draw(xna.GameTime, spriteBatch, 3);

                foreach (WorldObject selectedItem in selectedItems)
                {
                    Rectangle rect = new Rectangle((int)selectedItem.Position.X, (int)selectedItem.Position.Y,
                                                   selectedItem.Texture.Width, selectedItem.Texture.Height);

                }
            }


            spriteBatch.End();

            xna.GraphicsDevice.Present();
        }

        public void DrawArrows(SpriteBatch spriteBatch, Vector2 mousePosition)
        {
            WorldObject worldObject = GetSelectedWorldObject(mousePosition);

            if (worldObject != null)
            {

            }
        }

        #endregion - Rendering

        #region ObjectHandeling

        /// <summary>
        /// Adds a new object to the level
        /// </summary>
        /// <param name="position">The vector on the level, NOT the mouse vector. That will fail. Seriously</param>
        private void AddLevelObject(Vector2 position)
        {
            if(currentSelectedTexture != null)
            {
                EditorObject levelBlock = new EditorObject(string.Empty, position);

                using(FileStream textureStream = new FileStream(currentSelectedTexture.ImageLocation, FileMode.Open)){
                    levelBlock.Texture = Texture2D.FromStream(xna.GraphicsDevice, textureStream);
                }

                

                gameStateManager.AddExternalObject(levelBlock);
                gameStateManager.AdjustPosition(levelBlock);
            }
            else
            {
                MessageBox.Show("How about selecting a texture, genious?");
            }
            return;
        }


        public WorldObject GetSelectedWorldObject(Vector2 point)
        {
            foreach (WorldObject worldObject in gameStateManager.Level.Entities)
            {
                if (worldObject.IntersectRectangle.Intersects(new Rectangle((int)point.X, (int)point.Y, 1, 1)))
                {
                    return worldObject;
                }
            }

            return null;
        }

        #endregion ObjectHandeling

        #region HelperMethods

        private void SetNodes(TreeNodeCollection treeNodeCollection)
        {
            objectTree.Nodes.Clear();


            for (int i = 0; i < creator.Nodes.Count; i++)
            {
                TreeNode node = treeNodeCollection[i];
                creator.Nodes.Remove(node);

                objectTree.Nodes.Add(node);

            }
        }

        #endregion - HelperMethods
    }
}

