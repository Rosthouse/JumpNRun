using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JumpNRunShared.WorldObjects;
using JumpNRunShared.WorldObjects.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Editor.EditorObjects
{
    /// <summary>
    /// Represents an object only avaiable in the editor. It tracks special parameters for each object, coming from the 
    /// </summary>
    class EditorObject: WorldObject 
    {
        //Members
        private string name;
        private string type;

        private int layerDepth;

        private Point textureSize;
        private Point textureOrigin;

        private Dictionary<string, ParameterObject> parameterValueList;

        public EditorObject(): this(string.Empty, Vector2.Zero)
        {
        }


        public EditorObject(string textureAsset, Vector2 position): base(textureAsset, position)
        {
            parameterValueList = new Dictionary<string, ParameterObject>();
            
        } 

        public override void Update(GameTime gameTime)
        {
        
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int layerDepth)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }

        public override void OnCollision(WorldObject collisionObject)
        {
            
        }


    }

    internal struct ParameterObject
    {
        private ParameterType type;
        private string value;
    }
}
