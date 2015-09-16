using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.WorldObjects.Level
{
    public class Level
    {
        #region Members

        private string name;
        private Vector2 spawnPosition;
        private List<LevelBlock> entities;
        private Vector2 size;
        
        public Background background;

        #endregion - Members

        #region Constructors

        public Level(string name, List<LevelBlock> entities, Vector2 spawnPosition, Background background)
        {
            this.name = name;
            this.entities = entities;
            this.spawnPosition = spawnPosition;
            this.background = background;

            TestXml();
        }

        public Level() : this(null, new List<LevelBlock>(), Vector2.Zero, new Background())
        {
        }
        
        #endregion - Constructors

        #region Accessors

        public List<LevelBlock> VisibleObjects
        {
            get
            {
                List<LevelBlock> toReturn = new List<LevelBlock>();

                foreach (LevelBlock entity in entities)
                {
                    if (entity.IsVisible)
                    {
                        toReturn.Add(entity);
                    }
                }

                return entities;
            }
        }

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        public List<LevelBlock> Entities
        {
            get { return entities; }
            set { entities = value; }
        }

        public Vector2 SpawnPosition
        {
            get { return spawnPosition; }
            set { spawnPosition = value; }
        }

        #endregion - Accessors

        #region Object Logic

        

        public void Update(GameTime gameTime, Rectangle clientBounds, int distance)
        {
            background.Update(gameTime, distance);

            UpdateLevelObjects(gameTime, clientBounds, entities, distance);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int layerDepth)
        {

            background.Draw(gameTime, spriteBatch, layerDepth);

            foreach (LevelBlock block in entities)
            {
                block.Draw(gameTime, spriteBatch, layerDepth - 1);
            }



        }

        public void MoveToEnd(LevelBlock levelBlock, List<LevelBlock> list)
        {
            LevelBlock temp = levelBlock;

            list.Remove(levelBlock);

            temp.Position = new Vector2(list.Last().Position.X + temp.Texture.Width, temp.Position.Y);

            list.Add(temp);
        }

        public void UpdateLevelObjects(GameTime gameTime, Rectangle clientBounds, List<LevelBlock> objects, int distance)
        {
            foreach (LevelBlock levelObject in objects)
            {

                if (levelObject.IntersectRectangle.Intersects(clientBounds))
                {
                    levelObject.IsVisible = true;
                }
                else
                {
                    levelObject.IsVisible = false;
                }

                Vector2 newPosition = new Vector2(levelObject.Position.X - distance, levelObject.Position.Y);
                levelObject.Update(gameTime, newPosition);
            }
        } 

        #endregion - Object Logic

        #region Helper Methods

        public void TestXml()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("HelloWorldLevel.xml", settings))
            {
                //IntermediateSerializer.Serialize(writer, this, null);
            }
        }

        public void AdjustPosition(WorldObject levelObject)
        {
            foreach (WorldObject toCheck in VisibleObjects)
            {
                if (toCheck.IntersectRectangle.Intersects(levelObject.IntersectRectangle))
                {
                    levelObject.Position = new Vector2(toCheck.Position.X + toCheck.Texture.Width,
                                                       levelObject.Position.Y);
                }
            }
        }

        #endregion - Helper Methods

        public Vector2 Size
        {
            get
            {
                if(size == Vector2.Zero)
                {
                    float x = 0f;
                    float y = 0f;
                    int tx = 0;
                    int ty = 0;

                    foreach (LevelBlock levelBlock in entities)
                    {
                        if (levelBlock.Position.X > x)
                        {
                            x = levelBlock.Position.X;
                            tx = levelBlock.Texture.Width;
                        }

                        if (levelBlock.Position.Y > y)
                        {
                            y = levelBlock.Position.Y;
                            ty = levelBlock.Texture.Height;
                        }
                    }

                    size = new Vector2(x + tx, y + ty);
                }
                return this.size;
            }
        }
    }
}