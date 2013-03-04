using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.WorldObjects.Level
{
    public class Background
    {
        #region Members

        private Vector2 screenPosition;
        private Vector2 origin;
        private Texture2D texture;
        private string textureAsset;
        private bool isActive;

        private List<LevelBlock> blocks;

        #endregion - Members

        #region Constructors

        public Background(string textureAsset, Vector2 origin, bool isActive)
        {
            this.isActive = isActive;
            this.TextureAsset = textureAsset;
            this.Origin = origin;
            screenPosition = origin;
        }

        public Background(Texture2D texture, Vector2 origin, string textureAsset, bool isActive): this(textureAsset, origin, isActive)
        {
            this.texture = texture;
        }

        public Background(): this(null, Vector2.Zero, string.Empty, false){}

        #endregion - Constructors


        public string TextureAsset
        {
            get { return textureAsset; }
            set { textureAsset = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public void Update(GameTime gameTime, float distance)
        {
            
            if(distance > 0 && isActive)
            {
                if (PassedScreen(screenPosition))
                {
                    screenPosition = new Vector2(0, 0);
                }


                screenPosition.X -= distance ;
            }
        }

        public bool PassedScreen(Vector2 screenPosition)
        {
            if(screenPosition.X + texture.Width <= 0)
            {
                return true;
            }

            return false;
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int layerDepth)
        {

            if(texture == null)
                return;


            spriteBatch.Draw(texture, screenPosition, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
            if(screenPosition.X < 0)
            {
                Vector2 additionalScreenPosition = new Vector2(screenPosition.X + texture.Width, screenPosition.Y);
                spriteBatch.Draw(texture, additionalScreenPosition, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
            }
        }

        public void Load(ContentManager contentManager)
        {
            if(TextureAsset != string.Empty || TextureAsset != null)
            {
                texture = contentManager.Load<Texture2D>(TextureAsset);
            }
                
        }
    }
}