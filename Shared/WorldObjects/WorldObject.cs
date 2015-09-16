using System;
using AudioLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SimplePhysicsAndCollision.Collision;
using SimplePhysicsAndCollision.Collision.Detection;
using SimplePhysicsAndCollision.Interfaces;

namespace JumpNRunShared.WorldObjects
{
    public abstract class WorldObject: ICollisionObject
    {
        //Graphics
        private Texture2D texture;
        private string textureAsset;
        private bool isVisible;

        //position
        protected Vector3 position;
        
        //physics and collision
        private QuadTreePositionItem<ICollisionObject> collisionObject;
        protected PhysicState physicState;

        //Audio
        protected SoundEffectList soundEffectList;

        

        protected WorldObject(string textureAsset, Vector2 position)
        {
            TextureAsset = textureAsset;
            this.position = new Vector3(position, 0);
        }

        protected WorldObject() : this(String.Empty, Vector2.Zero){}

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public virtual Rectangle IntersectRectangle
        {
            get { return new Rectangle((int)Position.X,
                                     (int)Position.Y,
                                     Texture.Width,
                                     Texture.Height);
            }
        }

        public virtual Vector2 Center
        {
            get
            {
                return new Vector2(this.position.X + (float)this.Texture.Width / 2,
                                   this.position.Y + (float)this.Texture.Height / 2);
            }
        }

        public QuadTreePositionItem<ICollisionObject> CollisionItem
        {
            get { return this.collisionObject; }
            set { this.collisionObject = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(position.X, position.Y); }
            set
            {
                position = new Vector3(value.X, value.Y, position.Z);
                this.collisionObject.Position = Position;
            }
        }

        public virtual Vector2 Size {
            get
            {
                if(Texture == null)
                {
                    return Vector2.Zero;
                }
                return new Vector2(Texture.Width, Texture.Height);
            }
        }

        public PhysicState State
        {
            get { return this.physicState; }
            set { this.physicState = physicState; }
        }

        public void Finalize(GameTime gameTime)
        {
            //do nothing, This object will die soon
        }

        public Vector3 TimePosition
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public bool IsVisible
        {
           get { return isVisible; }
            
            set { isVisible = value; }
        }

        public string TextureAsset
        {
            get { return textureAsset; }
            set { textureAsset = value; }
        }


        public virtual void Load(ContentManager contentManager)
        {
            if(TextureAsset != String.Empty)
            {
                Texture = contentManager.Load<Texture2D>(TextureAsset);   
            }
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, int layerDepth);

        public void PlaySound(string soundName)
        {
            soundEffectList.Play(soundName);
        }

        public abstract void OnCollision(WorldObject collisionObject);

        //public abstract void OutOfWindowHandeling(OutOfWindowPosition position);
    }
}