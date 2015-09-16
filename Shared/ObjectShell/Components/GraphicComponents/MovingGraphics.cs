using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.ObjectShell.Components.GraphicComponents
{
    class MovingGraphics: GraphicComponent
    {
        private PhysicsComponents.PhysicComponent physicsComponent;


        public MovingGraphics(ObjectShell parent, string texture) : base(parent, texture)
        {
            physicsComponent = new PhysicsComponents.EmptyPhysicComponent(parent);
        }

        public void SetPositionItem(PhysicsComponents.PhysicComponent physicsComponent)
        {
            this.physicsComponent = physicsComponent;
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(texture.Height, texture.Width);
            }
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, physicsComponent.Position, Color.White);
        }
    }
}
