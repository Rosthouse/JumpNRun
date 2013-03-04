using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.WorldObjects
{
    public class Animation
    {
        #region Members

        private int millisecondsPerFrame;
        private int timeSinceLastFrame;

        public Vector2 sizePerFrame;
        private Point currentFrame;
        private Point sheetSize;

        private Point fromPosition;
        private Point toPosition;

        private SpriteEffects effect;

        private string name;

        #endregion - Members

        #region Constructors

        public Animation(string name, Point sheetSize, Vector2 sizePerFrame, Point fromPosition, Point toPosition)
        {
            this.name = name;
            this.sheetSize = sheetSize;
            this.fromPosition = fromPosition;
            this.toPosition = toPosition;

            this.sizePerFrame = sizePerFrame;

            currentFrame = fromPosition;

            millisecondsPerFrame = 100;
            timeSinceLastFrame = 0;

            effect = SpriteEffects.None;
        }

        #endregion - Constructors


        #region Acessors



        #endregion - Acessors

        #region Object Logic

        public void Update(GameTime gameTime, bool looksRight)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if(looksRight)
            {
                effect = SpriteEffects.None;
            }
            else
            {
                effect = SpriteEffects.FlipHorizontally;
            }

            if(!PointComparator(currentFrame, toPosition))
            {
                if (timeSinceLastFrame > millisecondsPerFrame)
                {

                    timeSinceLastFrame = 0;
                    ++currentFrame.X;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= sheetSize.Y)
                            currentFrame.Y = 0;
                    }
                } 
            }
            else
            {
                currentFrame = fromPosition;
            }
        }

        public void Draw(Vector2 position, GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture, int layerDepth)
        {
            //spriteBatch.Begin(); texture.Width/(int)this.sizePerFrame.X
          
            spriteBatch.Draw(texture, 
                new Rectangle((int)position.X, (int)position.Y, (int)sizePerFrame.X, (int)sizePerFrame.Y), 
                             new Rectangle((int)(currentFrame.X*sizePerFrame.X), (int)(currentFrame.Y*sizePerFrame.Y), (int)sizePerFrame.X, (int)sizePerFrame.Y), Color.White, 0f, new Vector2(0, 0), effect, layerDepth);
          
            //spriteBatch.End();
        }

        public bool PointComparator(Point a, Point b)
        {
            if(a.X >= b.X)
            {
                if(a.Y >= b.Y)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        #endregion - Object Logic
    }
}