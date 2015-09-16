using System;
using System.Collections.Generic;
using System.Xml.XPath;
using JumpNRunShared.ObjectShell.ObjectShellFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using SimplePhysicsAndCollision;

namespace JumpNRunShared.WorldObjects.Level
{

    using System;
    using System.Collections;
    using System.Data;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;
    using System.Text;

    public class XMLValidator
    {
        // Validation Error Count
        static int ErrorsCount = 0;

        // Validation Error Message
        static string ErrorMessage = "";

        public static void ValidationHandler(object sender,
                                             ValidationEventArgs args)
        {
            ErrorMessage = ErrorMessage + args.Message + "\r\n";
            ErrorsCount++;
        }

        
    }

    public class LeLevelBuilder2
    {
        public void BuildLevel(ref GameStateManager gamestatemanager, string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XPathNavigator navigator = xmlDocument.CreateNavigator();

            XPathExpression xpath = navigator.Compile("/Level/Structure/Layer");

            XPathNodeIterator iterator = navigator.Select(xpath);

            Dictionary<string, Queue<string>> dependencyMap = new Dictionary<string,Queue<string>>();

            try
            {
                while(iterator.MoveNext())
                {
                    XPathNavigator element = iterator.Current;
                    LoadLayer(element, ref dependencyMap);
                } 
            }
            catch(XPathException xpe)
            {
                    
            }
        }

        private void LoadLayer(XPathNavigator element, ref Dictionary<string, Queue<string>> dependencyMap)
        {
            string layerName = element.GetAttribute("Name", null);
            int layerDepth = Int32.Parse(element.GetAttribute("Layer", null));


        }
    }

    public class LevelBuilder
    {
        public Level level;
        
        #region Default

        /// <summary>
        /// Builds the level from a xml file
        /// </summary>
        /// <param name="contentManager">the contentmanager to load textures and other assets</param>
        /// <param name="pathToLevel">the path to the xml file</param>
        /// <returns>a level according to the xml file</returns>
        public Level BuildLevel(ContentManager contentManager, string pathToLevel)
        {
            //Fill the level with information from the xml
            LoadFromFile(pathToLevel);

            //Load the textures for each level entity
            foreach (LevelBlock entity in level.Entities)
            {
                entity.Load(contentManager);
            }

            level.background.Load(contentManager);

            //return the new level
            return level;
        }

        public void BuildLevelNew(ContentManager contentManager, GameStateManager movementManager)
        {
            
        }

        /// <summary>
        /// Saves the level, given as a parameter, to a xml file
        /// </summary>
        /// <param name="pathToLevel">The path where the level needs to be saved to</param>
        /// <param name="level">the level that needs to be saved</param>
        public void SaveLevel(string pathToLevel, Level level)
        {
            //Read from an XML file
            this.level = level;
            XmlDocument document = new XmlDocument();
            
            //Create a new document
            document.CreateXmlDeclaration("1.0", "utf-8", "yes");

            
            //Create the root element
            XmlElement root = document.CreateElement("Level");


            //Save SpawnPosition
            XmlNode spawnPosition = WriteVector2(document, "SpawnPosition", level.SpawnPosition);
            root.AppendChild(spawnPosition);

            //Set LevelDefinitions
            XmlNode levelDefinition = WriteLevelDefinition(document);

            root.AppendChild(levelDefinition);   

            root.AppendChild(WriteBackground(level.background, document));

            //Go through the level and save its elements to the root
            WriteNodes(root, document);

            //Add the root to the document
            document.AppendChild(root);

            //Save the document
            document.Save(@pathToLevel + "\\" + level.Name +".xml");
        }

        

        
        /// <summary>
        /// Starts loading a level from an xml into the level object
        /// </summary>
        /// <param name="path">the path to the xml</param>
        private void LoadFromFile(string path)
        {
            //Load from an XML file
            XmlDocument document = new XmlDocument();
            level = new Level();

            //Load the xml information into the current document
            document.Load(@path);
            
            XmlElement root = document.DocumentElement;


            ReadNodes(root);
        }

        

