using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SimplePhysicsAndCollision.Interfaces;

namespace JumpNRunShared.WorldObjects.Enemies
{
    public class GreenTurtle: Enemy, IPhysicsObject
    {
        private Point boundaries;
        private bool isWalkingRight;
        private Vector3 lastMovementVector;

        public GreenTurtle(string textureAsset, Vector2 position, float acceleration, float maxSpeed,  Point boundaries) 
            : base(textureAsset, position, acceleration, maxSpeed)
        {
            isWalkingRight = false;
            this.boundaries = boundaries;

            lastMovementVector = Vector3.Zero;
        }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            soundEffectList.Add("dance", contentManager.Load<SoundEffect>(@"Audio\Effects\tingle_dance"));
            soundEffectList.Add("hoi", contentManager.Load<SoundEffect>(@"Audio\Effects\tingle_hoi"));
            soundEffectList.Add("laugh", contentManager.Load<SoundEffect>(@"Audio\Effects\tingle_laugh"));
            soundEffectList.Add("magic", contentManager.Load<SoundEffect>(@"Audio\Effects\tingle_magic"));

            base.Load(contentManager);
        }

        public override void Update(GameTime gameTime)
        {
            //movementVector = Vector3.Zero;

            if(this.Position.X <= boundaries.X && !isWalkingRight)
            {
                isWalkingRight = true;
                PlayRandomSong();
            } 
            else if(this.Position.X >= boundaries.Y && isWalkingRight)
            {
                this.isWalkingRight = false;
                PlayRandomSong();
            }

            if(isWalkingRight)
            {
                inputVector.X = 1;
            } else
            {
                inputVector.X = -1;
            }

            Animations[CurrentAnimation].Update(gameTime, isWalkingRight);


            base.Update(gameTime);
        }

        private void PlayRandomSong()
        {
            Random random = new Random();
            int rand = (int) (random.NextDouble()*4);

            switch (rand)
            {
                case 0:
                    PlaySound("dance");
                    //soundEffectList.Play("dance");
                    break;
                case 1:
                    PlaySound("hoi");
                    //soundEffectList.Play("hoi");
                    break;
                case 2:
                    PlaySound("laugh");
                    //soundEffectList.Play("laugh");
                    break;
                case 3:
                    PlaySound("magic");
                    //soundEffectList.Play("magic");
                    break;
            }
        }

        public override void createAnimations()
        {
            //throw new NotImplementedException();
            AddAnimation(new Animation("idle", new Point(0, 0),
                                       new Vector2(((Texture.Width / 1)), Texture.Height),
                                       new Point(0, 0), new Point(0, 0)));
        }

        public int Weight
        {
            get { return 5; }
        }
    }
}
