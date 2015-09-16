using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.WorldObjects
{
    public abstract class AnimatedObject : MovingObject
    {

        #region Members

        public int walkedDistance;
        private List<Animation> animations;
        protected int currentAnimation;

        protected bool looksRight;
        
        #endregion - Members

        #region Constructors

        public AnimatedObject(string textureAsset, Vector2 position, float acceleration, float maxSpeed)
            : base(textureAsset, position, acceleration, maxSpeed)
        {
            looksRight = true;
            animations = new List<Animation>();
            walkedDistance = 0;
        }

        
        #endregion - Constructors

        #region Accessors

        public override Vector2 Size
        {
            get
            {
                return new Vector2(animations[currentAnimation].sizePerFrame.X,
                                animations[currentAnimation].sizePerFrame.Y);
            }
        }

        public override Rectangle IntersectRectangle
        {
            get
            {
                return new Rectangle((int)Position.X,
                                     (int)Position.Y,
                                     (int)Size.X,
                                     (int)Size.Y);
            }
        }

        public List<Animation> Animations
        {
            get { return animations; }
        }

        public int CurrentAnimation
        {
            get { return currentAnimation; }
        } 

        #endregion - Accessors

        #region Object Logic
		

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int layerDepth)
        {
            animations[currentAnimation].Draw(Position, gameTime, spriteBatch, Texture, layerDepth);
        }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            base.Load(contentManager);
            createAnimations();

            
        }

        public virtual void killObject(AnimatedObject killer)
        {
            
        }


        #endregion - Object Logic
       
        public void AddAnimation(Animation animation)
        {
            this.animations.Add(animation);

            currentAnimation = 0;
        }

        public abstract void createAnimations();

        

        
    }
}