        #endregion - Default

        #region Read


        /// <summary>
        /// Reads the information out of the xml into it's specific containers
        /// </summary>
        /// <param name="element">the root element of the level</param>
        private void ReadNodes(XmlElement element)
        {
            //Go through each element
            foreach (XmlElement xmlElement in element.ChildNodes)
            {
                //Switch according to the elements name
                switch (xmlElement.Name)
                {
                    case ("Floor"):
                        //Read the information for the floor
                        ReadLevelBlock(xmlElement.ChildNodes, level.Entities);
                        break;
                    case ("Entities"):
                        //Read the information for the entities
                        ReadLevelBlock(xmlElement.ChildNodes, level.Entities);
                        break;
                    case ("Spawnposition"):
                        //Set the spawnposition
                        level.SpawnPosition = ReadVector2(xmlElement);
                        break;
                    case ("Background"):
                        //Create the background
                        level.background = ReadBackground(xmlElement);
                        break;
                    case ("Name"):
                        //Set the name of the level
                        level.Name = xmlElement.InnerText;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Reads a xmlElement and returns a Background
        /// </summary>
        /// <param name="xmlElement">the xml element to be converted into a background</param>
        /// <returns>background</returns>
        private Background ReadBackground(XmlElement xmlElement)
        {
            string textureAsset = xmlElement["TextureAsset"].InnerText;
            Vector2 screenPosition = ReadVector2(xmlElement["ScreenPosition"]);

            return new Background(textureAsset, screenPosition, true);
        }

        /// <summary>
        /// converts a XmlNode into a Vector2
        /// </summary>
        /// <param name="xmlElement">a XmlNode, containing 2 int-Values, separated by a space (' ')</param>
        /// <returns>a Vector2</returns>
        private Vector2 ReadVector2(XmlNode xmlElement)
        {
            string value = xmlElement.InnerText;
            string[] values = null;
            values = value.Split(' ');

            return new Vector2(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
        }

        /// <summary>
        /// Converts a XmlNode into a boolean
        /// </summary>
        /// <param name="xmlNode">a XmlNode containing either 'true' or 'false'</param>
        /// <returns></returns>
        private bool ReadBoolean(XmlNode xmlNode)
        {
            if (xmlNode.InnerText == "true")
            {
                return true;
            }

            return false;
        }

        private void ReadLevelBlock(XmlNodeList nodes, List<LevelBlock> blocks)
        {
            foreach (XmlNode node in nodes)
            {
                string textureAsset = node["TextureAsset"].InnerText;
                Vector2 position = ReadVector2(node["Position"]);

                LevelBlock currentBlock = new LevelBlock(textureAsset, position);
                blocks.Add(currentBlock);
            }
        }


        #endregion - Read

        #region Write

        /// <summary>
        /// Writes the level elements into their specific list
        /// </summary>
        /// <param name="xmlElement">the root of the xml document</param>
        /// <param name="xmlDocument">the current xml document</param>
        private void WriteNodes(XmlElement xmlElement, XmlDocument xmlDocument)
        {
            XmlNode floorNode = xmlDocument.CreateNode(XmlNodeType.Element, "Floor", null);
            foreach (LevelBlock levelBlock in level.Entities)
            {
                XmlNode item = xmlDocument.CreateNode(XmlNodeType.Element, "Item", null);
                WriteLevelBlock(item, levelBlock, xmlDocument);
                floorNode.AppendChild(item);
            }
            xmlElement.AppendChild(floorNode);

            XmlNode entitiesNode = xmlDocument.CreateNode(XmlNodeType.Element, "Entities", null);
            foreach (LevelBlock entity in level.Entities)
            {
                XmlNode item = xmlDocument.CreateNode(XmlNodeType.Element, "Item", null);
                WriteLevelBlock(item, entity, xmlDocument);
                entitiesNode.AppendChild(item);
            }
            xmlElement.AppendChild(entitiesNode);
        }

        private XmlNode WriteLevelDefinition(XmlDocument document)
        {
            XmlNode levelDefinition = document.CreateNode(XmlNodeType.Element, "LevelDefinition", null);

            //Create Level Name
            XmlNode name = document.CreateNode(XmlNodeType.Element, "Name", null);
            name.InnerText = level.Name;
            levelDefinition.AppendChild(name);

            //Create Level version
            XmlNode version = document.CreateNode(XmlNodeType.Element, "Version", null);
            version.InnerText = "0.1";
            levelDefinition.AppendChild(version);

            //The creator of the map
            XmlNode author = document.CreateNode(XmlNodeType.Element, "Author", null);
            author.InnerText = "Rosthouse";
            levelDefinition.AppendChild(author);

            //Wanna write a mail to the author?
            XmlNode authorMail = document.CreateNode(XmlNodeType.Element, "AuthorMail", null);
            authorMail.InnerText = "rosthouse@gmail.com";
            levelDefinition.AppendChild(authorMail);

            //Next Level
            XmlNode nextLevel = document.CreateNode(XmlNodeType.Element, "NextLevel", null);
            nextLevel.InnerText = string.Empty;
            levelDefinition.AppendChild(authorMail);

            //And finished is the level definition
            return levelDefinition;
        }

        public XmlNode WriteBackground(Background background, XmlDocument xmlDocument)
        {
            XmlNode backgroundNode = xmlDocument.CreateNode(XmlNodeType.Element, "Background", null);

            XmlNode origin = WriteVector2(xmlDocument, "ScreenPosition", background.Origin);
            XmlNode textureAsset = xmlDocument.CreateNode(XmlNodeType.Element, "TextureAsset", null);

            textureAsset.InnerText = background.TextureAsset;

            backgroundNode.AppendChild(origin);
            backgroundNode.AppendChild(textureAsset);

            return backgroundNode;
        }

        private void WriteLevelBlock(XmlNode node, LevelBlock levelBlock, XmlDocument xmlDocument)
        {
            XmlNode textureAsset = xmlDocument.CreateNode(XmlNodeType.Element, "TextureAsset", null);
            textureAsset.InnerText = levelBlock.TextureAsset;

            XmlNode isVisible = WriteBool(xmlDocument, "isVisible", levelBlock.IsVisible);

            XmlNode position = WriteVector2(xmlDocument, "Position", levelBlock.Position);

            node.AppendChild(textureAsset);
            node.AppendChild(isVisible);
            node.AppendChild(position);
        }

        private XmlNode WriteItem(WorldObject worldObject, XmlDocument document)
        {
            //To this node we want to write the worldObject into
            XmlNode item = document.CreateNode(XmlNodeType.Element, "Item", null);

            //Write the position of the object
            XmlNode position = WriteVector2(document, "Position", worldObject.Position);
            item.AppendChild(position);

            //Wheter the object is visible during the game
            XmlNode isVisible = document.CreateNode(XmlNodeType.Element, "IsVisible", null);
            isVisible.InnerText = "true";
            item.AppendChild(isVisible);

            //The drawing layer
            XmlNode layer = document.CreateNode(XmlNodeType.Element, "Layer", null);
            layer.InnerText = "0";
            item.AppendChild(layer);

            //List<Parameter> parameterList = worldObject.GetParameterList();



            return item;
        }

        private XmlNode WriteVector2(XmlDocument document, string name, Vector2 vector2)
        {
            XmlNode position = document.CreateNode(XmlNodeType.Element, name, null);
            position.InnerText = vector2.X + " " + vector2.Y;
            return position;
        }

        private XmlNode WriteBool(XmlDocument document, string name, bool b)
        {
            XmlNode boolToWrite = document.CreateNode(XmlNodeType.Element, name, null);

            if (b)
            {
                boolToWrite.InnerText = "true";
                return boolToWrite;
            }

            boolToWrite.InnerText = "false";
            return boolToWrite;
        }

        #endregion - Write
    }
}