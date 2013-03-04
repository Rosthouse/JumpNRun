using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.ObjectShell.Components.GraphicComponents
{
    /// <summary>
    /// This class holds information about the graphical representation of a game object
    /// </summary>
    public abstract class GraphicComponent: BaseComponent
    {
        protected Texture2D texture;
        private string textureAsset;

        protected GraphicComponent(ObjectShell parent, string textureAsset) : base(parent)
        {
            this.textureAsset = textureAsset;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public void Load(ContentManager contentManager)
        {
            if(textureAsset != string.Empty)
            {
                this.texture = contentManager.Load<Texture2D>(textureAsset);
            }
        }
    }
}
